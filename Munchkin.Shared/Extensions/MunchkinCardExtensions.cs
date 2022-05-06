using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Extensions
{
    public static class MunchkinCardExtensions
    {
        public static IEnumerable<T> DrawCards<T>(this Stack<T> deck, int count)
            where T : MunchkinCard
        {
            List<T> cards = new();

            for (int i = 0; i < count; i++)
            {
                cards.Add(deck.Pop());
            }

            return cards;
        }
    }
}
