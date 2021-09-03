// Вставьте сюда финальное содержимое файла SnakeMazeTask.cs

namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                Move(robot, width - 3, Direction.Right);
                Move(robot, 2, Direction.Down);
                Move(robot, width - 3, Direction.Left);
                if (!robot.Finished)
                    Move(robot, 2, Direction.Down);
            }
        }

        private static void Move(Robot robot, int width, Direction direction)
        {
            for (var i = 0; i < width; i++)
                robot.MoveTo(direction);
        }
    }
}