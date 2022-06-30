using Munchkin.Shared.Events;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class GameStateUpdatedEventTests
    {
        [Theory, AutoGameData]
        public void GameStateUpdated_ShouldAssignStateName(
            string state)
        {
            // Assert
            var game = new GameBuilder()
                .Build();

            // Act
            GameStateUpdatedEvent @event = new(game.Id, state);
            @event.Apply(game);

            // Arrange
            game.State.ShouldBe(state);
        }
    }
}
