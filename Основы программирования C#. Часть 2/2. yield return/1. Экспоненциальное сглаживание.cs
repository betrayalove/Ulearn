// Вставьте сюда финальное содержимое файла ExpSmoothingTask.cs

using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            var check = false;
            var pastValue = 0.0d;
            foreach (var point in data)
            {
                var item = new DataPoint(point);
                if (check) item = point.WithExpSmoothedY(pastValue + alpha * (point.OriginalY - pastValue));
                else
                {
                    item = point.WithExpSmoothedY(point.OriginalY);
                    check = true;
                }
                yield return item;
                pastValue = item.ExpSmoothedY;
            }
        }
    }
}