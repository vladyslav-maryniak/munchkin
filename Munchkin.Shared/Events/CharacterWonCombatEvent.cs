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

            place.Character.Level += game.Table.CombatField.MonsterSquad
                .Select(x => x.VictoryLevels)
                .Aggregate((result, x) => result + x);

            var victoryTreasures = game.Table.CombatField.MonsterSquad
                .Select(x => x.Treasures)
                .Aggregate((result, x) => result + x);

            for (int i = 0; i < victoryTreasures; i++)
            {
                place.InHandCards.Add(game.Table.TreasureDeck.Pop());
            }

            var combatField = game.Table.CombatField;

            if (combatField.Reward is not null && combatField.Reward.OffereeId != Guid.Empty)
            {
                combatField.Reward.Perform(game);
            }
        }
    }
}
