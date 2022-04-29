using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Cards.Treasures;

namespace Munchkin.Infrastucture.Projections
{
    public class Game
    {
        public Guid Id { get; set; }
        public List<Character> Characters { get; set; } = new();
        public Stack<DoorCard> DoorDeck { get; set; } = new();
        public Stack<TreasureCard> TreasureDeck { get; set; } = new();
        public Table Table { get; set; } = new();
    }
}
