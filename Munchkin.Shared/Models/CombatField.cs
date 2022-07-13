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

        public int VictoryLevels
            => MonsterSquad.Select(x => x.VictoryLevels)
                           .Aggregate((result, x) => result + x);

        public int VictoryTreasures
        {
            get
            {
                var monsterTreasures = MonsterSquad
                    .Select(x => x.Treasures);
                var enhancerTreasures = MonsterEnhancers
                    .SelectMany(x => x.Value)
                    .Select(x => x.Treasures);

                return monsterTreasures.Concat(enhancerTreasures)
                                       .Aggregate((result, x) => result + x);
            }
        }

        public int MonsterSquadStrength
        {
            get
            {
                var levels = MonsterSquad.Select(x => x.Level);
                var enhancers = MonsterEnhancers
                    .SelectMany(x => x.Value)
                    .Select(x => x.Bonus);

                return levels.Concat(enhancers)
                             .Aggregate((result, x) => result + x);
            }
        }

        public int CharacterSquadStrength
        {
            get
            {
                var levels = CharacterSquad.Select(x => x.Level);
                var bonuses = CharacterSquad.Select(x => x.Equipment.Bonus);

                return levels.Concat(bonuses)
                             .Aggregate((result, x) => result + x);
            }
        }

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
