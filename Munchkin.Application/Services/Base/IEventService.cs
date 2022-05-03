using EventStore.Client;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Application.Services.Base
{
    public interface IEventService
    {
        Task PublishAsync(IGameEvent @event);
        Task<StreamSubscription> SubscribeAsync(Func<IGameEvent, Task> eventAppeared);
    }
}
