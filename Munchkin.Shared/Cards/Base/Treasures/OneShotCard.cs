using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures
{
    public abstract class OneShotCard : TreasureCard
    {
        public abstract int GoldPieces { get; }

        public abstract bool TryUse(Table table, Dictionary<string, string> metaData);
    }
}
