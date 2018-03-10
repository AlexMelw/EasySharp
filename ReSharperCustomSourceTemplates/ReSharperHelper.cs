using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasySharp.ReSharperCustomSourceTemplates
{
    public static class ReSharperHelper
    {
        [SourceTemplate]
        [Macro(Target = "item", Expression = "suggestVariableName()", Editable = 0)]
        public static void printAll<T>(this IEnumerable<T> source) where T : struct 
        {
            foreach (var item in source)
            {
                //$$SELSTART$Console.WriteLine(item);$SELEND$
            }
            //$$END$
        }

        [SourceTemplate]
        [Macro(Target = "lvalue", Expression = "suggestVariableName()", Editable = 0)]
        public static void assign<T>(this T source)
        {
            //$ lvalue = source;
        }

        [SourceTemplate]
        public static void isNullOrEmpty(this string source)
        {
            //$string.IsNullOrEmpty(source)$END$
        }

        [SourceTemplate]
        public static void isNullOrWhiteSpace(this string source)
        {
            //$string.IsNullOrWhiteSpace(source)$END$
        }

        [SourceTemplate]
        [Macro(Target = "TYPE", Expression = "completeType()", Editable = 0)]
        public static void As<T>(this T source)
        {
            //$(source as TYPE)
        }
    }
}