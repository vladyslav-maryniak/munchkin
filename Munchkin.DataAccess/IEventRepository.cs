using EventStore.Client;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.DataAccess
{
    public interface IEventRepository
    {
        Task PublishAsync(IEvent @event);

        Task<StreamSubscription> SubscribeAsync(
            Func<StreamSubscription, ResolvedEvent, CancellationToken, Task> eventAppeared);

        IEvent DeserializeEvent(ResolvedEvent resolvedEvent);
    }
}
