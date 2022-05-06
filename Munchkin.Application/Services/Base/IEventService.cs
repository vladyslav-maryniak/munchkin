using EventStore.Client;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Application.Services.Base
{
    public interface IEventService
    {
        Task PublishAsync(IGameEvent @event, CancellationToken cancellationToken = default);
        Task<StreamSubscription> SubscribeAsync(
            Func<IGameEvent, Task> eventAppeared, CancellationToken cancellationToken = default);
    }
}
