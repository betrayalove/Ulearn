// Вставьте сюда финальное содержимое файла DijkstraPathFinder.cs

using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
    public class DijkstraData
    {
        public int Price;
        public Point? Previous;
    }

    public class DijkstraPathFinder
    {
        private static readonly List<Point> IncidentNodes = new List<Point>
        {
            new Point(0, 1),
            new Point(1, 0),
            new Point(0, -1),
            new Point(-1, 0)
        };

        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var chests = new HashSet<Point>(targets);
            var contendersToOpen = new HashSet<Point> {start};
            var visitedNodes = new HashSet<Point>();
            var track = new Dictionary<Point, DijkstraData> {[start] = new DijkstraData { Price = 0, Previous = null }};
            const int bestCost = int.MaxValue;
            while (true)
            {
                var toOpen = GetMinToOpen(contendersToOpen, track, bestCost, null); 
                if (toOpen == null) 
                    yield break;
                if (chests.Contains(toOpen.Value)) 
                    yield return GetPath(track, toOpen.Value);
                var nodes = GetIncidentNodes(toOpen.Value, state);
                foreach (var incidentNode in nodes)
                {
                    var price = track[toOpen.Value].Price + state.CellCost[incidentNode.X, incidentNode.Y];
                    if (!visitedNodes.Contains(incidentNode)) contendersToOpen.Add(incidentNode);
                    if (!track.ContainsKey(incidentNode)) 
                        track[incidentNode] = new DijkstraData { Previous = toOpen, Price = price };
                }
                contendersToOpen.Remove(toOpen.Value);
                visitedNodes.Add(toOpen.Value);
            }
        }

        private static Point? GetMinToOpen(IEnumerable<Point> contendersToOpen, 
            IReadOnlyDictionary<Point, DijkstraData> track, int bestCost, Point? toOpen)
        {
            foreach (var contender in contendersToOpen.Where(key => track[key].Price < bestCost))
            {
                bestCost = track[contender].Price;
                toOpen = contender;
            }

            return toOpen;
        }

        public PathWithCost GetPath(Dictionary<Point, DijkstraData> track, Point end)
        {
            var result = new List<Point>();
            Point? currentPoint = end;

            while (currentPoint != null)
            {
                result.Add(currentPoint.Value);
                currentPoint = track[currentPoint.Value].Previous;
            }
            result.Reverse();
            return new PathWithCost(track[end].Price, result.ToArray());
        }

        public IEnumerable<Point> GetIncidentNodes(Point node, State state)
        {
            return IncidentNodes
                .Select(point => point + (Size)node)
                .Where(point => state.InsideMap(point) && !state.IsWallAt(point));
        }
    }
}