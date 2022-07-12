using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class LoseLevelLightning : CurseCard
    {
        public override string Code => "lose_level_lightning";
        public override string Name => "Curse!";
        public override string Description => "Lose a level.";

        public override void Apply(Game game, Character character)
        {
            character.Level--;
        }
    }
}
