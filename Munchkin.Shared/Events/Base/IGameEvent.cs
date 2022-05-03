using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events.Base
{
    public interface IGameEvent
    {
        Guid GameId { get; init; }

        void Apply(Game game);
    }
}
