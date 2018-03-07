namespace EasySharp.NHelpers.CustomExMethods
{
    using System;

    public static class ConvertExtHelper
    {
        /// <summary>
        ///     Returns an object of the specified type and whose value is equivalent to the specified object.
        /// </summary>
        /// <typeparam name="TTarget">The type of object to return.</typeparam>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <param name="source">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
        /// <returns>
        ///     An object whose type is <typeparamref name="TTarget" /> and whose value is equivalent to
        ///     <paramref name="source" />.-or-A null reference (<see langword="Nothing" /> in Visual Basic), if
        ///     <paramref name="source" /> is <see langword="null" /> and <typeparamref name="TTarget" /> is not a value type.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">
        ///     This conversion is not supported.  -or-
        ///     <paramref name="source" /> does not implement the <see cref="T:System.IConvertible" /> interface.
        /// </exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="source" /> is not in a format recognized by <typeparamref name="TTarget" />.
        /// </exception>
        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="source" /> represents a number that is out of the range of <typeparamref name="TTarget" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static TTarget ChangeTypeTo<TTarget, TSource>(this TSource source,
            TTarget defaultValue = default(TTarget))
        {
            TTarget ret;

            try
            {
                if (source != null)
                {
                    ret = (TTarget) Convert.ChangeType(source, typeof(TSource));
                }
                else
                {
                    ret = defaultValue;
                }
            }
            catch (Exception)
            {
                ret = defaultValue;
            }

            return ret;
        }
    }
}