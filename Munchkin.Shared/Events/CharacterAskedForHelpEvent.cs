using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterAskedForHelpEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Table.Places
                .First(x => x.Character.Id == CharacterId)
                .Character;

            game.Table.CombatField.CharacterSquad.Add(character);
        }
    }
}
