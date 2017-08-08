using System;

namespace EasySharp.NHelpers.Reflection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CustomExtMethods;
    using NHelpers.CustomExtMethods;

    public static class EasySharpReflector
    {
        public static string Serialize<TValue>(TValue objectGraph)
        {
            return Serialize<TValue>(objectGraph, allowRecursiveSerialization: false,
                recursiveSerializationMaxAllowedDepth: 0, currentRecursionLevel: 0);
        }

        private static string Serialize<TValue>(
            TValue objectGraph,
            bool allowRecursiveSerialization,
            int recursiveSerializationMaxAllowedDepth,
            int currentRecursionLevel)
        {
            if (objectGraph == null)
            {
                return null;
            }
            // allow recursive serialization
            if (currentRecursionLevel > recursiveSerializationMaxAllowedDepth)
            {
                return "{ CONTENT NOT ANALYSED }";
            }

            IEnumerable<PropertyInfo> propertyInfos = objectGraph.GetType()
                .GetProperties()
                .Where(propInfo =>
                {
                    Type propertyType = propInfo.PropertyType;
                    return propertyType.IsPrimitive
                           || propertyType == typeof(string)
                           || propertyType == typeof(DateTime)
                           || propertyType == typeof(TimeSpan)
                           || propertyType == typeof(Enum);
                });

            if (!propertyInfos.Any())
            {
                return "[ NO PROPERTIES FOUND: NOTHING TO BE SERIALIZED ]";
            }

            IEnumerable<string> primitievePropertyKeyValuePairCollection =
                propertyInfos.Select(
                    propInfo =>
                    {
                        string key = propInfo.Name;
                        string value = propInfo.GetValue(objectGraph) is string
                            ? $"\"{propInfo.GetValue(objectGraph).ToString()}\""
                            : propInfo.GetValue(objectGraph).ToString();

                        return $"{key}: {value}";
                    });

            IEnumerable<string> nonPrimitievePropertyKeyValuePairCollection = objectGraph.GetType().GetProperties()
                .Where(p => !p.PropertyType.IsPrimitive)
                .Select(propInfo =>
                {
                    string key = propInfo.Name;
                    string value = propInfo.PropertyType.HasToStringOverridden()
                        ? propInfo.ToString()
                        : EasySharpReflector.Serialize(
                            propInfo.GetValue(objectGraph),
                            allowRecursiveSerialization,
                            recursiveSerializationMaxAllowedDepth,
                            currentRecursionLevel + 1);

                    return $"{key}: {value}";
                });

            IEnumerable<string> allPropertiesGraph =
                nonPrimitievePropertyKeyValuePairCollection.Concat(primitievePropertyKeyValuePairCollection);

            string projection = ProjectPropertiesSeparatingByCommaAndNewLine(allPropertiesGraph, new string(' ', 0));

            string resultString = $"{projection}";

            if (!allowRecursiveSerialization)
            {
                return resultString;
            }

            return resultString;
        }

        private static bool HasToStringOverridden(this Type type)
        {
            bool isToStringOverridden = type.GetMethod(
                name: nameof(ToString),
                bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                types: Type.EmptyTypes,
                modifiers: null
            ).IsOverridden();

            return isToStringOverridden;
        }

        private static string ProjectPropertiesSeparatingByCommaAndNewLine(IEnumerable<string> enumerationAsStrings,
            string indentation)
        {
            string NewLine = Environment.NewLine;

            return enumerationAsStrings.Aggregate(
                seed: $"",
                func: (accumulator, item) => $@"{accumulator}{NewLine}{indentation}{item},",
                resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 1)}{NewLine}");
        }

        private static string ToJsObjectRepsrezentation(IEnumerable<string> enumerationAsStrings,
            string indentation)
        {
            string NewLine = Environment.NewLine;

            return enumerationAsStrings.Aggregate(
                seed: $"{{",
                func: (accumulator, item) => $@"{accumulator}{NewLine}{indentation}{item},",
                resultSelector: accumulator => $"{accumulator.Substring(0, accumulator.Length - 1)}{NewLine}}}");
        }
    }
}