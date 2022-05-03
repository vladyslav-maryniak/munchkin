using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record CharacterWonCombatEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Characters.First(x => x.Id == CharacterId);
            character.Level += game.Table.MonsterCards
                .Select(x => x.VictoryLevels)
                .Aggregate((result, x) => result + x);

            game.Table = new();
        }
    }
}
