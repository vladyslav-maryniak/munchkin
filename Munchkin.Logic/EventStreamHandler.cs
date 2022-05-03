using EventStore.Client;
using Munchkin.DataAccess.Base;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.Logic
{
    public class EventStreamHandler : IDisposable
    {
        private readonly IEventService service;
        private readonly IGameRepository repository;
        private StreamSubscription? subscription;
        
        private EventStreamHandler(IEventService service, IGameRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        private async Task<EventStreamHandler> InitializeAsync()
        {
            subscription = await service.SubscribeAsync(EventAppeared);
            return this;
        }

        public static Task<EventStreamHandler> CreateAsync(IEventService service, IGameRepository repository)
        {
            var instance = new EventStreamHandler(service, repository);
            return instance.InitializeAsync();
        }

        private async Task EventAppeared(IGameEvent @event)
        {
            var game = await repository.GetGameAsync(@event.GameId);
            @event.Apply(game);
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
