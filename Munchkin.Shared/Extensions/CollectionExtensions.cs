using System.Security.Cryptography;

namespace Munchkin.Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToList();
            for (int i = 0; i < collection.Count(); i++)
            {
                int j = RandomNumberGenerator.GetInt32(collection.Count());
                (list[j], list[i]) = (list[i], list[j]);
            }
            return list;
        }
    }
}
