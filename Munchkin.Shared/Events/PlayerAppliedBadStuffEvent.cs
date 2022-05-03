using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record PlayerAppliedBadStuffEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Characters.First(x => x.Player.Id == PlayerId);
            game.Table.MonsterCards.ForEach(x => x.ApplyBadStuff(character));

            game.Table = new();
        }
    }
}
