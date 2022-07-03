using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerJoinedEventTests
    {
        [Theory, AutoGameData]
        public void PlayerJoinedEvent_ShouldAddPlayerToLobby(
            Player player)
        {
            // Arrange
            var game = new GameBuilder()
                .Build();

            // Act
            PlayerJoinedEvent @event = new(game.Id, player);
            @event.Apply(game);

            // Assert
            game.Lobby.Players.ShouldContain(player);
        }
    }
}
