using Munchkin.Shared.Models;

namespace Munchkin.Shared.Offers
{
    public class Bribe : Offer
    {
        public string Agreement { get; set; }
        public Guid[] ItemCardIds { get; set; }

        public Bribe(
            Guid offerorId,
            Guid offereeId,
            string agreement,
            Guid[] itemCardIds)
            : base(offerorId, offereeId)
        {
            Agreement = agreement;
            ItemCardIds = itemCardIds;
        }

        public override void Perform(Game game)
        {
            TransferCards(OfferorId, OffereeId, ItemCardIds, game);
        }
    }
}
