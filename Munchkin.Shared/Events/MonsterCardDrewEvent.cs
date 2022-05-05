using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record MonsterCardDrewEvent(Guid GameId, Guid PlayerId, MonsterCard Card) : IGameEvent
    {
        public void Apply(Game game)
        {
            game.Table.CombatField.MonsterSquad.Add(Card);
        }
    }
}
