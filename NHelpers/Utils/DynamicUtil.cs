namespace EasySharp.NHelpers.Utils
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    public class DynamicUtil
    {
        public static T ToAnonymous<T>(ExpandoObject source, T sample)
            where T : class
        {
            var dict = (IDictionary<string, object>) source;

            var ctor = sample.GetType().GetConstructors().Single();

            var parameters = ctor.GetParameters();

            var parameterValues = parameters.Select(p => dict[p.Name]).ToArray();

            return (T) ctor.Invoke(parameterValues);
        }
    }
}