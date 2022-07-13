using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class MrBones : MonsterCard
    {
        public override string Code => "mr_bones";
        public override int Level => 2;
        public override string Name => "Mr. Bones";
        public override string Description => "If you must flee, you lose a leven if you escape.";
        public override string BadStuff => "His bony touch costs you 2 levels.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
            character.Level -= 2;
        }
    }
}
