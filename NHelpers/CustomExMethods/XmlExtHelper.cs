namespace EasySharp.NHelpers.CustomExMethods
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public static class XmlExtHelper
    {
        public static T GetAs<T>(this XAttribute attr, T defaultValue = default(T))
        {
            T ret = defaultValue;

            if (typeof(string) == typeof(T))
            {
                return (T) (object) attr?.Value;
            }

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

            if (typeof(string) == typeof(T))
            {
                return (T) (object) elem?.Value;
            }


            if (!string.IsNullOrEmpty(elem?.Value))
            {
                // Cast to Return Data Type 
                // ChangeType does NOT work with Nullable Types
                ret = (T) Convert.ChangeType(elem.Value, typeof(T));
            }

            return ret;
        }

        public static string SerializeToXml<T>(this T source)
        {
            if (source == null) return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, source);
                    return stringWriter.ToString();
                }
            }
        }

        public static T DeserializeTo<T>(this string xmlSource)
        {
            var serializer = new XmlSerializer(typeof(T));

            T deserialized = default(T);

            using (StringReader reader = new StringReader(xmlSource))
            {
                deserialized = (T) serializer.Deserialize(reader);
            }

            return deserialized;
        }
    }
}