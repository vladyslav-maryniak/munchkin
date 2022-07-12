using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class Bigfoot : MonsterCard
    {
        public override string Code => "bigfoot";
        public override int Level => 12;
        public override string Name => "Bigfoot";
        public override string Description => "+3 against Dwarves and Halflings.";
        public override string BadStuff => "Stomps you flat and eats your hat. Lose the Headgear you were wearing.";
        public override int VictoryLevels => 1;
        public override int Treasures => 3;

        public override void ApplyBadStuff(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.TakeOff(equipment.Headgear);
            game.Table.Discard(item);
        }
    }
}
