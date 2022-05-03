using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record CurseCardDrewEvent(Guid GameId, Guid PlayerId, CurseCard Card) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Characters.First(x => x.Player.Id == PlayerId);
            Card.Apply(character);
        }
    }
}
