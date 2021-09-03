// Вставьте сюда финальное содержимое файла GrayscaleTask.cs

namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var image = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var r = original[x, y].R;
                    var g = original[x, y].G;
                    var b = original[x, y].B;
                    image[x, y] = (0.299 * r + 0.587 * g + 0.114 * b) / 255;
                }
            }
            return image;
        }
    }
}