using EventStore.Client;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.DataAccess
{
    public interface IEventService
    {
        Task PublishAsync(IGameEvent @event);
        Task<StreamSubscription> SubscribeAsync(Action<IGameEvent> eventAppeared);
    }
}
