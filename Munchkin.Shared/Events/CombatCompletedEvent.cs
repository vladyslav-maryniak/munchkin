using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CombatCompletedEvent(Guid GameId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var cards = game.Table.CombatField.Clear();
            game.Table.Discard(cards.ToArray());
            game.TurnIndex++;
        }
    }
}
