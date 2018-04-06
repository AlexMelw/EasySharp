namespace EasySharp.NHelpers.Collections.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ListExtensions
    {
        /// <summary>
        ///     Shuffles source of type <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">A sequence of values to shuffle</param>
        public static void SelfShuffle<T>(this IList<T> source)
        {
            Random rnd = new Random();

            for (int i = source.Count - 1; i > 0; i--)
                source.SwapElementsAt(i, rnd.Next(0, i));
        }

        /// <summary>
        ///     Shuffles source's copy of type <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">A sequence of values to shuffle</param>
        /// <returns><see cref="IList{T}" /> that is a shuffled  copy of <paramref name="source" /></returns>
        public static IList<T> Shuffle<T>(this IList<T> source)
        {
            IList<T> sourceCopy = new List<T>(source);
            Random rnd = new Random();

            for (int i = source.Count - 1; i > 0; i--)
                sourceCopy.SwapElementsAt(i, rnd.Next(0, i));

            return sourceCopy;
        }

        /// <summary>
        ///     Swaps two <paramref name="source" />'s elements located at positions <paramref name="firstPos" /> and
        ///     <paramref name="secondPos" />
        /// </summary>
        /// <typeparam name="T">Type of elements to be swapped</typeparam>
        /// <param name="source">A sequence of values</param>
        /// <param name="firstPos">First position in <paramref name="source" /> at which is located an element to be swapped</param>
        /// <param name="secondPos">Second position in <paramref name="source" /> at which is located an element to be swapped</param>
        public static void SwapElementsAt<T>(this IList<T> source, int firstPos, int secondPos)
        {
            T aux = source[firstPos];
            source[firstPos] = source[secondPos];
            source[secondPos] = aux;
        }
    }
}