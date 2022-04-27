using EventStore.Client;
using Munchkin.DataAccess;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Logic
{
    public class EventService : IDisposable
    {
        private readonly IEventRepository repository;
        private StreamSubscription? subscription;
        
        private readonly List<Game> games = new() { new(Guid.Parse("166b7c67-01ee-4ca6-b83d-23f5af9ec67d")) };

        private EventService(IEventRepository repository)
        {
            this.repository = repository;
        }

        private async Task<EventService> InitializeAsync()
        {
            subscription = await repository.SubscribeAsync(EventAppeared);
            return this;
        }

        public static Task<EventService> CreateAsync(IEventRepository repository)
        {
            var instance = new EventService(repository);
            return instance.InitializeAsync();
        }

        private Task EventAppeared(
            StreamSubscription subscription, ResolvedEvent resolvedEvent, CancellationToken cancellationToken)
        {
            var @event = repository.DeserializeEvent(resolvedEvent);

            var game = games.First(x => x.Id == @event.GameId);
            @event.Apply(game);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
