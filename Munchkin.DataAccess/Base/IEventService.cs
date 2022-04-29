using EventStore.Client;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.DataAccess.Base
{
    public interface IEventService
    {
        Task PublishAsync(IGameEvent @event);
        Task<StreamSubscription> SubscribeAsync(Func<IGameEvent, Task> eventAppeared);
    }
}
