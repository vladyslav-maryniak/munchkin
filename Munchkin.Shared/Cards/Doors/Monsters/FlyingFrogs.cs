using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class FlyingFrogs : MonsterCard
    {
        public override int Level => 2;
        public override string Name => "Flying Frogs";
        public override string Description => "-1 to Run Away";
        public override string BadStuff => "They bite! Lose 2 levels.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Character character)
        {
            character.Level -= 2;
        }
    }
}
