using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CharacterAppliedCurseEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var table = game.Table;
            var character = table.Places
                .First(x => x.Character.Id == CharacterId)
                .Character;
            var curse = character.Curses
                .First(x => x.Id == table.CombatField.CursePlace!.Id);

            curse.Apply(game, character);
            character.Curses.Remove(curse);

            table.CombatField.Clear();

            if (character.Level < 1)
            {
                character.Level = 1;
            }

            game.TurnIndex++;
        }
    }
}
