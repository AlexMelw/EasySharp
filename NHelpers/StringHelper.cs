using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class StringHelper
    {
        public static int CountOccurencesCaseInsensitive(this string source, string subStr)
        {
            int occurencesCount = 0;
            
            for (int cursorPosition = 0; true; cursorPosition += subStr.Length)
            {
                cursorPosition = source.IndexOf(subStr, cursorPosition,
                    StringComparison.InvariantCultureIgnoreCase);

                // searchKey isn't contained by the given substring
                if (cursorPosition == -1) break;

                // searchKey IS contained by the given substring
                ++occurencesCount;
            }

            return occurencesCount;
        }

        public static int CountOccurences(this string source, string subStr)
        {
            int occurencesCount = 0;

            for (int cursorPosition = 0; true; cursorPosition += subStr.Length)
            {
                cursorPosition = source.IndexOf(subStr, cursorPosition,
                    StringComparison.InvariantCulture);

                // searchKey isn't contained by the given substring
                if (cursorPosition == -1) break;

                // searchKey IS contained by the given substring
                ++occurencesCount;
            }

            return occurencesCount;
        }
    }
}