using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record GameStartedEvent(Guid GameId) : IGameEvent
    {
        private const int cardsPerPlayer = 4;

        public void Apply(Game game)
        {
            foreach (var player in game.Lobby.Players)
            {
                var place = new Place(player, new Character());
                game.Table.Places.Add(place);
            }

            foreach (var place in game.Table.Places)
            {
                for (int i = 0; i < cardsPerPlayer; i++)
                {
                    place.InHandCards.Add(game.Table.DrawDoorCard(out bool _));
                    place.InHandCards.Add(game.Table.DrawTreasureCard(out bool _));
                }
            }

            game.Lobby.Players.Clear();
            game.Status = GameStatus.Started;
        }
    }
}
