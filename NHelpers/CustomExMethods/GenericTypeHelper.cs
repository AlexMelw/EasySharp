namespace EasySharp.NHelpers.CustomExMethods
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Reflection;
    using Reflection.CustomExMethods;

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
        /// <param name="allowRecursivePrinting"></param>
        /// <param name="recursiveMaxLevelDepth"></param>
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
        public static void Print<TValue>(this TValue value,
            bool allowRecursivePrinting = false,
            int recursiveMaxLevelDepth = int.MaxValue - 1)
        {
            if (value is IEnumerable enumerable)
            {
                PrintEnumerable(
                    enumerable,
                    allowRecursivePrinting,
                    recursiveMaxLevelDepth);
                return;
            }

            // Value is not a collection

            Type valueType = value.GetType();

            bool isPrimitive = valueType.IsPrimitive;

            if (!isPrimitive)
            {
                bool isOverridden = valueType.GetMethod(
                    name: nameof(ToString),
                    bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                    binder: null,
                    types: Type.EmptyTypes,
                    modifiers: null
                ).IsOverridden();

                if (isOverridden)
                {
                    Console.Out.WriteLine(value);
                    return;
                }

                // Not Overridden
                Console.Out.WriteLine(EasySharpReflector.Serialize(
                    value,
                    allowRecursivePrinting,
                    recursiveMaxLevelDepth));
                return;
            }

            // Primitive Type
            Console.Out.WriteLine(value);
        }

        public static void PrintRecursively<TValue>(this TValue value,
            int recursiveMaxLevelDepth = int.MaxValue - 1)
        {
            Print(value, /*Recursively*/ true, recursiveMaxLevelDepth);
        }

        private static void PrintEnumerable(
            IEnumerable enumerable,
            bool allowRecursiveSerialization,
            int recursiveMaxLevelDepth = int.MaxValue - 1,
            int currentRecursionLevel = 1)
        {
            IEnumerable<object> objectsCollection = enumerable.Cast<object>().ToList();

            if (!objectsCollection.Any())
                return;

            object firstItem = objectsCollection.First();
            Type itemType = firstItem.GetType();
            bool isPrimitive = itemType.IsPrimitive;

            IEnumerable<string> enumerationAsStrings = Enumerable.Empty<string>();

            // We deal with an unknown type of items within IEnumerable<T>

            // Collection has non-primitive items
            if (!isPrimitive)
            {
                // string is a non-primitive type
                if (itemType == typeof(string)
                    || itemType == typeof(DateTime)
                    || itemType == typeof(TimeSpan)
                    || itemType == typeof(Enum))
                {
                    PrintSimpleTypesCollection(objectsCollection, new string(' ', 4));
                    return;
                }

                bool isOverridden = itemType.GetMethod(
                    name: nameof(ToString),
                    bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                    binder: null,
                    types: Type.EmptyTypes,
                    modifiers: null
                ).IsOverridden();

                if (isOverridden)
                {
                    enumerationAsStrings = objectsCollection.Select(o => o.ToString());
                    Console.Out.WriteLine(enumerationAsStrings.ToJsArrayRepresentation());
                    return;
                }

                // ToString() is not overridden
                enumerationAsStrings = objectsCollection.Select(o => EasySharpReflector.Serialize(o,
                    allowRecursiveSerialization,
                    recursiveMaxLevelDepth));
                Console.Out.WriteLine(enumerationAsStrings.ToJsArrayRepresentation());
                return;
            }

            // Other primitive types
            enumerationAsStrings = objectsCollection.Select(o => o.ToString());
            PrintPrimitiveItemsCollection(enumerationAsStrings);
        }

        private static void PrintSimpleTypesCollection(IEnumerable<object> enumerationAsStrings, string indentation)
        {
            string resultString = ProjectStringSimpleTypesByCommaAndNewLine(enumerationAsStrings, indentation);

            Console.Out.WriteLine(resultString);
        }

        public static string ProjectStringSimpleTypesByCommaAndNewLine(IEnumerable<object> enumerationAsStrings,
            string indentation)
        {
            string newLine = Environment.NewLine;
            string wrapper = enumerationAsStrings.FirstOrDefault().GetType() == typeof(string) ? "\"" : string.Empty;

            return enumerationAsStrings.Aggregate(
                seed: $"[",
                func: (accumulator, item) => $@"{accumulator}{newLine}{indentation}{wrapper}{item}{wrapper},",
                resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 1)}{newLine}]");
        }

        private static void PrintPrimitiveItemsCollection(IEnumerable<string> enumerationAsStrings)
        {
            Console.Out.WriteLine($"[ {enumerationAsStrings.ToCommaSeparatedString()} ]");
        }


        public static Type GetItemsType(this Type source)
        {
            return source.GetGenericArguments() // new[] { typeof(T) }
                .Single(); // typeof(T)
        }

        /// <summary>
        ///     Determines if collection is of type <see cref="IEnumerable{T}" />
        /// </summary>
        /// <param name="sourceType"><see cref="Type" /> to be inspected.</param>
        /// <returns><see cref="bool" /> if collection is such, otherwise <see langword="null" /></returns>
        public static bool IsImplementsIEnumerable(this Type sourceType)
        {
            //return typeof(IEnumerable).IsAssignableFrom(sourceType);
            return sourceType.GetInterfaces()
                .Any(type => type.IsGenericType
                             && type.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }
    }
}