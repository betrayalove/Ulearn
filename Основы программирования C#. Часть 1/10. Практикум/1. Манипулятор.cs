// Вставьте сюда финальное содержимое файла AnglesToCoordinatesTask.cs

using System;
using System.Drawing;
using static System.Math;
using static Manipulation.Manipulator;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var upperArm = Manipulator.UpperArm;
            var forearm = Manipulator.Forearm;
            var palm = Manipulator.Palm;
			
            var elbowX = upperArm * Cos(shoulder);
            var elbowY = upperArm * Sin(shoulder);
            var elbowPos = new PointF((float)elbowX, (float)elbowY);

            var wristX = forearm * Cos(shoulder + Math.PI + elbow) + elbowX;
            var wristY = forearm * Sin(shoulder + Math.PI + elbow) + elbowY;
            var wristPos = new PointF((float)wristX, (float)wristY);

            var palmX = palm * Cos(shoulder + Math.PI + elbow + Math.PI + wrist) + wristX;
            var palmY = palm * Sin(shoulder + Math.PI + elbow + Math.PI + wrist) + wristY;
            var palmEndPos = new PointF((float)palmX, (float)palmY);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, -(Manipulator.Forearm),
            Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, Math.PI, 3 * Math.PI, 0, Manipulator.Forearm + Manipulator.UpperArm + Manipulator.Palm)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(GetDistance(joints[0], new PointF(0, 0)), UpperArm);
            Assert.AreEqual(GetDistance(joints[0], joints[1]), Forearm);
            Assert.AreEqual(GetDistance(joints[1], joints[2]), Palm);
        }

        public double GetDistance(PointF point1, PointF point2)
        {
            var variableX = (point1.X - point2.X) * (point1.X - point2.X);
            var variableY = (point1.Y - point2.Y) * (point1.Y - point2.Y);
            return Sqrt(variableX + variableY);
        }
    }
}