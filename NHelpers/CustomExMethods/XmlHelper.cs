namespace EasySharp.NHelpers.CustomExMethods
{
    using System;
    using System.Xml.Linq;

    public static class XmlHelper
    {
        public static T GetAs<T>(this XAttribute attr, T defaultValue = default(T))
        {
            T ret = defaultValue;

            if (!string.IsNullOrEmpty(attr?.Value))
            {
                // Cast to Return Data Type 
                // ChangeType does NOT work with Nullable Types
                ret = (T) Convert.ChangeType(attr.Value, typeof(T));
            }

            return ret;
        }

        public static T GetAs<T>(this XElement elem, T defaultValue = default(T))
        {
            T ret = defaultValue;

            if (!string.IsNullOrEmpty(elem?.Value))
            {
                // Cast to Return Data Type 
                // ChangeType does NOT work with Nullable Types
                ret = (T) Convert.ChangeType(elem.Value, typeof(T));
            }

            return ret;
        }
    }
}