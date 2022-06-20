using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerDeclinedOfferEvent(Guid GameId, Guid OfferId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var offer = game.Table.Offers.First(x => x.Id == OfferId);
            game.Table.Offers.Remove(offer);
        }
    }
}
