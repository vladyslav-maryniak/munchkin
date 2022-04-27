using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record PlayerJoinedEvent(Guid GameId, Player Player) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.JoinPlayer(Player);
        }
    }
}
