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
        public static string Serialize<TValue>(
            TValue objectGraph,
            bool allowRecursiveSerialization = false,
            int recursiveSerializationMaxLevelDepth = int.MaxValue - 1)
        {
            string serializedObject = Serialize(objectGraph,
                allowRecursiveSerialization
                    ? recursiveSerializationMaxLevelDepth == 0
                        ? int.MaxValue - 1
                        : recursiveSerializationMaxLevelDepth
                    : 1,
                currentRecursionLevel: 1);
            return serializedObject;
        }

        private static string Serialize<TValue>(
            TValue objectGraph,
            int recursiveSerializationMaxAllowedDepth,
            int currentRecursionLevel)
        {
            if (objectGraph == null)
            {
                return "null";
            }

            if (currentRecursionLevel > recursiveSerializationMaxAllowedDepth)
            {
                return "{ NOT EVALUATED }";
            }

            PropertyInfo[] propertiesInfos = objectGraph.GetType().GetProperties();

            if (!propertiesInfos.Any())
            {
                return "{ NOTHING TO BE SERIALIZED: No properties found }";
            }

            #region Comments

            //IEnumerable<string> simplePropertyKeyValuePairCollection = propertiesInfos
            //    .Where(p => IsSimpleType(p.PropertyType))
            //    .Select(propInfo =>
            //    {
            //        string key = propInfo.Name;
            //        string value = propInfo.GetValue(objectGraph) is string
            //            ? $"\"{propInfo.GetValue(objectGraph).ToString()}\""
            //            : propInfo.GetValue(objectGraph).ToString();

            //        return $"{key}: {value}";
            //    });

            //if (currentRecursionLevel == recursiveSerializationMaxLevelDepth)
            //{
            //    IEnumerable<string> enumerable = simplePropertyKeyValuePairCollection.Concat(
            //        propertiesInfos.Where(p => false == IsSimpleType(p.PropertyType))
            //            .Select(p =>
            //            {
            //                string key = p.Name;
            //                string value = "{ CONTENT NOT ANALYSED }";

            //                return $"{key}: {value}";
            //            }));

            //    ProjectPropertiesSeparatingByCommaAndNewLine(enumerable,
            //        new string(' ', 4 * (currentRecursionLevel - 1)));
            //}

            #endregion

            IEnumerable<string> simplePropertiesKeyValuePairCollection = propertiesInfos
                .Where(p => IsSimpleType(p.PropertyType))
                .Select(propInfo =>
                {
                    string key = propInfo.Name;
                    string value = propInfo.GetValue(objectGraph) is string
                        ? $"\"{propInfo.GetValue(objectGraph).ToString()}\""
                        : propInfo.GetValue(objectGraph).ToString();

                    return $"{key}: {value}";
                });

            IEnumerable<string> complexPropertyKeyValuePairCollection = propertiesInfos
                .Where(p => false == IsSimpleType(p.PropertyType))
                .Select(propInfo =>
                {
                    string key = propInfo.Name;
                    string value = propInfo.PropertyType.HasToStringOverridden()
                        ? propInfo.GetValue(objectGraph) is string
                            ? $"\"{propInfo.GetValue(objectGraph).ToString()}\""
                            : propInfo.GetValue(objectGraph).ToString()
                        : EasySharpReflector.Serialize(
                            propInfo.GetValue(objectGraph),
                            recursiveSerializationMaxAllowedDepth,
                            currentRecursionLevel + 1);

                    return $"{key}: {value}";
                });

            IEnumerable<string> allPropertiesGraph =
                simplePropertiesKeyValuePairCollection.Concat(complexPropertyKeyValuePairCollection);

            string projection = currentRecursionLevel == 1
                ? ProjectPropertiesSeparatingByCommaAndNewLine(allPropertiesGraph,
                    new string(' ', 4 * (currentRecursionLevel - 1)))
                : $"{{{(ToJsObjectRepsrezentation(allPropertiesGraph, new string(' ', 4 * (currentRecursionLevel - 1))))}";

            return projection;
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                   || type == typeof(string)
                   || type == typeof(DateTime)
                   || type == typeof(TimeSpan)
                   || type == typeof(Enum);
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
                resultSelector: accumulator => $"{accumulator.Substring(2, accumulator.Length - 3)}");
        }

        private static string ToJsObjectRepsrezentation(IEnumerable<string> enumerationAsStrings,
            string indentation)
        {
            string NewLine = Environment.NewLine;

            string result = enumerationAsStrings.Aggregate(
                seed: $"",
                func: (accumulator, item) => $@"{accumulator}{NewLine}{indentation}{item},",
                resultSelector: accumulator =>
                    $"{accumulator.Substring(0, accumulator.Length - 1)}{NewLine}{indentation.Substring(0, indentation.Length - 4)}}}");

            return result;
        }
    }
}