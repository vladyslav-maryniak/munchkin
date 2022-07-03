using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CharacterGotHelpEventTests
    {
        [Theory, AutoGameData]
        public void CharacterGotHelpEvent_ShouldAddCharacterToSquad_WhenSquadDoesNotContainsCharacter(
            Place helper)
        {
            // Arrange
            var table = new TableBuilder()
                .WithPlaces(helper)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterGotHelpEvent @event = new(game.Id, helper.Character.Id);
            @event.Apply(game);

            // Assert
            game.Table.CombatField.CharacterSquad.ShouldContain(helper.Character);
        }

        [Theory, AutoGameData]
        public void CharacterGotHelpEvent_ShouldAssignOffereeIdToReward_WhenRewardIsIntended(
            Place helper, Reward reward)
        {
            // Arrange
            var combatField = new CombatFieldBuilder()
                .WithReward(reward)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(helper)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterGotHelpEvent @event = new(game.Id, helper.Character.Id);
            @event.Apply(game);

            // Assert
            game.Table.CombatField.Reward!.OffereeId.ShouldNotBe(Guid.Empty);
        }
    }
}
