using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class DroolingSlime : MonsterCard
    {
        public override string Code => "drooling_slime";
        public override int Level => 1;
        public override string Name => "Drooling Slime";
        public override string Description => "Yucky slime! +4 against Elves.";
        public override string BadStuff => "Discard the Footgear you are wearing. Lose a level if you have no Footgear.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
            var equipment = character.Equipment;
            if (equipment.Footgear is null)
            {
                character.Level--;
            }
            else
            {
                var item = equipment.TakeOff(equipment.Footgear);
                game.Table.Discard(item);
            }
        }
    }
}
