using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerJoinedEvent(Guid GameId, Player Player) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Lobby.Add(Player);
        }
    }
}
