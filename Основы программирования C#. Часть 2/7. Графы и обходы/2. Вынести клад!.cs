// Вставьте сюда финальное содержимое файла DungeonTask.cs

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathToExit = BfsTask.FindPaths(map, map.InitialPosition,
                new[] { map.Exit }).FirstOrDefault();
            if (pathToExit == null)
                return new MoveDirection[0];
            if (map.Chests.Any(chest => pathToExit.Contains(chest)))
                return MakeDirection(pathToExit);
            var pathToChests = BfsTask.FindPaths(map,
                map.InitialPosition, map.Chests);
            var pathTwo = BfsTask.FindPaths(map, map.Exit, map.Chests);
            var paths = pathToChests.Join(pathTwo, listToChests =>
                listToChests.Value, listToExit => listToExit.Value, Tuple.Create);
            var shortestPath = FindShortestPath(paths.ToList());
            return MakeDirection(shortestPath ?? pathToExit.ToList());
        }

        public static List<Point> FindShortestPath(List<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> paths)
        {
            if (paths.Count == 0) return null;
            var minValue = int.MaxValue;
            var pathNumber = 0;

            for (var i = 0; i < paths.Count; i++)
            {
                var (pathToChests, pathToExit) = paths[i];
                var pathLength = pathToChests.Length + pathToExit.Length;
                if (pathLength >= minValue) continue;
                minValue = pathLength;
                pathNumber = i;
            }

            var (shortestToChests, shortestToExit) = paths[pathNumber];
            var result = shortestToExit.Skip(1)
                .Aggregate(shortestToChests, (current, point) =>
                    new SinglyLinkedList<Point>(point, current));
            return result.ToList();
        }

        public static MoveDirection[] MakeDirection(IEnumerable<Point> pathToExit)
        {
            var list = pathToExit
                .Reverse()
                .ToList();
            return list.Zip(list.Skip(1), (point1, point2) =>
                Walker.ConvertOffsetToDirection(new Size(point2 - (Size)point1))).ToArray();
        }
    }
}