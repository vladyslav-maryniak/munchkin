using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.Footgear
{
    public class BootsOfButtKicking : FootgearCard
    {
        public override Guid Id { get; set; }
        public override int Bonus => 2;
        public override string Name => "Boots of butt-kicking";
        public override string Description => string.Empty;
        public override int GoldPieces => 400;

        public BootsOfButtKicking()
        {
            Id = Guid.NewGuid();
        }
    }
}
