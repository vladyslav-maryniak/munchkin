using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record GameStateUpdated(Guid GameId, string State) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.State = State;
        }
    }
}
