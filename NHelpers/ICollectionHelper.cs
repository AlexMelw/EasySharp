using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class ICollectionHelper
    {
        /// <summary>
        ///     Adds <paramref name="value" /> to it lacks this value.
        /// </summary>
        /// <typeparam name="T">Type of generic <see cref="ICollection{T}" /></typeparam>
        /// <param name="source">The <see cref="ICollection{T}" /> where should be insterted a distinct <paramref name="value" /></param>
        /// <param name="value">Value to be inserted in <paramref name="source" /> if it is a distinct one.</param>
        public static void AddIfNotContains<T>(this ICollection<T> source, T value)
        {
            if (!source.Contains(value)) source.Add(value);
        }
    }
}
