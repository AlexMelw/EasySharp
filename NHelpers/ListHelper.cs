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
        public static void AddIfNotContains<T>(this IList<T> list, T value)
        {
            if (!list.Contains(value)) list.Add(value);
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();

            for (int i = list.Count - 1; i > 0; i--)
                Swap(list[i], list[rnd.Next(0, i)]);
        }

        private static void Swap<T>(T lhs, T rhs)
        {
            var aux = lhs;
            lhs = rhs;
            rhs = aux;
        }
    }
}
