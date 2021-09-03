// Вставьте сюда финальное содержимое файла NotGreedyPathFinder.cs

using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;

namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        private readonly Dictionary<Point, Dictionary<Point, PathWithCost>> paths =
            new Dictionary<Point, Dictionary<Point, PathWithCost>>();
        private List<Point> bestPath = new List<Point>();
        private int maxChests;
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var currentPath = new List<Point>();
            var pathFinder = new DijkstraPathFinder();
            var points = new List<Point>(state.Chests) { state.Position };
            foreach (var point in points)
            {
                if (paths != null && !paths.ContainsKey(point)) paths.Add(point, new Dictionary<Point, PathWithCost>());
                foreach (var path in pathFinder.GetPathsByDijkstra(state, point, state.Chests))
                {
                    if (path.Start.Equals(path.End)) continue;
                    if (paths != null) paths[path.Start][path.End] = path;
                }
            }
            foreach (var path in paths[state.Position])
            {
                var hashSet = new HashSet<Point>();
                foreach (var pair in paths[path.Key]) 
                    hashSet.Add(pair.Key);
                FindPath(state.Energy - path.Value.Cost,
                path.Key, hashSet, 1, new List<Point> { state.Position, path.Key });
            }
            for (var i = 0; i < bestPath.Count - 1; i++)
                currentPath = currentPath.Concat(paths[bestPath[i]][bestPath[i + 1]].Path.Skip(1)).ToList();
            return currentPath;
        }

        private void FindPath(int currentEnergy, Point currentPosition, IEnumerable<Point> leftoverChests,
            int takenChests, List<Point> points)
        {
            var chests = new HashSet<Point>(leftoverChests);
            chests.Remove(currentPosition);
            foreach (var point in chests)
            {
                if (paths[currentPosition][point].Cost > currentEnergy) continue;
                var newPath = new List<Point>(points) { point };
                FindPath(currentEnergy - paths[currentPosition][point].Cost,
                    point, chests, takenChests + 1, newPath);
            }
            if (takenChests <= maxChests) return;
            maxChests = takenChests;
            bestPath = points;
        }
    }
}