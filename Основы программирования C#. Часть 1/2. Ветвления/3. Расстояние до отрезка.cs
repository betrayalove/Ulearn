// Вставьте сюда финальное содержимое файла DistanceTask.cs

using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if (DistanceToPoint(x, y, ax, ay) <= (DistanceToPoint(x, y, bx, by)))
            {
                var angle = CalculateAngle(x, y, ax, ay, bx, by);
                if (Math.PI / 2 >= angle)
                    return DistanceToPoint(x, y, ax, ay) * Math.Sin(angle);
                return DistanceToPoint(x, y, ax, ay);
            }
            var angle = CalculateAngle(x, y, bx, by, ax, ay);
            if (Math.PI / 2 >= angle)
                return DistanceToPoint(x, y, bx, by) * Math.Sin(angle);
            return DistanceToPoint(x, y, bx, by);
        }

        static double CalculateAngle(double x, double y, double ax, double ay, double bx, double by)
        {
            var lengthAB = Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));
            var lengthXA = Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
            return Math.Acos((((ax - bx) * (ax - x)) + ((ay - by) * (ay - y))) / (lengthAB * lengthXA));
        }

        static double DistanceToPoint(double x, double y, double aX, double aY)
        {
            return Math.Sqrt((aX - x) * (aX - x) + (aY - y) * (aY - y));
        }
    }
}