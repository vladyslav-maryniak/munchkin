using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class MonsterCardDrewEventTests
    {
        [Theory, AutoGameData]
        public void MonsterCardDrewEvent_ShouldAddCharacterAndMonsterToCombatField(
            MonsterCard card, Place place)
        {
            // Arrange
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithDoorDeck(card)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            MonsterCardDrewEvent @event = new(game.Id, place.Player.Id);
            @event.Apply(game);

            // Assert
            table.CombatField.ShouldSatisfyAllConditions(
                x => x.CharacterSquad.ShouldContain(place.Character),
                x => x.MonsterSquad.ShouldContain(card));
        }
    }
}
