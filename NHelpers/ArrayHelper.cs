using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class ArrayHelper
    {
        /// <summary>
        /// Sets each element to its default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void SetToDefaults<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = default(T);
            }
        }
    }
}