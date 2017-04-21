using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasySharp.ReSharperCustomSourceTemplates
{
    public static class ReSharperHelper
    {
        [SourceTemplate]
        public static void PrintAll<T>(this IEnumerable<T> source) where T : struct 
        {
            foreach (var element in source)
                Console.WriteLine(element);
            //$ $END$
        }
    }
}