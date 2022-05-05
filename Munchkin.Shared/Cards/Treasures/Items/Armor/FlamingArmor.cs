using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Armor
{
    public class FlamingArmor : ArmorCard
    {
        public override Guid Id { get; set; }
        public override int Bonus => 2;
        public override string Name => "Flaming armor";
        public override string Description => string.Empty;
        public override int GoldPieces => 400;

        public FlamingArmor()
        {
            Id = Guid.NewGuid();
        }
    }
}
