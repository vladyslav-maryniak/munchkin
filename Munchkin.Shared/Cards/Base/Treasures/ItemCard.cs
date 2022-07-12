using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures
{
    public abstract class ItemCard : SaleableCard
    {
        public abstract int Bonus { get; }

        public abstract bool TryEquip(Character character);
    }
}
