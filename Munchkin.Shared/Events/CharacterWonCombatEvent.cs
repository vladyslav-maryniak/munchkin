using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterWonCombatEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Character.Id == CharacterId);
            var combatField = game.Table.CombatField;

            place.Character.Level += combatField.VictoryLevels;

            for (int i = 0; i < combatField.VictoryTreasures; i++)
            {
                place.InHandCards.Add(game.Table.DrawTreasureCard(out bool _));
            }

            if (combatField.Reward is not null && combatField.Reward.OffereeId != Guid.Empty)
            {
                combatField.Reward.Perform(game);
            }
        }
    }
}
