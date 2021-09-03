// Вставьте сюда финальное содержимое файла MovingAverageTask.cs

using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var total = 0.0d;
            var window = new Queue<double>();
            foreach (var point in data)
            {
                if (window.Count >= windowWidth) total -= window.Dequeue();
                window.Enqueue(point.OriginalY);
                total += point.OriginalY;
                var item = point.WithAvgSmoothedY(total / window.Count);
                yield return item;
            }
        }
    }
}