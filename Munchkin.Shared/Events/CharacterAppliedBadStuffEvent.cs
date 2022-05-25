using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterAppliedBadStuffEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Table.Places
                .First(x => x.Character.Id == CharacterId)
                .Character;

            game.Table.CombatField.MonsterSquad.ForEach(x => x.ApplyBadStuff(character));

            if (character.Level < 1)
            {
                character.Level = 1;
            }
        }
    }
}
