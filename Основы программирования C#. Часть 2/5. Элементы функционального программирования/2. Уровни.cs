// Вставьте сюда финальное содержимое файла LevelsTask.cs

using System;
using System.Collections.Generic;

namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Physics standardPhysics = new Physics();

        public static IEnumerable<Level> CreateLevels() => Levels();

        private static IEnumerable<Level> Levels()
        {
            var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
            var target = new Vector(700, 500);
            Gravity zero = (size, v) => Vector.Zero;
            Gravity heavy = (size, v) => new Vector(0, 0.9);
            Gravity up = (size, v) => new Vector(0, -300 / (300 + size.Height - v.Y));
            Gravity whiteGravity = (size, v) =>
            {
                var direction = target - v;
                return direction.Normalize() * -140 * direction.Length / (direction.Length * direction.Length + 1);
            };
            Gravity blackGravity = (size, v) =>
            {
                var direction = (target + rocket.Location) * 0.5 - v;
                return direction.Normalize() * 300 * direction.Length / (direction.Length * direction.Length + 1);
            };
            Gravity mixedGravity = (size, v) => (whiteGravity(size, v) + blackGravity(size, v)) * 0.5;
            yield return new Level("WhiteHole", rocket, target, whiteGravity, standardPhysics);
            yield return new Level("BlackHole", rocket, target, blackGravity, standardPhysics);
            yield return new Level("BlackAndWhite", rocket, target, mixedGravity, standardPhysics);
            yield return new Level("Zero", rocket, target, zero, standardPhysics);
            yield return new Level("Heavy", rocket, target, heavy, standardPhysics);
            yield return new Level("Up", rocket, target, up, standardPhysics);
        }
    }
}