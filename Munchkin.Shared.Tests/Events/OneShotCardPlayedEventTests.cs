using Moq;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class OneShotCardPlayedEventTests
    {
        [Theory, AutoGameData]
        public void OneShotCardPlayedEvent_ShouldInvokeUseMethod(
            OneShotCard card, Player player)
        {
            // Arrange
            var place = new PlaceBuilder()
                .WithPlayer(player)
                .WithInHandCards(card)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            OneShotCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(card).Verify(m => m.TryUse(It.IsAny<Table>(), default), Times.Once);
        }

        [Theory, AutoGameData]
        public void OneShotCardPlayedEvent_ShouldRemoveCardFromHand_WhenCardWasUsed(
            OneShotCard card, Player player)
        {
            // Arrange
            var place = new PlaceBuilder()
                .WithPlayer(player)
                .WithInHandCards(card)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            Mock.Get(card).Setup(m => m.TryUse(It.IsAny<Table>(), default)).Returns(true);

            // Act
            OneShotCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            place.InHandCards.ShouldNotContain(card);
        }
    }
}
