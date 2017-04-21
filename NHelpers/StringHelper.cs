using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp.NHelpers
{
    public static class StringHelper
    {
        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparision mode is <see cref="StringComparison.OrdinalIgnoreCase" /></remarks>
        /// <param name="source"></param>
        /// <param name="subStr"></param>
        /// <returns>Number of occurences of <paramref name="subStr" /> is <paramref name="source" /></returns>
        public static int CountOccurencesOrdinalCaseInsensitive(this string source, string subStr)
        {
            return CountOccurenceInSource(source, subStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparision mode is <see cref="StringComparison.InvariantCultureIgnoreCase" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesInvariantCultureIgnoreCase(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparision mode is <see cref="StringComparison.Ordinal" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesOrdinal(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparision mode is <see cref="StringComparison.InvariantCulture" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesInvariantCulture(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.InvariantCulture);
        }


        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparision mode is <see cref="StringComparison.Ordinal" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString">search key</param>
        /// <param name="stringComparisonMode"></param>
        /// <returns>Number of occurences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurences(this string source, string subString,
            StringComparison stringComparisonMode = StringComparison.Ordinal)
        {
            return CountOccurenceInSource(source, subString, stringComparisonMode);
        }

        /// <summary>
        ///     Counts occurences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <param name="mode"></param>
        /// <returns>Number of occurences of <paramref name="subStr" /> is <paramref name="source" /></returns>
        private static int CountOccurenceInSource(string source, string subString, StringComparison mode)
        {
            int occurencesCount = 0;

            for (int indexPosition = 0;; indexPosition += subString.Length)
            {
                indexPosition = source.IndexOf(subString, indexPosition, mode);

                // searchKey isn't contained by the given substring (indexPosition..source.Length-1)
                if (indexPosition == -1) break;

                // searchKey IS contained by the given substring (indexPosition..source.Length-1)
                ++occurencesCount;
            }

            return occurencesCount;
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{T}" /> where <c>T</c> is
        ///     <see cref="string" />
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection source that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedString(this IEnumerable<string> source) => source.Aggregate(
            (s1, s2) => $"{s1}, {s2}");

        /// <summary>
        ///     Aggregates the <paramref name="source" /> that is a collection of variable number of arguments or an array of arguments of 
        ///     <see cref="string" /> type.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection source that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        public static string CommaSeparatedString(params string[] source) => source.CommaSeparatedString();
    }
}