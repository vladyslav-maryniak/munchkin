using Munchkin.Shared.Events;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerOfferedRewardEventTests
    {
        [Theory, AutoGameData]
        public void PlayerOfferedRewardEvent_ShouldAssignRewardToCombatField(
            Reward offer)
        {
            // Arrange
            var game = new GameBuilder()
                .Build();

            // Act
            PlayerOfferedRewardEvent @event = new(
                game.Id,
                offer.OfferorId,
                offer.ItemCardIds,
                offer.CardIdsForPlay,
                offer.VictoryTreasures,
                offer.NumberOfTreasures,
                offer.HelperPicksFirst);
            @event.Apply(game);

            // Assert
            game.Table.CombatField.Reward.ShouldNotBeNull();
        }
    }
}
