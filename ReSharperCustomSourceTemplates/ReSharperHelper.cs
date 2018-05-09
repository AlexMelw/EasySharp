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
        [Macro(Target = "suggestion", Editable = 1, Expression = "variableOfType()")]
        public static void outv<T>(this T source)
        {
            //$ Console.Out.WriteLine($"$suggestion$ = {source}");
        }

        [SourceTemplate]
        [Macro(Target = "lvalue", Editable = 0, Expression = "suggestVariableName()")]
        public static void assign<T>(this T source)
        {
            //$ lvalue = source;
        }

        [SourceTemplate]
        public static void isNullOrWhiteSpace(this string source)
        {
            //$string.IsNullOrWhiteSpace(source)$END$
        }

        [SourceTemplate]
        public static void ifNullOrWhiteSpace(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                //$$END$    
            }
        }

        [SourceTemplate]
        public static void ifnotNullOrWhiteSpace(this string source)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                //$$END$    
            }
        }

        [SourceTemplate]
        public static void ifnot(this bool sourceExpr)
        {
            if (!sourceExpr)
            {
                //$$END$    
            }
        }


        [SourceTemplate]
        [Macro(Target = "TYPE", Expression = "completeType()", Editable = 0)]
        public static void As<T>(this T source)
        {
            //$(source as TYPE)
        }
    }
}