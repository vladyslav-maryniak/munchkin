using Munchkin.Shared.Cards.Base.Doors;

namespace Munchkin.Shared.Cards.Doors.MonsterEnhancers
{
    public class Baby : MonsterEnhancerCard
    {
        public override string Code => "baby";
        public override string Name => "Baby";
        public override string Description => "Play during combat. If the monster is defeated, draw 1 fewer Treasure (minimun of 1).";
        public override int Bonus => -5;
        public override int Treasures => -1;
    }
}
