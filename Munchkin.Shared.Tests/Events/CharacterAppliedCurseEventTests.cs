using Moq;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CharacterAppliedCurseEventTests
    {

        [Theory, AutoGameData]
        public void CharacterAppliedCurseEvent_ShouldInvokeApplyMethod(
            CurseCard curseCard)
        {
            // Arrange
            var character = new CharacterBuilder()
                .WithCurses(curseCard)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithCurse(curseCard)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedCurseEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            Mock.Get(curseCard).Verify(m => m.Apply(It.IsAny<Game>(), It.IsAny<Character>()));
        }

        [Theory, AutoGameData]
        public void CharacterAppliedCurseEvent_ShouldRemoveCurseFromCharacter(
            CurseCard curseCard)
        {
            // Arrange
            var character = new CharacterBuilder()
                .WithCurses(curseCard)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithCurse(curseCard)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedCurseEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            character.Curses.ShouldNotContain(curseCard);
        }

        [Theory, AutoGameData]
        public void CharacterAppliedCurseEvent_ShouldClearCombatField(
            CurseCard curseCard)
        {
            // Arrange
            var character = new CharacterBuilder()
                .WithCurses(curseCard)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithCharacters(character)
                .WithCurse(curseCard)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedCurseEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            game.ShouldSatisfyAllConditions(
                x => x.Table.CombatField.CharacterSquad.Count.ShouldBe(0),
                x => x.Table.CombatField.CursePlace.ShouldBeNull()
            );
        }

        [Theory, AutoGameData]
        public void CharacterAppliedCurseEvent_ShouldSetLevelOneForCharacter_WhenItsLevelIsLessThanOne(
            CurseCard curseCard)
        {
            // Assert
            var character = new CharacterBuilder()
                .WithLevel(-1)
                .WithCurses(curseCard)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithCurse(curseCard)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterAppliedCurseEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            character.Level.ShouldBe(1);
        }

        [Theory, AutoGameData]
        public void CharacterAppliedCurseEvent_ShouldIncreaseTurnIndexByOne(
            int turnIndex, CurseCard curseCard)
        {
            // Assert
            var character = new CharacterBuilder()
                .WithCurses(curseCard)
                .Build();
            var place = new PlaceBuilder()
                .WithCharacter(character)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithCurse(curseCard)
                .Build();
            var table = new TableBuilder()
                .WithPlaces(place)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .WithTurnIndex(turnIndex)
                .Build();

            // Act
            CharacterAppliedCurseEvent @event = new(game.Id, character.Id);
            @event.Apply(game);

            // Assert
            int difference = game.TurnIndex - turnIndex;
            difference.ShouldBe(1);
        }
    }
}
