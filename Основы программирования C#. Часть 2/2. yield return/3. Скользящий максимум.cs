// Вставьте сюда финальное содержимое файла MovingMaxTask.cs

using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var list = new LinkedList<double>();
            var window = new Queue<double>();
            foreach (var point in data)
            {
                window.Enqueue(point.OriginalY);
                if (window.Count > windowWidth)
                    if (window.Dequeue() >= list.First.Value) list.RemoveFirst();
                while (list.Count != 0 && point.OriginalY > list.Last.Value) 
					list.RemoveLast();
                list.AddLast(point.OriginalY);
                yield return point.WithMaxY(list.First.Value);
            }
        }
    }
}