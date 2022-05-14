using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Models
{
    public class Table
    {
        public List<Place> Places { get; set; } = new();
        public Stack<DoorCard> DoorDeck { get; set; } = new();
        public Stack<TreasureCard> TreasureDeck { get; set; } = new();
        public CombatField CombatField { get; set; } = new();
        public int DieValue { get; set; } = 1;

        public Table(Stack<DoorCard> doorDeck, Stack<TreasureCard> treasureDeck)
        {
            DoorDeck = doorDeck;
            TreasureDeck = treasureDeck;
        }
    }
}
