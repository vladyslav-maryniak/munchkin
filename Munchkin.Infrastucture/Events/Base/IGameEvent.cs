using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events.Base
{
    public interface IGameEvent
    {
        Guid GameId { get; init; }

        void Apply(Game game);
    }
}
