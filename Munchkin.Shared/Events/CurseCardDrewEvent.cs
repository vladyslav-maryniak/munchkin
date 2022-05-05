using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CurseCardDrewEvent(Guid GameId, Guid PlayerId, CurseCard Card) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Table.Places
                .First(x => x.Player.Id == PlayerId)
                .Character;

            Card.Apply(character);
        }
    }
}
