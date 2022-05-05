using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerLeftEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places.First(x => x.Player.Id == PlayerId);
            
            game.Table.Places.Remove(place);
        }
    }
}
