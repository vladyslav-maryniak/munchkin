using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class DuckOfDoom : CurseCard
    {
        public override Guid Id { get; set; }
        public override string Name => "Duck of doom";
        public override string Description => "You should know better than to pick up a duck in a dungeon. Lose 2 levels.";

        public DuckOfDoom()
        {
            Id = Guid.NewGuid();
        }

        public override void Apply(Character character)
        {
            character.Level -= 2;
        }
    }
}
