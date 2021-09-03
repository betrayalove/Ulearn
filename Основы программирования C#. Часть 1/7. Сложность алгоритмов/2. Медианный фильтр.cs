// Вставьте сюда финальное содержимое файла MedianFilterTask.cs

//Не работает на окне шириной не равной 3

using System;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var image = new double[width, height];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    image[x, y] = GetPosition(original, x, y, width, height);
            return image;
        }

        public static double GetPosition(double[,] original, int x, int y, int width, int height)
        {
            var startX = x - 1;
            var endX = x + 1;
            var startY = y - 1;
            var endY = y + 1;
            if (x == 0)
                startX = 0;
            if (x == width - 1)
                endX = width - 1;
            if (y == 0)
                startY = 0;
            if (y == height - 1)
                endY = height - 1;
            return GetMedian(original, startX, endX, startY, endY);
        }

        public static double GetMedian(double[,] original, int startX, int endX, int startY, int endY)
        {
            var list = new List<double>();
            for (var x = startX; x <= endX; x++)
                for (var y = startY; y <= endY; y++)
                    list.Add(original[x, y]);
            list.Sort();
            if (list.Count % 2 == 0)
                return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2;
            return list[list.Count / 2];
        }
    }
}