using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class Crabs : MonsterCard
    {
        public override string Code => "crabs";
        public override int Level => 1;
        public override string Name => "Crabs";
        public override string Description => "Cannot be Outrun!";
        public override string BadStuff => "Discard armor and all items worn below the waist.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
            var equipment = character.Equipment;
            var items = equipment.TakeOff(equipment.Armor, equipment.Footgear);
            game.Table.Discard(items);
        }
    }
}
