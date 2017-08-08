﻿namespace EasySharp.NHelpers.CustomExtensionMethods.Reflection
{
    using System.Reflection;

    public static class MethodInfoHelper
    {
        /// <summary>
        ///     Detects whether the given method is overridden.
        /// </summary>
        /// <param name="methodInfo">The method to inspect.</param>
        /// <returns><see langword="true" /> if method is overridden, otherwise <see langword="false" />.</returns>
        /// <remarks>For more information see <see href="https://stackoverflow.com/a/45560768/5259296">stackoverflow.</see>
        /// </remarks>
        public static bool IsOverridden(this MethodInfo methodInfo)
        {
            bool methodInfoIsVirtual = methodInfo.IsVirtual;
            return methodInfo.DeclaringType == methodInfo.ReflectedType
                   && !methodInfo.IsAbstract;
        }
    }
}