using Moq;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CharacterAppliedBadStuffEventTests
    {
        [Theory, AutoGameData]
        public void CharacterAppliedBadStuffEvent_ShouldInvokeApplyBadStuffMethodForEachCard(
            MonsterCard firstMonster, MonsterCard secondMonster)
        {
            // Arrange
            var combatField = new CombatFieldBuilder()
                .WithMonsters(firstMonster, secondMonster)
                .Build();
            var character = new CharacterBuilder()
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var table = new TableBuilder()
                .WithCombatField(combatField)
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedBadStuffEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(firstMonster).Verify(m => m.ApplyBadStuff(It.IsAny<Game>(), It.IsAny<Character>()));
            Mock.Get(secondMonster).Verify(m => m.ApplyBadStuff(It.IsAny<Game>(), It.IsAny<Character>()));
        }

        [Theory, InlineData(-1), InlineData(0)]
        public void CharacterAppliedBadStuffEvent_ShouldSetLevelOneForCharacter_WhenItsLevelIsLessThanOne(
            int characterLevel)
        {
            // Assert
            var character = new CharacterBuilder()
                .WithLevel(characterLevel)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedBadStuffEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            character.Level.ShouldBe(1);
        }
    }
}
