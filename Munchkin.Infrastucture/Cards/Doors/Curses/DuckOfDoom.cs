using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Cards.Doors.Curses
{
    public class DuckOfDoom : CurseCard
    {
        public override string Name => "Duck of doom";
        public override string Description => "You should know better than to pick up a duck in a dungeon. Lose 2 levels.";

        public override void Apply(Character character)
        {
            character.Level -= 2;
        }
    }
}
