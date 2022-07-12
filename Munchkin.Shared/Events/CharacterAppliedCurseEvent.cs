using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterAppliedCurseEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var table = game.Table;
            var combatField = table.CombatField;
            var character = table.Places
                .First(x => x.Character.Id == CharacterId)
                .Character;
            var curse = character.Curses
                .First(x => x.Id == combatField.CursePlace!.Id);

            curse.Apply(game, character);
            character.Curses.Remove(curse);

            if (character.Level < 1)
            {
                character.Level = 1;
            }

            var cards = combatField.Clear();
            table.Discard(cards.ToArray());
            game.TurnIndex++;
        }
    }
}
