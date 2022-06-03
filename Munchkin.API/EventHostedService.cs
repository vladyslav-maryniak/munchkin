using Microsoft.AspNetCore.SignalR;
using Munchkin.Application.Hubs;
using Munchkin.Application.Services;
using Munchkin.Application.Services.Base;

namespace Munchkin.API
{
    public class EventHostedService : IHostedService
    {
        private readonly IEventService service;
        private readonly IGameRepository repository;
        private readonly IHubContext<EventHub> hub;
        private EventStreamHandler? eventService;

        public EventHostedService(
            IEventService service,
            IGameRepository repository,
            IHubContext<EventHub> hub)
        {
            this.service = service;
            this.repository = repository;
            this.hub = hub;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            eventService = await EventStreamHandler.CreateAsync(service, repository, hub, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            eventService?.Dispose();
            return Task.CompletedTask;
        }
    }
}
