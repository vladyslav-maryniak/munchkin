using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Curses
{
    public class TrulyObnoxiousCurse : CurseCard
    {
        public override string Code => "truly_obnoxious_curse";
        public override string Name => "Truly Obnoxious Curse!";
        public override string Description => "Lose the item that gives you the biggest bonus.";

        public override void Apply(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.ToEnumerable()
                                .MaxBy(x => x.Bonus);
            
            var takenOffItem = equipment.TakeOff(item);
            game.Table.Discard(takenOffItem);
        }
    }
}
