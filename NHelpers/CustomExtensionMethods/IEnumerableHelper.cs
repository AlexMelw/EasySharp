namespace EasySharp.NHelpers.CustomExtensionMethods
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Environment;

    public static class IEnumerableHelper
    {
        /// <summary>Determines whether exists any element of a sequence that satisfies a condition.</summary>
        /// <param name="source">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to apply the predicate
        ///     to.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>
        ///     true if exists any element in the source sequence that passed the test in the specified predicate; otherwise,
        ///     false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is null.
        /// </exception>
        public static bool Exists<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Any(predicate);
        }

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
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma-separated string</returns>
        public static string ToCommaSeparatedString<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any())
            {
                return string.Empty;
            }

            return source.Aggregate(
                seed: string.Empty,
                func: (accumulator, item) => $"{accumulator}, {item}",
                resultSelector: accumulator => accumulator.Substring(2, accumulator.Length - 2));
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string ToCommaSeparatedString<TSource>(params TSource[] source)
        {
            return source.ToCommaSeparatedString();
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" /> ending with a dot.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma-separated string</returns>
        public static string ToCommaSeparatedStringWithEndingDot<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any())
            {
                return string.Empty;
            }

            return source.Aggregate(
                seed: string.Empty,
                func: (accumulator, item) => $"{accumulator}, {item}",
                resultSelector: accumulator => $"{accumulator.Substring(2, accumulator.Length - 2)}.");
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" /> ending with a dot.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string ToCommaSeparatedStringWithEndingDot<TSource>(params TSource[] source)
        {
            return source.ToCommaSeparatedStringWithEndingDot();
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to an JS-like array
        ///     representation.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns><see cref="string" /> as an JS-like array representation.</returns>
        public static string ToJsArrayRepresentation<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any())
            {
                return "[..EMPTY..]";
            }

            string indentation = "    ";
            string doubleIndentation = indentation + indentation;

            return source.Aggregate(
                seed: $"[",
                func: (accumulator, item) => $@"{accumulator}{NewLine}{indentation}{{{NewLine}{doubleIndentation}{
                        item.ToString().Replace(NewLine, $"{NewLine}{doubleIndentation}")
                    }{NewLine}{indentation}}},",
                resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 1)}{NewLine}]");
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to an JS-like array
        ///     representation.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns><see cref="string" /> as an JS-like array representation.</returns>
        public static string ToJsArrayRepresentation<TSource>(params TSource[] source)
        {
            return source.ToJsArrayRepresentation();
        }
    }
}