using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Extensions;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Models
{
    public class Table
    {
        public List<Place> Places { get; set; } = new();
        public List<Offer> Offers { get; set; } = new();
        public Stack<DoorCard> DoorDeck { get; set; }
        public Stack<TreasureCard> TreasureDeck { get; set; }
        public Stack<DoorCard> DoorDiscards { get; set; } = new();
        public Stack<TreasureCard> TreasureDiscards { get; set; } = new();
        public CombatField CombatField { get; set; } = new();
        public int DieValue { get; set; } = 1;

        public Table(Stack<DoorCard> doorDeck, Stack<TreasureCard> treasureDeck)
        {
            DoorDeck = doorDeck;
            TreasureDeck = treasureDeck;
        }

        public DoorCard DrawDoorCard(out bool shuffled)
            => DrawCard(DoorDeck, DoorDiscards, out shuffled);
        public DoorCard PeekDoorCard(out bool shuffled)
            => PeekCard(DoorDeck, DoorDiscards, out shuffled);

        public TreasureCard DrawTreasureCard(out bool shuffled)
            => DrawCard(TreasureDeck, TreasureDiscards, out shuffled);
        public TreasureCard PeekTreasureCard(out bool shuffled)
            => PeekCard(TreasureDeck, TreasureDiscards, out shuffled);

        private static T DrawCard<T>(Stack<T> deck, Stack<T> discards, out bool shuffled)
            where T : MunchkinCard => CheckDeck(deck, discards, deck.Pop, out shuffled);

        private static T PeekCard<T>(Stack<T> deck, Stack<T> discards, out bool shuffled)
            where T : MunchkinCard => CheckDeck(deck, discards, deck.Peek, out shuffled);

        private static T CheckDeck<T>(
            Stack<T> deck, Stack<T> discards, Func<T> deckAction, out bool shuffled)
            where T : MunchkinCard
        {
            shuffled = false;
            if (deck.TryPeek(out _) == false)
            {
                var cards = discards.Shuffle();
                foreach (var card in cards)
                {
                    deck.Push(card);
                }
                discards.Clear();
                shuffled = true;
            }
            return deckAction();
        }

        public void Discard(params MunchkinCard[] cards)
        {
            foreach (var card in cards)
            {
                switch (card)
                {
                    case DoorCard d:
                        DoorDiscards.Push(d);
                        break;
                    case TreasureCard t:
                        TreasureDiscards.Push(t);
                        break;
                }
            }
        }
    }
}
