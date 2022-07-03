using Moq;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class ItemCardPlayedEventTests
    {
        [Theory, AutoGameData]
        public void ItemCardPlayedEvent_ShouldInvokeEquipMethod(
            Player player, ItemCard card)
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
            ItemCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(card).Verify(m => m.TryEquip(It.IsAny<Character>()), Times.Once);
        }

        [Theory, AutoGameData]
        public void ItemCardPlayedEvent_ShouldRemoveCardFromHand_WhenCharacterEquippedItem(
            Player player, ItemCard card)
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

            Mock.Get(card).Setup(m => m.TryEquip(It.IsAny<Character>())).Returns(true);

            // Act
            ItemCardPlayedEvent @event = new(game.Id, player.Id, card.Id);
            @event.Apply(game);

            // Assert
            place.InHandCards.ShouldNotContain(card);
        }
    }
}
