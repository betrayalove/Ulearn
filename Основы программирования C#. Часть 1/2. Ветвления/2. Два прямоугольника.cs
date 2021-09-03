// Вставьте сюда финальное содержимое файла RectanglesTask.cs

using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var upperSide = r1.Top > r2.Top + r2.Height;
            var underSide = r2.Top > r1.Top + r1.Height;
            var leftSide = r1.Left > r2.Left + r2.Width;
            var rightSide = r2.Left > r1.Left + r1.Width;
            return !(upperSide || underSide || leftSide || rightSide);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                var minRight12 = Math.Min(r1.Right, r2.Right);
                var maxLeft12 = Math.Max(r1.Left, r2.Left);
                var minBottom12 = Math.Min(r1.Bottom, r2.Bottom);
                var maxTop12 = Math.Max(r1.Top, r2.Top);
                var xIntersection = Math.Max(minRight12 - maxLeft12, 0);
                var yIntersection = Math.Max(minBottom12 - maxTop12, 0);
                return xIntersection * yIntersection;
            }
            return 0;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (CompareRectangles(r1, r2)) return 0;
            if (CompareRectangles(r2, r1)) return 1;
            return -1;
        }

        public static bool CompareRectangles(Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left) && (r1.Right <= r2.Right) && (r1.Top >= r2.Top) && (r1.Bottom <= r2.Bottom);
        }
    }
}