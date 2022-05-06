using EventStore.Client;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Application.Services
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

        private async Task<EventStreamHandler> InitializeAsync(CancellationToken cancellationToken = default)
        {
            subscription = await service.SubscribeAsync(EventAppeared, cancellationToken);
            return this;
        }

        public static Task<EventStreamHandler> CreateAsync(
            IEventService service, IGameRepository repository, CancellationToken cancellationToken = default)
        {
            var instance = new EventStreamHandler(service, repository);
            return instance.InitializeAsync(cancellationToken);
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
