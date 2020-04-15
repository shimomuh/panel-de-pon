using System;
using System.Collections.Generic;

namespace SupportDomain
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        public static List<T> DeepCopyAndAddAndReturn<T>(this IList<T> list, T t) where T : IComparable<T>
        {
            var result = new List<T>(list);
            result.Add(t);
            return result;
        }

        public static List<T> DeepCopyAndAddAndReturn<T>(this IList<T> list, IList<T> ts) where T : IComparable<T>
        {
            var result = new List<T>(list);
            foreach (var t in ts)
            {
                result.Add(t);
            }
            return result;
        }

        public static T RandomTake<T>(this IList<T> list) where T : IComparable<T>
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        public static IList<T> Extract<T>(this IList<T> list, T t) where T : IComparable<T>
        {
            List<T> result = new List<T>();
            foreach (T item in list)
            {
                if (item.Equals(t))
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }

        public static IList<T> Extract<T>(this IList<T> list, List<T> ts) where T : IComparable<T>
        {
            List<T> result = new List<T>();
            foreach (T item in list)
            {
                bool isContain = false;
                foreach (T t in ts)
                {
                    if (item.Equals(t))
                    {
                        isContain = true;
                        break;
                    }
                }
                if (isContain)
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }
    }
}