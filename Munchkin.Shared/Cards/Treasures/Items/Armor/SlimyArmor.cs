using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Armor
{
    public class SlimyArmor : ArmorCard
    {
        public override string Code => "slimy_armor";
        public override string Name => "Slimy Armor";
        public override int Bonus => 1;
        public override int GoldPieces => 200;
        public override string Description => string.Empty;
    }
}
