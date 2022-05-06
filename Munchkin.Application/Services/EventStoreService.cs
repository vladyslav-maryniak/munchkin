using EventStore.Client;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events.Base;
using Newtonsoft.Json;
using System.Text;

namespace Munchkin.Application.Services
{
    public class EventStoreService : IEventService
    {
        private const string connectionString = "esdb://localhost:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";
        private readonly EventStoreClient client;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public EventStoreService()
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            client = new EventStoreClient(settings);
            jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public Task PublishAsync(IGameEvent @event, CancellationToken cancellationToken = default)
        {
            var eventData = SerializeEvent(@event);
            var streamId = @event.GameId.BuildStreamId();

            return client.AppendToStreamAsync(
                streamId,
                StreamState.Any,
                new[] { eventData },
                cancellationToken: cancellationToken
            );
        }

        public Task<StreamSubscription> SubscribeAsync(
            Func<IGameEvent, Task> eventAppeared, CancellationToken cancellationToken = default)
        {
            return client.SubscribeToAllAsync(
                FromAll.End,
                (subscription, resolvedEvent, cancellationToken) =>
                {
                    var @event = DeserializeEvent(resolvedEvent);
                    return eventAppeared(@event);
                },
                filterOptions: new SubscriptionFilterOptions(EventTypeFilter.ExcludeSystemEvents()),
                cancellationToken: cancellationToken
            );
        }

        private EventData SerializeEvent(IGameEvent @event)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, jsonSerializerSettings));
            return new(Uuid.NewUuid(), @event.GetType().Name, bytes);
        }

        private IGameEvent DeserializeEvent(ResolvedEvent resolvedEvent)
        {
            var eventType = Type.GetType(resolvedEvent.BuildTypeName());
            if (eventType == null) throw new Exception();

            var decodedObject = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var @event = (IGameEvent?)JsonConvert.DeserializeObject(decodedObject, eventType, jsonSerializerSettings);
            if (@event == null) throw new Exception();

            return @event;
        }
    }
}
