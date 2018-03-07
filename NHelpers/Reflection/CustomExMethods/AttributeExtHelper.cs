namespace EasySharp.NHelpers.Reflection.CustomExMethods
{
    using System.Reflection;

    public static class AttributeExtHelper
    {
        /// <summary>
        ///     Checks whether <paramref name="memberOrType" /> has specific attribute <typeparamref name="TAttribute" />.
        /// </summary>
        /// <param name="memberOrType">
        ///     <see cref="MemberInfo" /> to be inspected for presence of the quested attribute:
        ///     <typeparamref name="TAttribute" />.
        /// </param>
        /// <param name="inherit">
        ///     <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise,
        ///     <see langword="false" />. This parameter is ignored for properties and events; see Remarks.
        /// </param>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        public static bool HasDefinedAttribute<TAttribute>(this MemberInfo memberOrType, bool inherit = true)
        {
            return memberOrType.IsDefined(typeof(TAttribute), inherit);
        }
    }
}