using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record PlayerRolledDieEvent(Guid GameId, Guid PlayerId, int DieValue) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.DieValue = DieValue;
        }
    }
}
