using Munchkin.Shared.Models;

namespace Munchkin.Shared.Offers
{
    public class Trade : Offer
    {
        public Guid[] OfferorItemCardIds { get; set; }
        public Guid[] OffereeItemCardIds { get; set; }

        public Trade(
            Guid offerorId,
            Guid offereeId,
            Guid[] offerorItemCardIds,
            Guid[] offereeItemCardIds)
            : base(offerorId, offereeId)
        {
            OfferorItemCardIds = offerorItemCardIds;
            OffereeItemCardIds = offereeItemCardIds;
        }

        public override void Perform(Game game)
        {
            TransferEquipment(OfferorId, OffereeId, OfferorItemCardIds, game);
            TransferEquipment(OffereeId, OfferorId, OffereeItemCardIds, game);
        }
    }
}
