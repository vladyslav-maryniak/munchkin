using Munchkin.Shared.Events;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerDeclinedOfferEventTests
    {
        [Theory, AutoGameData]
        public void PlayerDeclinedOfferEvent_ShouldRemoveOfferFromTable(
            Offer offer)
        {
            // Arrange
            var table = new TableBuilder()
                .WithOffers(offer)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            PlayerDeclinedOfferEvent @event = new(game.Id, offer.Id);
            @event.Apply(game);

            // Assert
            table.Offers.ShouldNotContain(offer);
        }
    }
}
