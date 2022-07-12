using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerLootedRoomEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places.First(x => x.Player.Id == PlayerId);

            var card = game.Table.DrawDoorCard(out bool _);
            place.InHandCards.Add(card);

            game.TurnIndex++;
        }
    }
}
