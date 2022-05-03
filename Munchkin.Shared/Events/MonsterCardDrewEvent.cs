using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record MonsterCardDrewEvent(Guid GameId, Guid PlayerId, MonsterCard Card) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.MonsterCards.Add(Card);
        }
    }
}
