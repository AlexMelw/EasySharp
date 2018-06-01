namespace EasySharp.NHelpers.Collections.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ICollectionExtensions
    {
        /// <summary>
        ///     Adds <paramref name="value" /> to it lacks this value.
        /// </summary>
        /// <typeparam name="T">Type of generic <see cref="ICollection{T}" /></typeparam>
        /// <param name="source">
        ///     The <see cref="ICollection{T}" /> where should be inserted a distinct <paramref name="value" />
        /// </param>
        /// <param name="value">Value to be inserted in <paramref name="source" /> if it is a distinct one.</param>
        public static void AddIfNotContains<T>(this ICollection<T> source, T value)
        {
            if (!source.Contains(value)) source.Add(value);
        }


        /// <summary>
        ///     Removes the elements of the specified collection from the end of the <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of generic <see cref="ICollection{T}" />.</typeparam>
        /// <param name="source">
        ///     The <see cref="ICollection{T}" /> where should be inserted items of <paramref name="collection" />
        ///     parameter.
        /// </param>
        /// <param name="collection">
        ///     The collection whose elements should be removed from the <paramref name="source" />.
        ///     The collection itself cannot be <see langword="null" />, but it
        ///     can contain elements that are <see langword="null" />, if type <typeparamref name="T" /> is a reference type.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        /// <returns>Reference to the <paramref name="source"/> object.</returns>
        public static ICollection<T> RemoveRange<T>(this ICollection<T> source, IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (T item in collection) source.Remove(item);

            return source;
        }

        /// <summary>
        ///     Adds the elements of the specified collection to the end of the <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of generic <see cref="ICollection{T}" />.</typeparam>
        /// <param name="source">
        ///     The <see cref="ICollection{T}" /> where should be inserted items of <paramref name="collection" />
        ///     parameter.
        /// </param>
        /// <param name="collection">
        ///     The collection whose elements should be addred to the <paramref name="source" />.
        ///     The collection itself cannot be <see langword="null" />, but it
        ///     can contain elements that are <see langword="null" />, if type <typeparamref name="T" /> is a reference type.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        /// <returns>Reference to the <paramref name="source"/> object.</returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> source, IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (T item in collection) source.Add(item);

            return source;
        }
    }
}