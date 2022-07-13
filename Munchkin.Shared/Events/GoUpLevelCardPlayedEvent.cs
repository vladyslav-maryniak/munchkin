using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record GoUpLevelCardPlayedEvent(Guid GameId, Guid PlayerId, Guid CardId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Player.Id == PlayerId);
            var card = (GoUpLevelCard)place.InHandCards
                .First(x => x.Id == CardId);

            if (card.TryRiseLevel(place.Character))
            {
                game.Table.Discard(card);
                place.InHandCards.Remove(card);
            }
        }
    }
}
