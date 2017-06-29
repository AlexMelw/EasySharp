﻿namespace EasySharp.NHelpers.ExtensionMethods
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableHelper
    {
        /// <summary>
        ///     Returns <c>true</c> if sequence is empty, otherwise returns <c>false</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> that contains the elements to be counted.</param>
        /// <returns><c>true</c> if length is 0, else <c>false</c>.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source.LongCount().Equals(0L);
        }

        /// <summary>
        ///     Returns <c>true</c> if sequence is empty, otherwise returns <c>false</c>
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> if length is 0, else <c>false</c>.</returns>
        public static bool IsEmpty(this IEnumerable source)
        {
            return source.Cast<object>().LongCount().Equals(0L);
        }

        /// <summary>
        ///     Performs the specified action on each element of the <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">Type of elements in <paramref name="source" /></typeparam>
        /// <param name="source">
        ///     <see cref="IEnumerable{T}" /> sequence of values on which should be performed the
        ///     <paramref name="action" />
        /// </param>
        /// <param name="action">
        ///     The <see cref="Action{T}" /> delegate to perform on each element of the
        ///     <see cref="IEnumerable{T}" />.
        /// </param>
        /// <exception cref="ArgumentNullException">Passed <paramref name="action" /> is null</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T element in source)
                action(element);
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" />
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedString<TSource>(this IEnumerable<TSource> source)
        {
            return source.Aggregate(
                seed: string.Empty,
                func: (accumulator, item) => $"{item}, {accumulator}",
                resultSelector: accumulator => accumulator.Substring(0, accumulator.Length - 2));
        }

        /// <summary>
        ///     Aggregates the <paramref name="source" /> that is a collection of variable number of arguments or an array of
        ///     arguments of <typeparamref name="TSource"/> type.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedString<TSource>(params TSource[] source)
        {
            return source.CommaSeparatedString();
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" />
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedStringWithEndingDot<TSource>(this IEnumerable<TSource> source)
        {
            return source.Aggregate(
                seed: string.Empty,
                func: (accumulator, item) => $"{item}, {accumulator}",
                resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 2)}.");
        }

        /// <summary>
        ///     Aggregates the <paramref name="source" /> that is a collection of variable number of arguments or an array of
        ///     arguments of <typeparamref name="TSource"/> type.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedStringWithEndingDot<TSource>(params TSource[] source)
        {
            return source.CommaSeparatedStringWithEndingDot();
        }
    }
}