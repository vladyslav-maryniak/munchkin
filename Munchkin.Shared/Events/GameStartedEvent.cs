using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Extensions;
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
                place.InHandCards.AddRange(game.Table.DoorDeck.DrawCards(count: 4));
                place.InHandCards.AddRange(game.Table.TreasureDeck.DrawCards(count: 4));

                game.Table.Places.Add(place);
            }

            game.Lobby.Clear();
        }
    }
}
