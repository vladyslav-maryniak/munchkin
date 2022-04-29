using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record MonsterCardDrewEvent(Guid GameId, Guid PlayerId, MonsterCard Card) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.MonsterCards.Add(Card);
        }
    }
}
