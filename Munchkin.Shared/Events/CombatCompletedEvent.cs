using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CombatCompletedEvent(Guid GameId) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.TurnIndex++;
            game.Table.CombatField.Clear();
        }
    }
}
