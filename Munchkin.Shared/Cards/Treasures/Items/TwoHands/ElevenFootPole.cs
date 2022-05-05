using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.TwoHands
{
    public class ElevenFootPole : TwoHandsCard
    {
        public override Guid Id { get; set; }
        public override int Bonus => 1;
        public override int GoldPieces => 200;
        public override string Name => "Eleven-foot pole";
        public override string Description => string.Empty;

        public ElevenFootPole()
        {
            Id = Guid.NewGuid();
        }
    }
}
