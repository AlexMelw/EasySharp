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
        public static bool In<TSource>(this TSource key, IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            return source.Contains(key, comparer);
        }

        /// <summary>
        ///     <para>
        ///         Writes on console the parameter <paramref name="value" /> as a <see cref="string" />, followed by a line terminator to
        ///         the text string or stream.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" /> and <typeparamref name="TValue" /> is a
        ///         primitive type, then it is printed as an array of comma-separated items.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" /> and <typeparamref name="TValue" /> is
        ///         <see cref="string" /> type, then it is printed as an array of items wrapped in quotation marks, separated by
        ///         <see cref="Environment.NewLine" /> and a comma.
        ///     </para>
        ///     <para>
        ///         If <paramref name="value" /> is a <see cref="IEnumerable{T}" /> and <typeparamref name="TValue" /> is an
        ///         unknown type, then it is printed as a JS-like array representation.
        ///     </para>
        /// </summary>
        /// <param name="value">
        ///     The object to be written on console. If <paramref name="value" /> is <see langword="null" />, only
        ///     the line terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <typeparam name="TValue">The type of <paramref name="value" /> parameter.</typeparam>
        /// <example>
        ///     <code>777.Print();</code>
        ///     <code>
        ///         int x = 55; x.Print();
        ///     </code>
        ///     <code>new[] {"Apple", "Not \"Apple", "Foo"}.Print();</code>
        ///     <code>new[] { 1, 2, 6, 98 }.Print();</code>
        ///     <code>
        ///         new[]
        ///         {
        ///             new Student
        ///             {
        ///                 FirstName = "Alex",
        ///                 LastName = "Melvin",
        ///                 AverageMark = 9.54
        ///             },
        ///             new Student
        ///             {
        ///                 FirstName = "Allen",
        ///                 LastName = "Poll",
        ///                 AverageMark = 8.43
        ///             }
        ///         }.Print();
        /// </code>
        /// </example>
        public static void Print<TValue>(this TValue value)
        {
            if (value is IEnumerable enumerable)
            {
                IEnumerable<object> objectsCollection = enumerable.Cast<object>();

                if (!objectsCollection.Any())
                    return;

                object firstItem = objectsCollection.First();
                Type itemType = firstItem.GetType();
                bool isPrimitive = itemType.IsPrimitive;

                IEnumerable<string> enumerationAsStrings = objectsCollection.Select(o => o.ToString());

                if (isPrimitive)
                {
                    Console.Out.WriteLine($"[ {enumerationAsStrings.ToCommaSeparatedString()} ]");
                    return;
                }
                if (itemType == typeof(string))
                {
                    string indentation = "    ";
                    string NewLine = Environment.NewLine;

                    string resultString = enumerationAsStrings.Aggregate(
                        seed: $"[",
                        func: (accumulator, item) => $@"{accumulator}{NewLine}{indentation}""{item}"",",
                        resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 1)}{NewLine}]");

                    Console.Out.WriteLine(resultString);

                    return;
                }
                // ELSE
                // We deal with an unknown type of items within IEnumerable<T>
                Console.Out.WriteLine(enumerationAsStrings.ToJsArrayRepresentation());
            }
            else
            {
                // Single value (not a collection)
                Console.Out.WriteLine(value);
            }
        }
    }
}