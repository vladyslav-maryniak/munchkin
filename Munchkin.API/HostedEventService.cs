using Munchkin.DataAccess;
using Munchkin.Logic;

namespace Munchkin.API
{
    public class HostedEventService : IHostedService
    {
        private readonly IEventRepository repository;
        private EventService? eventService;

        public HostedEventService(IEventRepository repository)
        {
            this.repository = repository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            eventService = await EventService.CreateAsync(repository);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            eventService?.Dispose();
            return Task.CompletedTask;
        }
    }
}
