using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Armor
{
    public class LeatherArmor : ArmorCard
    {
        public override string Code => "leather_armor";
        public override string Name => "Leather Armor";
        public override int Bonus => 1;
        public override int GoldPieces => 200;
        public override string Description => string.Empty;
    }
}
