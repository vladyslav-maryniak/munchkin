using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Models
{
    public class CombatField
    {
        public List<MonsterCard> MonsterSquad { get; set; } = new();
        public List<Character> CharacterSquad { get; set; } = new();
        public Reward? Reward { get; set; }
        public CurseCard? CursePlace { get; set; }

        public void Clear()
        {
            MonsterSquad.Clear();
            CharacterSquad.Clear();
            Reward = null;
            CursePlace = null;
        }
    }
}
