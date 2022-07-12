using Munchkin.Shared.Cards.Base.Doors;

namespace Munchkin.Shared.Cards.Doors.MonsterEnhancers
{
    public class Enraged : MonsterEnhancerCard
    {
        public override string Code => "enraged";
        public override string Name => "Enraged";
        public override string Description => "Play during combat. If the monster is defeated, draw one extra Treasure.";
        public override int Bonus => 5;
        public override int Treasures => 1;
    }
}
