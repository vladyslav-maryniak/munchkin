using Munchkin.Infrastucture.Events.Base;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Events
{
    public record PlayerWonCombatEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Characters.First(x => x.Player.Id == PlayerId);
            character.Level += game.Table.MonsterCards
                .Select(x => x.VictoryLevels)
                .Aggregate((result, x) => result + x);
            
            game.Table = new();
        }
    }
}
