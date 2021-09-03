// Вставьте сюда финальное содержимое файла PathFinderTask.cs

using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var length = checkpoints.Length;
            var order = new int[length];
            var bestOrder = new int[length];
            for (var i = 0; i < length; i++)
                bestOrder[i] = i;
            MakeTrivialPermutation(checkpoints, order, bestOrder, 1, 0,
                checkpoints.GetPathLength(bestOrder));
            return bestOrder;
        }

        static double MakeTrivialPermutation(Point[] checkpoints, int[] order, int[] bestOrder, int position,
            double size, double bestLength, double newLength = 0.0)
        {
            if (newLength < bestLength)
            {
                if (position == checkpoints.Length)
                {
                    order.CopyTo(bestOrder, 0);
                    return bestLength;
                }
                for (var i = 1; i < checkpoints.Length; i++)
                {
                    var index = Array.IndexOf(order, i, 0, position);
                    if (index != -1)
                        continue;
                    order[position] = i;
                    newLength = size + PointExtensions.DistanceTo(checkpoints[order[position - 1]],
                        checkpoints[order[position]]);
                    if (newLength < bestLength)
                        newLength = MakeTrivialPermutation(checkpoints, order, bestOrder, position + 1, newLength,
                             checkpoints.GetPathLength(bestOrder));
                }
            }
            return bestLength;
        }
    }
}