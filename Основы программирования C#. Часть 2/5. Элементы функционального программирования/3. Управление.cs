// Вставьте сюда финальное содержимое файла ControlTask.cs

using System;

namespace func_rocket
{
    public class ControlTask
    {
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var distance = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y);
            var angleA = distance.Angle - rocket.Direction;
            var angleB = distance.Angle - rocket.Velocity.Angle;
            var totalAngle = angleA;
			
            if (Math.Abs(angleA) < 0.6 || Math.Abs(angleB) < 0.6)
                totalAngle = angleA + angleB;

            if (totalAngle > 0)
                return Turn.Right;
            return totalAngle < 0 ? Turn.Left : Turn.None;
        }
    }
}