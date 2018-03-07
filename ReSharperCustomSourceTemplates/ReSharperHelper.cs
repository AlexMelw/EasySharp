using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasySharp.ReSharperCustomSourceTemplates
{
    public static class ReSharperHelper
    {
        [SourceTemplate]
        [Macro(Target = "item", Expression = "suggestVariableName()", Editable = 0)]
        public static void PrintAll<T>(this IEnumerable<T> source) where T : struct 
        {
            foreach (var item in source)
            {
                //$$SELSTART$Console.WriteLine(item);$SELEND$
            }
            //$$END$
        }

        [SourceTemplate]
        [Macro(Target = "lvalue", Expression = "suggestVariableName()", Editable = 0)]
        public static void Assign<T>(this T source)
        {
            //$ lvalue = source;
        }
    }
}