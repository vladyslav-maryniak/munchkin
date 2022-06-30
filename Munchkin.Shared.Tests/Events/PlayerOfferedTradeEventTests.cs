using Munchkin.Shared.Events;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerOfferedTradeEventTests
    {
        [Theory, AutoGameData]
        public void PlayerOfferedTradeEvent_ShouldAddTradeToTable(
            Trade trade)
        {
            // Arrange
            var game = new GameBuilder()
                .Build();

            // Act
            PlayerOfferedTradeEvent @event = new(
                game.Id,
                trade.OfferorId,
                trade.OffereeId,
                trade.OfferorItemCardIds,
                trade.OffereeItemCardIds);
            @event.Apply(game);

            // Assert
            game.Table.Offers.Count.ShouldBe(1);
        }
    }
}
