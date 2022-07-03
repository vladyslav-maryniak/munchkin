using Moq;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerAcceptedOfferEventTests
    {
        [Theory, AutoGameData]
        public void PlayerAcceptedOfferEvent_ShouldInvokePerformMethod(
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
            PlayerAcceptedOfferEvent @event = new(game.Id, offer.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(offer).Verify(m => m.Perform(It.IsAny<Game>()));
        }

        [Theory, AutoGameData]
        public void PlayerAcceptedOfferEvent_ShouldRemoveOfferFromTable(
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
            PlayerAcceptedOfferEvent @event = new(game.Id, offer.Id);
            @event.Apply(game);

            // Assert
            table.Offers.ShouldNotContain(offer);
        }
    }
}
