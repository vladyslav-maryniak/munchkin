using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record CharacterEscapedEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table = new();
        }
    }
}
