using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerWonGameEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Status = GameStatus.Finished;
        }
    }
}
