// Вставьте сюда финальное содержимое файла DiggerTask.cs

using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 2;

        public bool DeadInConflict(ICreature conflictedObject) => true;

        public CreatureCommand Act(int x, int y) => new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
    }

    public class Player : ICreature
    {
        public static int X = 0;
        public static int Y = 0;
        private static int dX = 0;
        private static int dY = 0;

        public CreatureCommand Act(int x, int y)
        {
            dY = 0;
            dX = 0;
            if (Game.KeyPressed == Keys.Left)
                dX--;
            else if (Game.KeyPressed == Keys.Up)
                dY--;
            else if (Game.KeyPressed == Keys.Right)
                dX++;
            else if (Game.KeyPressed == Keys.Down)
                dY++;
            else Stay();

            if ((!(x + dX >= 0 && x + dX < Game.MapWidth && y + dY >= 0 && y + dY < Game.MapHeight)) ||
                (Game.Map[x + dX, y + dY] != null) && (Game.Map[x + dX, y + dY] is Sack))
                Stay();
            return new CreatureCommand() { DeltaX = dX, DeltaY = dY };
        }

        private static void Stay()
        {
            dX = 0;
            dY = 0;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Digger.png";
    }

    public class Sack : ICreature
    {
        public string GetImageFileName() => "Sack.png";

        public int GetDrawingPriority() => 3;

        public int FallingTime = 0;

        public CreatureCommand Act(int x, int y)
        {
            var result = new CreatureCommand { DeltaX = 0, DeltaY = 1, TransformTo = this };

            if (CanFallTo(x + result.DeltaX, y + result.DeltaY))
                ++FallingTime;
            else
            {
                if (FallingTime > 1)
                    result.TransformTo = new Gold();
                FallingTime = 0;
                result.DeltaY = 0;
            }

            return result;
        }

        private bool CanFallTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y)
                return false;
            var cell = Game.Map.GetValue(x, y);
            return cell == null || IsFalling() && cell is Player || cell is Monster;
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;

        public bool IsFalling() => FallingTime > 0;
    }


    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y) => new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

        public bool DeadInConflict(ICreature conflictedObject) =>
            conflictedObject is Player || conflictedObject is Monster;

        public int GetDrawingPriority() => 3;

        public string GetImageFileName() => "Gold.png";
    }


    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var dx = 0;
            var dy = 0;
            if (IsPlayerAlive())
                if (Player.X == x)
                {
                    if (Player.Y < y) dy--;
                    else if (Player.Y > y) dy++;
                }
                else
                {
                    if (Player.X < x) dx--;
                    else if (Player.X > x) dx++;
                }
            else return Stay();

            var map = Game.Map[x + dx, y + dy];
            if ((map != null) && (map is Terrain || map is Sack || map is Monster))
                return Stay();
            return new CreatureCommand() { DeltaX = dx, DeltaY = dy };
        }

        public bool DeadInConflict(ICreature conflictedObject) =>
            conflictedObject is Sack || conflictedObject is Monster;

        public int GetDrawingPriority() => 0;

        public string GetImageFileName() => "Monster.png";

        private static CreatureCommand Stay() => new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

        private static bool IsPlayerAlive()
        {
            for (var i = 0; i < Game.MapWidth; i++)
                for (var j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j] != null)
                        if (Game.Map[i, j] is Player)
                        {
                            Player.X = i;
                            Player.Y = j;
                            return true;
                        }

            return false;
        }
    }
}