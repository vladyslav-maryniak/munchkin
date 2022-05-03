using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Projections
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
