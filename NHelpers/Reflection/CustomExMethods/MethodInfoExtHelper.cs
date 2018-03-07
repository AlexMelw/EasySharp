namespace EasySharp.NHelpers.Reflection.CustomExMethods
{
    using System.Reflection;

    public static class MethodInfoExtHelper
    {
        /// <summary>
        ///     Detects whether the given method is overridden.
        /// </summary>
        /// <param name="methodInfo">The method to inspect.</param>
        /// <returns><see langword="true" /> if method is overridden, otherwise <see langword="false" />.</returns>
        /// <remarks>
        ///     For more information see <a href="https://stackoverflow.com/a/45560768/5259296">stackoverflow.</a>
        /// </remarks>
        public static bool IsOverridden(this MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType == methodInfo.ReflectedType
                   && !methodInfo.IsAbstract;
        }
    }
}