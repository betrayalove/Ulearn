// Вставьте сюда финальное содержимое файла ThresholdFilterTask.cs

using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double threshold)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var image = new double[width, height];
            var thresholdValue = GetThresholdValue(original, threshold, width, height);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (original[x, y] >= thresholdValue)
                        image[x, y] = 1;
                    //else
                        //image[x, y] = 0; вроде, это не надо
                }
            }
            return image;
        }

        static double GetThresholdValue(double[,] original, double threshold, int width, int height)
        {
            double thresholdValue; //Вроде, отсюда легку убирается thresholdValue, как минимум в конце
            var amountOfWhite = (int)(threshold * original.Length);
            var list = new List<double>();
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    list.Add(original[x, y]);
            list.Sort();
            if (amountOfWhite == 0)
                thresholdValue = int.MaxValue;
            else
                thresholdValue = list[original.Length - amountOfWhite];
            return thresholdValue;
        }
    }
}