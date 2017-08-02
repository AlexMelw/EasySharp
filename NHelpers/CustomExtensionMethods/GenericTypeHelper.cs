namespace EasySharp.NHelpers.CustomExtensionMethods
{
    using System.Collections.Generic;
    using System.Linq;

    public static class GenericTypeHelper
    {
        /// <summary>
        ///     Determines whether the sequence <paramref name="source" /> contains the specified element by using the default
        ///     equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="key">The value to locate in the sequence <paramref name="source" /></param>
        /// <param name="source">A sequence in which to locate the quest value (<paramref name="key" />)</param>
        /// <returns>
        ///     <see langword="true" /> if the source sequence contains an element that has the specified value
        ///     <paramref name="key" />; otherwise, <see langword="false" />.
        /// </returns>
        public static bool In<TSource>(this TSource key, IEnumerable<TSource> source)
        {
            return source.Contains(key);
        }


        /// <summary>
        ///     Determines whether the sequence <paramref name="source" /> contains the specified element by using the default
        ///     equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="key">The value to locate in the sequence <paramref name="source" /></param>
        /// <param name="source">A sequence in which to locate the quest value (<paramref name="key" />)</param>
        /// <param name="comparer">An equality comparer to compare values.</param>
        /// <returns>
        ///     <see langword="true" /> if the source sequence contains an element that has the specified value
        ///     <paramref name="key" />; otherwise, <see langword="false" />.
        /// </returns>
        public static bool In<TSource>(this TSource key, IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            return source.Contains(key, comparer);
        }
    }
}