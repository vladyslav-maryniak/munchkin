using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class MaulRat : MonsterCard
    {
        public override string Code => "maul_rat";
        public override int Level => 1;
        public override string Name => "Maul Rat";
        public override string Description => "A create from Hell. +3 against Clerics.";
        public override string BadStuff => "She whacks you. Lose a level.";
        public override int VictoryLevels => 1;
        public override int Treasures => 1;

        public override void ApplyBadStuff(Game game, Character character)
        {
            character.Level--;
        }
    }
}
