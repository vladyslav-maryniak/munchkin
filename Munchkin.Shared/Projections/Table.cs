using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Projections
{
    public class Table
    {
        public List<MonsterCard> MonsterCards { get; set; } = new();
        public List<Character> CharacterSquad { get; set; } = new();
        public int DieValue { get; set; } = 1;
    }
}
