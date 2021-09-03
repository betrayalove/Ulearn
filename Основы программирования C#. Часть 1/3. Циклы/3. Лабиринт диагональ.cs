// Вставьте сюда финальное содержимое файла DiagonalMazeTask.cs

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var numberSteps = (width - 2) / (height - 2);
            var (firstDirection, secondDirection) = (Direction.Right, Direction.Down);
            if (height > width)
            {
                numberSteps = (height - 2) / (width - 2);
                firstDirection = Direction.Down;
                secondDirection = Direction.Right;
            }
            while (!robot.Finished)
                Move(robot, numberSteps, firstDirection, secondDirection);
        }

        private static void Move(Robot robot, int stepCount, Direction longDirection, Direction shortDirection)
        {
            for (var i = 0; i < stepCount; i++)
                robot.MoveTo(longDirection);
            if (!robot.Finished)
                robot.MoveTo(shortDirection);
        }
    }
}