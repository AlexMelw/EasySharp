using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class ObjectConvertionHelper
    {
        /// <summary>
        /// Converts the object of type Object, originally anonymous, to the same anonymous type
        /// </summary>
        /// <typeparam name="T">inferred anonymous type</typeparam>
        /// <param name="value">anonymous type wrapper in an object</param>
        /// <param name="targetType">variable provided for type inference</param>
        /// <returns>wrapped value caseted to the inferred anonymous type</returns>
        public static T CastTo<T>(this Object value, T targetType)
        {
            return (T)value;
        }

        // USAGE
        /*
        static void Enter(object wrapper)
        {
            var anonymousTypeHolder = wrapper.CastTo(new { x = 0, y = string.Empty });
            Console.WriteLine($"{anonymousTypeHolder.x} : {anonymousTypeHolder.y}");
        }
        */
    }
}
