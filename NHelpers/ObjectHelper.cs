using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Converts the variable of type <see cref="object"/> (which underlying type is anonymous) to the same anonymous type.
        /// </summary>
        /// <example>
        /// <code>
        /// static void Enter(object wrapper)
        /// {
        ///     var anonymousTypeHolder = wrapper.CastTo(new { x = 0, y = string.Empty });
        ///     Console.WriteLine($"{anonymousTypeHolder.x} : {anonymousTypeHolder.y}");
        /// }
        /// </code>
        /// </example>
        /// <typeparam name="T">Inferred anonymous type</typeparam>
        /// <param name="value">Wrapper for an anonymous object</param>
        /// <param name="targetType">Variable provided for type inference</param>
        /// <returns>Wrapper casted to the inferred anonymous type</returns>
        public static T CastTo<T>(this object value, T targetType) => (T) value;
    }
}