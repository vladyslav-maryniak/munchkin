using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CurseCardDrewEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var table = game.Table;
            var card = (CurseCard)table.DoorDeck.Pop();
            var character = table.Places
                .First(x => x.Player.Id == PlayerId)
                .Character;

            character.Curses.Add(card);
            
            table.CombatField.CursePlace = card;
            table.CombatField.CharacterSquad.Add(character);
        }
    }
}
