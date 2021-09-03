// Вставьте сюда финальное содержимое файла VectorTask.cs

using System;

namespace GeometryTasks
{
    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }

    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(new Vector
            {
                X = Math.Abs(segment.Begin.X - segment.End.X),
                Y = Math.Abs(segment.Begin.Y - segment.End.Y)
            });
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var segment1 = Geometry.GetLength(new Vector
            {
                X = segment.Begin.X - vector.X, Y = segment.Begin.Y - vector.Y
            });
            var segment2 = Geometry.GetLength(new Vector
            {
                X = vector.X - segment.End.X, Y = vector.Y - segment.End.Y
            });
            return (segment1 + segment2) == GetLength(segment);
        }
    }
}