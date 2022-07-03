using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CombatCompletedEventTests
    {
        [Theory, AutoGameData]
        public void CombatCompletedEvent_ShouldIncreaseTurnIndexByOne(
            int turnIndex)
        {
            // Arrange
            var game = new GameBuilder()
                .WithTurnIndex(turnIndex)
                .Build();

            // Act
            CombatCompletedEvent @event = new(game.Id);
            @event.Apply(game);

            // Assert
            int difference = game.TurnIndex - turnIndex;
            difference.ShouldBe(1);
        }

        [Theory, AutoGameData]
        public void CombatCompletedEvent_ShouldClearCombatField(
            CombatField combatField)
        {
            // Arrange
            var table = new TableBuilder()
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CombatCompletedEvent @event = new(game.Id);
            @event.Apply(game);

            // Assert
            game.ShouldSatisfyAllConditions(
                x => x.Table.CombatField.MonsterSquad.Count.ShouldBe(0),
                x => x.Table.CombatField.CharacterSquad.Count.ShouldBe(0),
                x => x.Table.CombatField.CursePlace.ShouldBeNull(),
                x => x.Table.CombatField.Reward.ShouldBeNull()
            );
        }
    }
}
