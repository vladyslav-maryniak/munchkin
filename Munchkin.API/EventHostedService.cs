using Munchkin.DataAccess;
using Munchkin.Logic;

namespace Munchkin.API
{
    public class EventHostedService : IHostedService
    {
        private readonly IEventService service;
        private EventStreamHandler? eventService;

        public EventHostedService(IEventService service)
        {
            this.service = service;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            eventService = await EventStreamHandler.CreateAsync(service);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            eventService?.Dispose();
            return Task.CompletedTask;
        }
    }
}
