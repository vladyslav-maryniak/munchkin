using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class PottedPlant : MonsterCard
    {
        public override string Code => "potted_plant";
        public override int Level => 1;
        public override string Name => "Potted Plant";
        public override string Description => "Elves draw an extra Treasure after defeating it.";
        public override string BadStuff => "None. Escape is automatic";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
        }
    }
}
