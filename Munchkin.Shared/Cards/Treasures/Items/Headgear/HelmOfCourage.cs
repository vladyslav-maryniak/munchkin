using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Headgear
{
    public class HelmOfCourage : HeadgearCard
    {
        public override string Code => "helm_of_courage";
        public override int Bonus => 1;
        public override string Name => "Helm of courage";
        public override string Description => string.Empty;
        public override int GoldPieces => 200;
    }
}
