// Вставьте сюда финальное содержимое файла BilliardsTask.cs

using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return 2 * wallInclinationRadians - directionRadians;
        }
    }
}