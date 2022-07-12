using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;
using System.Security.Cryptography;

namespace Munchkin.Shared.Events
{
    public record PlayerRolledDieEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.DieValue = RandomNumberGenerator.GetInt32(1, 7);
        }
    }
}
