using Munchkin.DataAccess;
using Munchkin.Logic;

namespace Munchkin.API
{
    public class EventHostedService : IHostedService
    {
        private readonly IEventService service;
        private readonly IEventRepository repository;
        private EventStreamHandler? eventService;

        public EventHostedService(IEventService service, IEventRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            eventService = await EventStreamHandler.CreateAsync(service, repository);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            eventService?.Dispose();
            return Task.CompletedTask;
        }
    }
}
