using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures
{
    public abstract class OneShotCard : SaleableCard
    {
        public abstract bool TryUse(Table table, Dictionary<string, string>? metadata = default);
    }
}
