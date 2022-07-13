using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class LoseFootgear : CurseCard
    {
        public override string Code => "lose_footgear";
        public override string Name => "Curse!";
        public override string Description => "Lose the footgear you are wearing.";

        public override void Apply(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.TakeOff(equipment.Footgear);
            game.Table.Discard(item);
        }
    }
}
