using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record PlayerRanAwayEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
        }
    }
}
