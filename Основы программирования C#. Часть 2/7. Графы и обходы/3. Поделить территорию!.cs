// Вставьте сюда финальное содержимое файла RivalsTask.cs

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
    public class RivalsTask
    {
        public static List<Point> IncidentPoints = new List<Point>()
        {
            new Point(0, 1),
            new Point(0, -1),
            new Point(1, 0),
            new Point(-1, 0)
        };

        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<Tuple<Point, int, int>>();
            for (var i = 0; i < map.Players.Length; i++)
                queue.Enqueue(Tuple.Create(new Point(map.Players[i].X, map.Players[i].Y), i, 0));
            var visited = new HashSet<Point>();

            while (queue.Count != 0)
            {
                var (point, owner, distance) = queue.Dequeue();
                if (!map.InBounds(point) || map.Maze[point.X, point.Y] == MapCell.Wall || visited.Contains(point))
                    continue;
                visited.Add(point);
                yield return new OwnedLocation(owner, new Point(point.X, point.Y), distance);
                foreach (var p in IncidentPoints)
                {
                     queue.Enqueue(Tuple.Create(new Point
                        { X = point.X + p.X, Y = point.Y + p.Y }, owner, distance + 1));
                }
            }
        }
    }
}