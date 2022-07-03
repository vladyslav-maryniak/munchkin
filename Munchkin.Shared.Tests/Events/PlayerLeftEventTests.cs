using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class PlayerLeftEventTests
    {
        [Theory, AutoGameData]
        public void PlayerLeftEvent_ShouldRemovePlaceFromTable(
            Player player)
        {
            // Assert
            var place = new PlaceBuilder()
                .WithPlayer(player)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            PlayerLeftEvent @event = new(game.Id, player.Id);
            @event.Apply(game);

            // Assert
            table.Places.ShouldNotContain(place);
        }
    }
}
