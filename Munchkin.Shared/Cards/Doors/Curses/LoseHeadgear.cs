using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class LoseHeadgear : CurseCard
    {
        public override string Code => "lose_headgear";
        public override string Name => "Curse!";
        public override string Description => "Lose the headgear you are wearing.";

        public override void Apply(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.TakeOff(equipment.Headgear);
            game.Table.Discard(item);
        }
    }
}
