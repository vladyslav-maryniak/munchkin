using Munchkin.Shared.Cards.Base.Doors;

namespace Munchkin.Shared.Cards.Doors.MonsterEnhancers
{
    public class Ancient : MonsterEnhancerCard
    {
        public override string Code => "ancient";
        public override string Name => "Ancient";
        public override string Description => "Play during combat. If the monster is defeated, draw two extra Treasures.";
        public override int Bonus => 10;
        public override int Treasures => 2;
    }
}
