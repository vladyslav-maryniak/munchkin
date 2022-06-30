using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CurseCardDrewEventTests
    {
        [Theory, AutoGameData]
        public void CurseCardDrewEvent_ShouldAddCurseToCharacter(
            CurseCard card, Place place)
        {
            // Arrange
            var table = new TableBuilder()
                .WithDoorDeck(card)
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CurseCardDrewEvent @event = new(game.Id, place.Player.Id);
            @event.Apply(game);

            // Assert
            place.Character.Curses.ShouldContain(card);
        }

        [Theory, AutoGameData]
        public void CurseCardDrewEvent_ShouldAddCharacterAndCurseToCombatField(
            CurseCard card, Place place)
        {
            // Arranges
            var table = new TableBuilder()
                .WithDoorDeck(card)
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CurseCardDrewEvent @event = new(game.Id, place.Player.Id);
            @event.Apply(game);

            // Assert
            table.CombatField.ShouldSatisfyAllConditions(
                x => x.CharacterSquad.ShouldContain(place.Character),
                x => x.CursePlace.ShouldBe(card));
        }
    }
}
