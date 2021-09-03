// Вставьте сюда финальное содержимое файла ExtensionsTask.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        public static double Median(this IEnumerable<double> items)
        {
            var list = items.ToList();
            if (list.Count == 0)
                throw new InvalidOperationException();
            list.Sort();
            if (list.Count % 2 == 1)
                return list[list.Count / 2];
            return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2;
        }

        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var isFirst = true;
            var tempFirst = default(T);
            foreach (var item in items)
            {
                if (isFirst)
                    isFirst = false;
                else
                    yield return new Tuple<T, T>(tempFirst, item);
                tempFirst = item;
            }
        }
    }
}