using Moq;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerSoldCardsEventTests
    {
        [Theory, AutoGameData]
        public void PlayerSoldCardsEvent_ShouldRaiseCharacterLevelByTheNumberOfThousandsOfGoldPieces(
            ItemCard[] itemCards, Player player)
        {
            // Arrange
            var character = new CharacterBuilder()
                .WithLevel(1)
                .Build();
            var place = new PlaceBuilder()
                .WithPlayer(player)
                .WithCharacter(character)
                .WithInHandCards(itemCards)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            var initialLevel = character.Level;
            
            foreach (var itemCard in itemCards)
            {
                Mock.Get(itemCard)
                    .Setup(x => x.GoldPieces)
                    .Returns(PlayerSoldCardsEvent.MinAmountOfGoldPiecesForSale);
            }

            // Act
            PlayerSoldCardsEvent @event = new(game.Id, player.Id, itemCards.Select(x => x.Id));
            @event.Apply(game);

            // Assert
            var difference = character.Level - initialLevel;
            difference.ShouldBe(itemCards.Length);
        }

        [Theory, AutoGameData]
        public void PlayerSoldCardsEvent_ShouldRemoveAllCardsForSaleFromHand(
            ItemCard[] itemCards, Player player)
        {
            // Arrange
            var place = new PlaceBuilder()
                .WithPlayer(player)
                .WithInHandCards(itemCards)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            foreach (var itemCard in itemCards)
            {
                Mock.Get(itemCard)
                    .Setup(x => x.GoldPieces)
                    .Returns(PlayerSoldCardsEvent.MinAmountOfGoldPiecesForSale);
            }

            // Act
            PlayerSoldCardsEvent @event = new(game.Id, player.Id, itemCards.Select(x => x.Id));
            @event.Apply(game);

            // Assert
            place.InHandCards.Intersect(itemCards).ShouldBeEmpty();
        }
    }
}
