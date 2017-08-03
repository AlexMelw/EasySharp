namespace EasySharp.NHelpers.CustomExtensionMethods
{
    using System;
    using System.Collections;
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

        /// <summary>
        ///     <para>
        ///         Writes on console the <paramref name="value" /> as a <see cref="string" />, followed by a line terminator to
        ///         the text string or stream.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" />, then it is printed as a JS-like array
        ///         representation.
        ///     </para>
        /// </summary>
        /// <param name="value">
        ///     The object to be written on console. If <paramref name="value" /> is <see langword="null" />, only
        ///     the line terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <typeparam name="TValue">The type of <paramref name="value" /> parameter.</typeparam>
        public static void Print<TValue>(this TValue value)
        {
            if (value is IEnumerable enumerable)
            {
                IEnumerable<string> strings = enumerable.Cast<object>().Select(o => o.ToString());
                Console.Out.WriteLine(strings.ToJsArrayRepresentation());
            }
            else
            {
                Console.Out.WriteLine(value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Writes on console the <paramref name="value" /> as a <see cref="string" />, followed by a line terminator to
        ///         the text string or stream.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" />, then it is printed as sequence of
        ///         comma-separated items.
        ///     </para>
        /// </summary>
        /// <param name="value">
        ///     The object to be written on console. If <paramref name="value" /> is <see langword="null" />, only
        ///     the line terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <typeparam name="TValue">The type of <paramref name="value" /> parameter.</typeparam>
        public static void PrintAsCommaSeparatedString<TValue>(this TValue value)
        {
            if (value is IEnumerable enumerable)
            {
                IEnumerable<string> strings = enumerable.Cast<object>().Select(o => o.ToString());
                Console.Out.WriteLine(strings.ToCommaSeparatedString());
            }
            else
            {
                Console.Out.WriteLine(value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Writes on console the <paramref name="value" /> as a <see cref="string" />, followed by a line terminator to
        ///         the text string or stream.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" />, then it is printed as sequence of
        ///         comma-separated items ending with a dot.
        ///     </para>
        /// </summary>
        /// <param name="value">
        ///     The object to be written on console. If <paramref name="value" /> is <see langword="null" />, only
        ///     the line terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <typeparam name="TValue">The type of <paramref name="value" /> parameter.</typeparam>
        public static void PrintAsCommaSeparatedStringWithEndingDot<TValue>(this TValue value)
        {
            if (value is IEnumerable enumerable)
            {
                IEnumerable<string> strings = enumerable.Cast<object>().Select(o => o.ToString());
                Console.Out.WriteLine(strings.ToCommaSeparatedStringWithEndingDot());
            }
            else
            {
                Console.Out.WriteLine($"{value}.");
            }
        }
    }
}