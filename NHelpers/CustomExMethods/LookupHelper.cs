namespace EasySharp.NHelpers.CustomExMethods
{
    using System.Collections.Generic;
    using System.Linq;

    public static class LookupHelper
    {
        /// <summary>
        /// Grabs all keys from source as <see cref="IEnumerable{T}"/> where <c>T</c> is <typeparamref name="TKey"/>
        /// </summary>
        /// <remarks>Instant execution</remarks>
        /// <typeparam name="TKey">Type of key</typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source">Source lookup</param>
        /// <returns><see cref="IEnumerable{T}"/> of keys</returns>
        public static IEnumerable<TKey> GetKeys<TKey, TValue>(this ILookup<TKey, TValue> source)
        {
            return source.Select(group => group.Key).ToList();
        }
    }
}