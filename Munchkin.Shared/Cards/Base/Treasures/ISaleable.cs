namespace Munchkin.Shared.Cards.Base.Treasures
{
    public interface ISaleable
    {
        public Guid Id { get; set; }
        int GoldPieces { get; }
    }
}