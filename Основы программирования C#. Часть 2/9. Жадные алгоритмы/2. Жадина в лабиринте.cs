// Вставьте сюда финальное содержимое файла GreedyPathFinder.cs

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var chests = new HashSet<Point>(state.Chests);
            var pathFinder = new DijkstraPathFinder();
            var cost = 0;
            var position = state.Position;
            var result = new List<Point>();

            for (var i = 0; i < state.Goal; i++)
            {
                var path = pathFinder.GetPathsByDijkstra(state, position, chests).FirstOrDefault();
                if (path == null) 
                    return new List<Point>();
                position = path.End;
                cost += path.Cost;
                if (state.Energy < cost) 
                    return new List<Point>();
                chests.Remove(path.End);
                result = result.Concat(path.Path.Skip(1)).ToList();
            }

            return result;
        }
    }
}