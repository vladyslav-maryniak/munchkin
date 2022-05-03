using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record PlayerEscapedEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table = new();
        }
    }
}
