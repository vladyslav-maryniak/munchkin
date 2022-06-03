using Munchkin.Shared.Cards.Base.Doors;

namespace Munchkin.Shared.Models
{
    public class CombatField
    {
        public List<MonsterCard> MonsterSquad { get; set; } = new();
        public List<Character> CharacterSquad { get; set; } = new();
        public CurseCard? CursePlace { get; set; }

        public void Clear()
        {
            MonsterSquad.Clear();
            CharacterSquad.Clear();
            CursePlace = null;
        }
    }
}
