using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterGotHelpEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var combatField = game.Table.CombatField;
            var place = game.Table.Places
                .First(x => x.Character.Id == CharacterId);

            combatField.CharacterSquad.Add(place.Character);

            if (combatField.Reward is not null)
            {
                combatField.Reward.OffereeId = place.Player.Id;
            }
        }
    }
}
