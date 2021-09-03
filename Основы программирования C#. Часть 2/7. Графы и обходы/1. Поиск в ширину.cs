// Вставьте сюда финальное содержимое файла BfsTask.cs

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var hashSet = new HashSet<Point> { start };
            var chestHashSet = chests.ToHashSet();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();

                if (chestHashSet.Contains(point.Value)) 
                    yield return point;

                foreach (var size in Walker.PossibleDirections)
                {
                    var nextPoint = new Point { X = point.Value.X + size.Width, Y = point.Value.Y + size.Height };

                    if (!map.InBounds(nextPoint) || 
                        hashSet.Contains(nextPoint) || 
                        map.Dungeon[nextPoint.X, nextPoint.Y] != MapCell.Empty)
                        continue;
                    hashSet.Add(nextPoint);
                    queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, point));
                }
            }
        }
    }
}