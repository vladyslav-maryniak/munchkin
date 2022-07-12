using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerSoldCardsEvent(Guid GameId, Guid PlayerId, IEnumerable<Guid> CardIds) : IGameEvent
    {
        const int maxLevel = 9; // 10th level must be reached in combat

        public static int MinAmountOfGoldPiecesForSale => 1000;

        public void Apply(Game game)
        {
            var place = game.Table.Places.First(x => x.Player.Id == PlayerId);
            var cardsForSale = place.InHandCards
                .Where(x => CardIds.Contains(x.Id))
                .Cast<SaleableCard>();

            var goldPieces = cardsForSale
                .Select(x => x.GoldPieces)
                .Aggregate((total, x) => total + x);

            int upLevels = goldPieces / MinAmountOfGoldPiecesForSale;
            if (upLevels > 0 && place.Character.Level + upLevels <= maxLevel)
            {
                place.Character.Level += upLevels;
                game.Table.Discard(cardsForSale.ToArray());
                place.InHandCards.RemoveAll(x => cardsForSale.Contains(x));
            }
        }
    }
}
