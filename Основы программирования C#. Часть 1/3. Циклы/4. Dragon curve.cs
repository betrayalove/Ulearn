// Вставьте сюда финальное содержимое файла DragonFractalTask.cs

//Меня захейтили за ref, так что :)

using System;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var x = 1.0d;
            var y = 0.0d;
            var randomSeed = new Random(seed);
            for (int i = 0; i < iterationsCount; i++)
            {
                var randomNumber = randomSeed.Next(2);
                if (randomNumber == 1)
                    SetPixelAngle(ref x, ref y, 45);
                else
                    SetPixelAngle(ref x, ref y, 135);
                pixels.SetPixel(x, y);
            }
        }

        private static void SetPixelAngle(ref double x, ref double y, int angel)
        {
            var x1 = (x * Math.Cos(Math.PI * angel / 180) - y * Math.Sin(Math.PI * angel / 180)) / Math.Sqrt(2);
            var y1 = (x * Math.Sin(Math.PI * angel / 180) + y * Math.Cos(Math.PI * angel / 180)) / Math.Sqrt(2);
            x = x1;
            if (angel == 135)
                x++;
            y = y1;
        }
    }
}