using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record PlayerJoinedEvent(Guid GameId, Player Player) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = new Character(Player);
            game.Characters.Add(character);
        }
    }
}
