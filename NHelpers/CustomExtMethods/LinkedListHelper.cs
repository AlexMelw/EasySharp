namespace EasySharp.NHelpers.CustomExtMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LinkedListHelper
    {
        /// <summary>
        ///     Sorts source of type <see cref="LinkedList{T}" /> in ascending order
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        public static void SelfSortAscending<TSource, TKey>(this LinkedList<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>(source);
            source.Clear();
            IEnumerable<TSource> orderedEnumerable = tempLinkedList.OrderBy(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => source.AddLast(value));
        }

        /// <summary>
        ///     Sorts source of type <see cref="LinkedList{T}" /> in descending order
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        public static void SelfSortDescending<TSource, TKey>(this LinkedList<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>(source);
            source.Clear();
            IEnumerable<TSource> orderedEnumerable = tempLinkedList.OrderByDescending(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => source.AddLast(value));
        }

        /// <summary>
        ///     Sorts the copy of source of type <see cref="LinkedList{T}" /> in ascending order
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An <see cref="LinkedList{T}"/> (where <c>T</c> is <typeparamref name="TSource"/>) whose elements are sorted in ascending order according to a key.</returns>
        public static LinkedList<TSource> SortedAscending<TSource, TKey>(this LinkedList<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>();
            IEnumerable<TSource> orderedEnumerable = source.OrderBy(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => tempLinkedList.AddLast(value));
            return tempLinkedList;
        }

        /// <summary>
        ///     Sorts the copy of source of type <see cref="LinkedList{T}" /> in descending order
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An <see cref="LinkedList{T}"/> (where <c>T</c> is <typeparamref name="TSource"/>) whose elements are sorted in descending order according to a key.</returns>
        public static LinkedList<TSource> SortedDescending<TSource, TKey>(this LinkedList<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>();
            IEnumerable<TSource> orderedEnumerable = source.OrderByDescending(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => tempLinkedList.AddLast(value));
            return tempLinkedList;
        }
    }
}