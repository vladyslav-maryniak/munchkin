using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
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
