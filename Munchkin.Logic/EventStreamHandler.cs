using EventStore.Client;
using Munchkin.DataAccess;
using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Logic
{
    public class EventStreamHandler : IDisposable
    {
        private readonly IEventService service;
        private StreamSubscription? subscription;
        
        private readonly List<Game> games = new() { new(Guid.Parse("166b7c67-01ee-4ca6-b83d-23f5af9ec67d")) };

        private EventStreamHandler(IEventService service)
        {
            this.service = service;
        }

        private async Task<EventStreamHandler> InitializeAsync()
        {
            subscription = await service.SubscribeAsync(EventAppeared);
            return this;
        }

        public static Task<EventStreamHandler> CreateAsync(IEventService service)
        {
            var instance = new EventStreamHandler(service);
            return instance.InitializeAsync();
        }

        private void EventAppeared(IGameEvent @event)
        {
            var game = games.First(x => x.Id == @event.GameId);
            @event.Apply(game);
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
