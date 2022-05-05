using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures
{
    public abstract class ItemCard : TreasureCard
    {
        public abstract int Bonus { get; }
        public abstract int GoldPieces { get; }

        public abstract bool TryEquip(Character character);
    }
}
