// Вставьте сюда финальное содержимое файла SobelFilterTask.cs

using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var sy = GetTransposeMatrix(sx);
            var image = new double[width, height];
            var halfGradientMatrix = sx.GetLength(0) / 2;
            for (var x = halfGradientMatrix; x < width - halfGradientMatrix; x++)
                for (var y = halfGradientMatrix; y < height - halfGradientMatrix; y++)
                {
                    var gx = GetСonvolution(g, sx, x, y, halfGradientMatrix);
                    var gy = GetСonvolution(g, sy, x, y, halfGradientMatrix);
                    image[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return image;
        }

        public static double[,] GetTransposeMatrix(double[,] sx)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            var sy = new double[height, width];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    sy[y, x] = sx[x, y];
            return sy;
        }

        public static double GetСonvolution(double[,] g, double[,] array, int x, int y, int halfGradientMatrix)
        {
            var arrayWidth = array.GetLength(0);
            var arrayLength = array.GetLength(1);
            var amount = 0.0;
            for (var i = 0; i < arrayWidth; i++)
                for (var j = 0; j < arrayLength; j++)
                    amount += array[i, j] * g[x + i - halfGradientMatrix, y + j - halfGradientMatrix];
            return amount;
        }
    }
}