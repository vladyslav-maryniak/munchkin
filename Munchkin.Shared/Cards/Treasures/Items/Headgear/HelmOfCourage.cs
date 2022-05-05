using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Headgear
{
    public class HelmOfCourage : HeadgearCard
    {
        public override Guid Id { get; set; }
        public override int Bonus => 1;
        public override string Name => "Helm of courage";
        public override string Description => string.Empty;
        public override int GoldPieces => 200;

        public HelmOfCourage()
        {
            Id = Guid.NewGuid();
        }
    }
}
