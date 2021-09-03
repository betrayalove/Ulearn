// Вставьте сюда финальное содержимое файла TriangleTask.cs

using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if (a > b + c || b > a + c || c > a + b)
                return Double.NaN;
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(4, 5, 3, 0.6435)]
        [TestCase(3, 5, 4, 0.9273)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(0, 0, 0, Double.NaN)]
        [TestCase(2, 5, 10, Double.NaN)]
        [TestCase(10, 20, 5, Double.NaN)]
        [TestCase(1, 8, 30, Double.NaN)]
        [TestCase(2, 8, 18, Double.NaN)]
        [TestCase(23, 1, 30, Double.NaN)]
        [TestCase(30, 8, 3, Double.NaN)]
        [TestCase(1, 2, 3, Math.PI)]
        [TestCase(3, 2, 1, 0)]
        [TestCase(1, 3, 2, 0)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var angle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, angle, 1e-4);
        }
    }
}