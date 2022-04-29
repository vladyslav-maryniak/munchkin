using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record PlayerRolledDieEvent(Guid GameId, Guid PlayerId, int DieValue) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.DieValue = DieValue;
        }
    }
}
