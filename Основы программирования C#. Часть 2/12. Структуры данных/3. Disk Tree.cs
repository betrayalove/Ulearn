// Вставьте сюда финальное содержимое файла DiskTreeTask.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public class Root
        {
            public string Name;
            public Dictionary<string, Root> Nodes = new Dictionary<string, Root>();

            public Root(string name)
            {
                Name = name;
            }

            public Root GetDirection(string subRoot) => 
                Nodes.TryGetValue(subRoot, out var node) 
                    ? node 
                    : Nodes[subRoot] = new Root(subRoot);

            public List<string> GetConclusion(int i, List<string> list)
            {
                if (i >= 0) 
                    list.Add(new string(' ', i) + Name);
                i++;
                return Nodes
                    .Values
                    .OrderBy(root => root.Name, StringComparer.Ordinal)
                    .Aggregate(list, (current, root) => root.GetConclusion(i, current));
            }
        }

        public static List<string> Solve(List<string> input)
        {
            var root = new Root("");
            foreach (var name in input)
                name.Split('\\')
                    .Aggregate(root, (current, subRoot) => current.GetDirection(subRoot));
            return root.GetConclusion(-1, new List<string>());
        }
    }
}