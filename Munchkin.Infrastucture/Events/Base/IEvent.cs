using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events.Base
{
    public interface IEvent
    {
        Guid GameId { get; init; }

        void Apply(Game game);
    }
}
