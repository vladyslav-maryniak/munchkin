using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Models
{
    public class CombatField
    {
        public List<MonsterCard> MonsterSquad { get; set; } = new();
        public Dictionary<Guid, List<MonsterEnhancerCard>> MonsterEnhancers { get; set; } = new();
        public List<Character> CharacterSquad { get; set; } = new();
        public Reward? Reward { get; set; }
        public CurseCard? CursePlace { get; set; }


        public IEnumerable<MunchkinCard> Clear()
        {
            List<MunchkinCard> discards = new();

            if (MonsterSquad.Any())
            {
                discards.AddRange(MonsterSquad);
                MonsterSquad.Clear();
            }
            if (MonsterEnhancers.Any())
            {
                var cards = MonsterEnhancers.SelectMany(x => x.Value);
                discards.AddRange(cards);
                MonsterEnhancers.Clear();
            }
            if (CursePlace is not null)
            {
                discards.Add(CursePlace);
                CursePlace = null;
            }

            CharacterSquad.Clear();
            Reward = null;

            return discards;
        }
    }
}
