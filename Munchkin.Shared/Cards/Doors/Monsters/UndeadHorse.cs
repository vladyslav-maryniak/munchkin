using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class UndeadHorse : MonsterCard
    {
        public override int Level => 4;
        public override string Name => "Undead Horse";
        public override string Description => "+5 against Dwarves.";
        public override string BadStuff => "Kicks, bites, and smells awful. Lose 2 levels.";
        public override int VictoryLevels => 1;

        public override void ApplyBadStuff(Character character)
        {
            character.Level -= 2;
        }
    }
}
