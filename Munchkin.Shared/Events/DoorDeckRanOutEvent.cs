using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record DoorDeckRanOutEvent(Guid GameId, List<Guid> Deck) : IGameEvent
    {
        public void Apply(Game game)
        {
            Deck.Reverse();
            foreach (var cardId in Deck)
            {
                var card = game.Table.DoorDiscards.First(x => x.Id == cardId);
                game.Table.DoorDeck.Push(card);
            }
            game.Table.DoorDiscards.Clear();
        }
    }
}
