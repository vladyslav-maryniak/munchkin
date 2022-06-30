using Munchkin.Shared.Events;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerOfferedBribeEventTests
    {
        [Theory, AutoGameData]
        public void PlayerOfferedBribeEvent_ShouldAddBribeToTable(
            Bribe offer)
        {
            // Arrange
            var game = new GameBuilder()
                .Build();

            // Act
            PlayerOfferedBribeEvent @event = new(
                game.Id,
                offer.OfferorId,
                offer.OffereeId,
                offer.Agreement,
                offer.ItemCardIds);
            @event.Apply(game);

            // Assert
            game.Table.Offers.Count.ShouldBe(1);
        }
    }
}
