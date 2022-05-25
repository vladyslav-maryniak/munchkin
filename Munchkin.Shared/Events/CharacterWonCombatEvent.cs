using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterWonCombatEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Table.Places
                    .First(x => x.Character.Id == CharacterId)
                    .Character;

            character.Level += game.Table.CombatField.MonsterSquad
                .Select(x => x.VictoryLevels)
                .Aggregate((result, x) => result + x);
        }
    }
}
