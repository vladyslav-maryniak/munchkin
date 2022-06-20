using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Events
{
    public record PlayerOfferedTradeEvent(
        Guid GameId,
        Guid OfferorId,
        Guid OffereeId,
        Guid[] OfferorItemCardIds,
        Guid[] OffereeItemCardIds) : IGameEvent
    {
        public void Apply(Game game)
        {
            Trade offer = new(OfferorId, OffereeId, OfferorItemCardIds, OffereeItemCardIds);

            game.Table.Offers.Add(offer);
        }
    }
}
