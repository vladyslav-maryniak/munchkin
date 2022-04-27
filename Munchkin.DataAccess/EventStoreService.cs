using EventStore.Client;
using Munchkin.Infrastucture.Events;
using Munchkin.Infrastucture.Events.Base;
using Newtonsoft.Json;
using System.Text;

namespace Munchkin.DataAccess
{
    public class EventStoreService : IEventRepository
    {
        private const string connectionString = "esdb://localhost:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";
        private readonly EventStoreClient client;

        public EventStoreService()
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            client = new EventStoreClient(settings);
        }

        public Task PublishAsync(IEvent @event)
        {
            var eventData = SerializeEvent(@event);
            var streamId = BuildStreamId(@event.GameId);

            return client.AppendToStreamAsync(streamId, StreamState.Any, new[] { eventData });
        }

        public Task<StreamSubscription> SubscribeAsync(
            Func<StreamSubscription, ResolvedEvent, CancellationToken, Task> eventAppeared)
        {
            return client.SubscribeToAllAsync(
                FromAll.End,
                eventAppeared,
                filterOptions: new SubscriptionFilterOptions(EventTypeFilter.ExcludeSystemEvents())
            );
        }

        private EventData SerializeEvent(IEvent @event)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            return new(Uuid.NewUuid(), @event.GetType().Name, bytes);
        }

        public IEvent DeserializeEvent(ResolvedEvent resolvedEvent)
        {
            var eventType = Type.GetType(BuildTypeName(resolvedEvent));
            if (eventType == null) throw new Exception();

            var decodedObject = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var @event = (IEvent?)JsonConvert.DeserializeObject(decodedObject, eventType);
            if (@event == null) throw new Exception();

            return @event;
        }

        private string BuildTypeName(ResolvedEvent resolvedEvent)
        {
            var type = typeof(EventEntrypoint);
            return $"{type.Namespace}.{resolvedEvent.Event.EventType}, {type.Assembly}";
        }

        private string BuildStreamId(Guid gameId) => $"MunchkinGameStream_{gameId}";
    }
}
