using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Events
{
    public record PlayerOfferedBribeEvent(
        Guid GameId,
        Guid OfferorId,
        Guid OffereeId,
        string Agreement,
        Guid[] ItemCardIds) : IGameEvent
    {
        public void Apply(Game game)
        {   
            Bribe offer = new(OfferorId, OffereeId, Agreement, ItemCardIds);

            game.Table.Offers.Add(offer);
        }
    }
}
