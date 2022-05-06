using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record OneShotCardPlayedEvent(
        Guid GameId, Guid PlayerId, Guid CardId, Dictionary<string, string> MetaData) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Player.Id == PlayerId);
            var card = (OneShotCard)place.InHandCards
                .First(x => x.Id == CardId);

            if (card.TryUse(game.Table, MetaData))
            {
                place.InHandCards.Remove(card);
            }
        }
    }
}
