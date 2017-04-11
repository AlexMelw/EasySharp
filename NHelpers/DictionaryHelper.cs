using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class DictionaryHelper
    {
        public static void computeIfAbsent<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            TKey key, Func<TKey, List<TValue>> mappingFunc)
        {
            // JAVA CODE
            //if ((v = get(key)) == null)
            //{
            //    V newValue;
            //    if ((newValue = mappingFunc.apply(key)) != null)
            //    {
            //        put(key, newValue);
            //    }
            //}
        }

        // USAGE
        //for (Student student: studentsCollection){
        //     map.computeIfAbsent(keySpecification.getComparisionKey(student), k -> new ArrayList<>());
        //     map.get(keySpecification.getComparisionKey(student)).add(student);
        //}

        // instead of

        //for (Student student: studentsCollection){
        //   if (map.get(keySpecification.getComparisionKey(student)) == null){
        //       map.put(keySpecification.getComparisionKey(student), new ArrayList<>());
        //   }
        //   map.get(keySpecification.getComparisionKey(student)).add(student);
        //}
    }
}