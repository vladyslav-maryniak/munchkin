using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class LameGoblin : MonsterCard
    {
        public override string Code => "lame_goblin";
        public override int Level => 1;
        public override string Name => "Lame Goblin";
        public override string Description => "+1 to Run Away.";
        public override string BadStuff => "He whacks you with his crutch. Lose a level.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
            character.Level--;
        }
    }
}
