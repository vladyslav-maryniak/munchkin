using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
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
