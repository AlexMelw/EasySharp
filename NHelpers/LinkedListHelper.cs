using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
    public static class LinkedListHelper
    {
        public static void SelfSortAscending<TSource, TKey>(this LinkedList<TSource> linkedList, Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>(linkedList);
            linkedList.Clear();
            IEnumerable<TSource> orderedEnumerable = tempLinkedList.OrderBy(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => linkedList.AddLast(value));
        }
        public static void SelfSortDescending<TSource, TKey>(this LinkedList<TSource> linkedList, Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>(linkedList);
            linkedList.Clear();
            IEnumerable<TSource> orderedEnumerable = tempLinkedList.OrderByDescending(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => linkedList.AddLast(value));
        }

        public static LinkedList<TSource> SortedDescending<TSource, TKey>(this LinkedList<TSource> list,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>();
            IEnumerable<TSource> orderedEnumerable = list.OrderByDescending(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => tempLinkedList.AddLast(value));

            return tempLinkedList;
        }

        public static LinkedList<TSource> SortedAscending<TSource, TKey>(this LinkedList<TSource> list,
            Func<TSource, TKey> keySelector)
        {
            LinkedList<TSource> tempLinkedList = new LinkedList<TSource>();
            IEnumerable<TSource> orderedEnumerable = list.OrderBy(keySelector).AsEnumerable();
            orderedEnumerable.ForEach(value => tempLinkedList.AddLast(value));

            return tempLinkedList;
        }
    }
}
