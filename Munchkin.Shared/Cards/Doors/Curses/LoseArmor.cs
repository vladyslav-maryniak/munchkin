using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class LoseArmor : CurseCard
    {
        public override string Code => "lose_armor";
        public override string Name => "Curse!";
        public override string Description => "Lose the armor you are wearing.";

        public override void Apply(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.TakeOff(equipment.Armor);
            game.Table.Discard(item);
        }
    }
}
