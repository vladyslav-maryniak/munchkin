using Munchkin.Shared.Models;

namespace Munchkin.Shared.Offers
{
    public class Reward : Offer
    {
        public Guid[] ItemCardIds { get; set; } = Array.Empty<Guid>();
        public Guid[] CardIdsForPlay { get; set; } = Array.Empty<Guid>();
        public int VictoryTreasures { get; set; }
        public int NumberOfTreasures { get; set; }
        public bool HelperPicksFirst { get; set; }

        public Reward()
        {
        }

        public Reward(
            Guid offerorId,
            Guid[] itemCardIds,
            Guid[] cardIdsForPlay,
            int victoryTreasures,
            int numberOfTreasures,
            bool helperPicksFirst)
            : base(offerorId, Guid.Empty)
        {
            ItemCardIds = itemCardIds;
            CardIdsForPlay = cardIdsForPlay;
            VictoryTreasures = victoryTreasures;
            NumberOfTreasures = numberOfTreasures;
            HelperPicksFirst = helperPicksFirst;
        }

        public override void Perform(Game game)
        {
            TransferCards(OfferorId, OffereeId, ItemCardIds.Concat(CardIdsForPlay).ToArray(), game);
            TransferTreasures(OfferorId, OffereeId, VictoryTreasures, NumberOfTreasures, HelperPicksFirst, game);
        }
    }
}
