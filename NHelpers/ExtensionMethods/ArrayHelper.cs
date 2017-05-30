namespace EasySharp.NHelpers.ExtensionMethods
{
    using System;

    public static class ArrayHelper
    {
        /// <summary>
        ///     Sets each element in the <paramref name="sourceArray" /> (of type <c>T</c>) to its default value (<c>default(T)</c>
        ///     )
        /// </summary>
        /// <typeparam name="T">Any available type</typeparam>
        /// <param name="sourceArray">Array which elements should be initialized or set to defaults (<c>default(T)</c>)</param>
        public static void SelfSetToDefaults<T>(this T[] sourceArray)
        {
            for (int i = 0; i < sourceArray.Length; i++)
                sourceArray[i] = default(T);
        }

        /// <summary>
        ///     Sets each element in the copy of <paramref name="sourceArray" /> (of type <c>T</c>) to its default value (
        ///     <c>default(T)</c>)
        /// </summary>
        /// <typeparam name="T">Any available type</typeparam>
        /// <param name="sourceArray">Array which elements should be initialized or set to defaults (<c>default(T)</c>)</param>
        /// <returns><see cref="Array" /> of type <c>T</c></returns>
        public static T[] SetToDefaults<T>(this T[] sourceArray)
        {
            T[] newArray = { };

            for (int i = 0; i < sourceArray.Length; i++)
                newArray[i] = default(T);

            return newArray;
        }
    }
}