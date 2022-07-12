using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record MonsterEnhancerCardPlayedEvent(
        Guid GameId, Guid PlayerId, Guid CardId, Dictionary<string, string>? Metadata = default) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Player.Id == PlayerId);
            var card = (MonsterEnhancerCard)place.InHandCards
                .First(x => x.Id == CardId);
            var combatField = game.Table.CombatField;
            
            if (Metadata is not null
                && Metadata.TryGetValue("monsterCardId", out string? monsterCardIdMeta)
                && monsterCardIdMeta is not null
                && Guid.TryParse(monsterCardIdMeta, out Guid monsterCardId))
            {
                if (combatField.MonsterEnhancers.ContainsKey(monsterCardId) == false)
                {
                    combatField.MonsterEnhancers.Add(monsterCardId, new List<MonsterEnhancerCard>());
                }
                combatField.MonsterEnhancers[monsterCardId].Add(card);
                place.InHandCards.Remove(card);
            }
        }
    }
}
