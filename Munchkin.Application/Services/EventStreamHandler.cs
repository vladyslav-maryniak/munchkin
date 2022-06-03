using EventStore.Client;
using Microsoft.AspNetCore.SignalR;
using Munchkin.Application.Hubs;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Application.Services
{
    public class EventStreamHandler : IDisposable
    {
        private readonly IEventService service;
        private readonly IGameRepository repository;
        private readonly IHubContext<EventHub> hub;
        private StreamSubscription? subscription;

        private EventStreamHandler(
            IEventService service,
            IGameRepository repository,
            IHubContext<EventHub> hub)
        {
            this.service = service;
            this.repository = repository;
            this.hub = hub;
        }

        private async Task<EventStreamHandler> InitializeAsync(CancellationToken cancellationToken = default)
        {
            subscription = await service.SubscribeAsync(EventAppeared, cancellationToken);
            return this;
        }

        public static Task<EventStreamHandler> CreateAsync(
            IEventService service,
            IGameRepository repository,
            IHubContext<EventHub> hub,
            CancellationToken cancellationToken = default)
        {
            var instance = new EventStreamHandler(service, repository, hub);
            return instance.InitializeAsync(cancellationToken);
        }

        private async Task EventAppeared(IGameEvent @event)
        {
            var game = await repository.GetGameAsync(@event.GameId);
            @event.Apply(game);
            await repository.UpdateGameAsync(game);

            string eventName = @event.GetType().ToString().Split('.').Last();
            await hub.Clients.All.SendAsync(@event.GameId.ToString(), eventName);
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
