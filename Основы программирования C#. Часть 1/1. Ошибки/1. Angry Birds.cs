// Вставьте сюда финальное содержимое файла AngryBirdsTask.cs

using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
		public static double FindSightAngle(double v, double distance)
		{
			var g = 9.8;
			return 0.5 * Math.Asin(distance * g / (v*v));
		}
	}
}