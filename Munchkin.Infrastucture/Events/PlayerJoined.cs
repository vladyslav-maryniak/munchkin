using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record PlayerJoined(Guid GameId, Player Player) : IEvent
    {
        public void Apply(Game game)
        {
            game.JoinPlayer(Player);
        }
    }
}
