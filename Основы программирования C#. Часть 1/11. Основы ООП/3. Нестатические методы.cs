// Вставьте сюда финальное содержимое файла VectorTask.cs

using System;

namespace GeometryTasks
{
    public class Segment
    {
		public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector v)
        {
            return Geometry.IsVectorInSegment(v, this);
        }
		
        public Vector Begin;
        public Vector End;
    }

    public class Vector
    {
		public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
		
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
            var segmentLengthX = segment.End.X - segment.Begin.X;
            var segmentLengthY = segment.End.Y - segment.Begin.Y;
            return Math.Sqrt(segmentLengthX * segmentLengthX + segmentLengthY * segmentLengthY);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var segment1 = new Segment();
            var segment2 = new Segment();
            segment1.Begin = segment.Begin;
            segment1.End = vector;
            segment2.Begin = vector;
            segment2.End = segment.End;
            return GetLength(segment) == (GetLength(segment1) + GetLength(segment2));
        }
    }
}