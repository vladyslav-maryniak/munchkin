using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record MonsterCardPlayedEvent(Guid GameId, Guid PlayerId, Guid CardId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places.First(x => x.Player.Id == PlayerId);
            var card = (MonsterCard)place.InHandCards.First(x => x.Id == CardId);

            game.Table.CombatField.CharacterSquad.Add(place.Character);
            game.Table.CombatField.MonsterSquad.Add(card);

            place.InHandCards.Remove(card);
        }
    }
}
