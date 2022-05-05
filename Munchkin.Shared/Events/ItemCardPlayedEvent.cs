using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record ItemCardPlayedEvent(Guid GameId, Guid CharacterId, Guid CardId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Character.Id == CharacterId);
            var card = (ItemCard)place.InHandCards
                .First(x => x.Id == CardId);

            if (card.TryEquip(place.Character))
            {
                place.InHandCards.Remove(card);
            }
        }
    }
}
