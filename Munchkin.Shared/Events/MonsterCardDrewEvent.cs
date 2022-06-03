using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record MonsterCardDrewEvent(Guid GameId, Guid PlayerId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Player.Id == PlayerId);
            var card = (MonsterCard)game.Table.DoorDeck.Pop();
            
            game.Table.CombatField.CharacterSquad.Add(place.Character);
            game.Table.CombatField.MonsterSquad.Add(card);
        }
    }
}
