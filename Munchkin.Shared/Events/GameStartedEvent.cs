using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record GameStartedEvent(Guid GameId) : IGameEvent
    {
        public void Apply(Game game)
        {
            foreach (var player in game.Lobby)
            {
                var place = new Place(player, new Character());
                place.InHandCards.AddRange(DrawCards(game.Table.DoorDeck, 4));
                place.InHandCards.AddRange(DrawCards(game.Table.TreasureDeck, 4));

                game.Table.Places.Add(place);
            }

            game.Lobby.Clear();
        }

        private IEnumerable<T> DrawCards<T>(Stack<T> deck, int count)
        {
            List<T> drawnCards = new();

            for (int i = 0; i < count; i++)
            {
                drawnCards.Add(deck.Pop());
            }

            return drawnCards.AsEnumerable();
        }
    }
}
