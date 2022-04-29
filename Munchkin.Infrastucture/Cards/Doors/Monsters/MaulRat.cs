using Munchkin.Infrastucture.Cards.Base;

namespace Munchkin.Infrastucture.Cards.Doors.Monsters
{
    public class MaulRat : MonsterCard
    {
        public override int Level => 1;
        public override string Name => "Maul Rat";
        public override string Description => "A create from Hell. +3 against Clerics.";
        public override string BadStuff => "She whacks you. Lose a level.";
        public override int VictoryLevels => 1;
    }
}
