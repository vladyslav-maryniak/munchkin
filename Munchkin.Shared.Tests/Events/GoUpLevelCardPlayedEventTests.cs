using Moq;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class GoUpLevelCardPlayedEventTests
    {
        [Theory, AutoGameData]
        public void GoUpLevelCardPlayedEvent_ShouldInvokeRiseLevelMethod(
            Player player, GoUpLevelCard card)
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
            GoUpLevelCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(card).Verify(m => m.TryRiseLevel(It.IsAny<Character>()), Times.Once);
        }

        [Theory, AutoGameData]
        public void GoUpLevelCardPlayedEvent_ShouldRemoveCardFromHand_WhenLevelRaisedUp(
            Player player, GoUpLevelCard card)
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

            Mock.Get(card).Setup(x => x.TryRiseLevel(It.IsAny<Character>())).Returns(true);

            // Act
            GoUpLevelCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            place.InHandCards.ShouldNotContain(card);
        }
    }
}
