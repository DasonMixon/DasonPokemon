using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class PackExtensions
    {
        private static readonly Random random = new Random();

        public static bool RollBetweenOneAnd(int limit)
        {
            var num = random.Next(1, limit - 1);
            return num == 1;
        }

        public static List<T> GetRandom<T>(this IEnumerable<T> list, int count, Func<T, bool> where = null)
        {
            if (count <= 0)
                return new List<T>();

            // Make a copy of the list so that we can modify as we go
            var items = where == null ? new List<T>(list) : new List<T>(list.Where(where));

            var results = new List<T>();
            for (int i = 0; i < count; i++)
            {
                var pick = items.OrderBy(x => random.Next(items.Count - 1)).First();
                results.Add(pick);
                items.Remove(pick);
            }

            return results;
        }
    }
}
