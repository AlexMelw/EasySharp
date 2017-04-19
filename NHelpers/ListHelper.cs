using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class ListHelper
    {
        public static void AddIfNotContains<T>(this IList<T> source, T value)
        {
            if (!source.Contains(value)) source.Add(value);
        }
        public static void Shuffle<T>(this IList<T> source)
        {
            Random rnd = new Random();

            for (int i = source.Count - 1; i > 0; i--)
                Swap(source[i], source[rnd.Next(0, i)]);
        }

        private static void Swap<T>(T lhs, T rhs)
        {
            var aux = lhs;
            lhs = rhs;
            rhs = aux;
        }
    }
}
