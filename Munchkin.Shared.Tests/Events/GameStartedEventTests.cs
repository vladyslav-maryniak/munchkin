using AutoFixture;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class GameStartedEventTests
    {
        private const int numberOfTreasureCards = 4;
        private const int numberOfDoorCards = 4;

        [Theory, InlineData(5)]
        public void GameStartedEvent_ShouldMoveAllPlayersFromLobbyToTable(
            int numberOfPlayers)
        {
            // Arrange
            var lobby = new LobbyBuilder()
                .WithPlayers(numberOfPlayers)
                .Build();
            var table = new TableBuilder()
                .WithDoorDeck(numberOfDoorCards * numberOfPlayers)
                .WithTreasureDeck(numberOfTreasureCards * numberOfPlayers)
                .Build();
            var game = new GameBuilder()
                .WithLobby(lobby)
                .WithTable(table)
                .Build();

            // Act
            GameStartedEvent @event = new(game.Id);
            @event.Apply(game);

            // Assert
            lobby.Players.ShouldBeEmpty();
            game.Table.Places.Count.ShouldBe(numberOfPlayers);
        }

        [Theory, InlineData(5)]
        public void GameStartedEvent_ShouldHandOutCardsToEachPlayer(
            int numberOfPlaces)
        {
            // Arrange
            var table = new TableBuilder()
                .WithDoorDeck(numberOfDoorCards * numberOfPlaces)
                .WithTreasureDeck(numberOfTreasureCards * numberOfPlaces)
                .WithPlaces(fixture =>
                    fixture.Build<Place>()
                           .Without(x => x.InHandCards)
                           .CreateMany(numberOfPlaces))
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            GameStartedEvent @event = new(game.Id);
            @event.Apply(game);

            // Assert
            int numberOfCards = numberOfDoorCards + numberOfTreasureCards;
            table.Places.ShouldAllBe(x => x.InHandCards.Count == numberOfCards);
        }

        [Fact]
        public void GameStartedEvent_ShouldAssignGameStatusOfStarted()
        {
            // Arrange
            var game = new GameBuilder()
                .WithStatus(GameStatus.NotStarted)
                .Build();

            // Act
            GameStartedEvent @event = new(game.Id);
            @event.Apply(game);

            // Assert
            game.Status.ShouldBe(GameStatus.Started);
        }
    }
}
