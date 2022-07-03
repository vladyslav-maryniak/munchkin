using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Tests.Builders
{
    public class CharacterBuilder
    {
        private readonly Character character = new();

        public CharacterBuilder WithLevel(int level)
        {
            character.Level = level;
            return this;
        }

        public CharacterBuilder WithCurses(params CurseCard[] curses)
        {
            character.Curses = new(curses);
            return this;
        }

        public Character Build() => character;
    }
}
