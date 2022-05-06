using Munchkin.Application.Services;
using Munchkin.Application.Services.Base;

namespace Munchkin.API
{
    public class EventHostedService : IHostedService
    {
        private readonly IEventService service;
        private readonly IGameRepository repository;
        private EventStreamHandler? eventService;

        public EventHostedService(IEventService service, IGameRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            eventService = await EventStreamHandler.CreateAsync(service, repository, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            eventService?.Dispose();
            return Task.CompletedTask;
        }
    }
}
