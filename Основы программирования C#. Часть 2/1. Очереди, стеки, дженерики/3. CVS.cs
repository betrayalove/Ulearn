// Вставьте сюда финальное содержимое файла CloneVersionSystem.cs

// Last на Peek поменяй

using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        readonly Dictionary<int, Clone> clones = new Dictionary<int, Clone>();

        public string Execute(string query)
        {
            var command = query.Split();
            var step = command[0];
            var ci = int.Parse(command[1]);
            if (!clones.ContainsKey(ci)) clones[ci] = new Clone();
            if (step == "learn") clones[ci].Learn(command[2]);
            else if (step == "rollback") clones[ci].Rollback();
            else if (step == "relearn") clones[ci].Relearn();
            else if (step == "clone") clones.Add(clones.Count + 1, clones[ci].CreateClone());
            else if (step == "check") return clones[ci].Check();
            return null;
        }
    }

    public class Clone
    {
        public Stack<string> LearnHistory;
        public Stack<string> RollbackHistory;

        public Clone()
        {
            LearnHistory = new Stack<string>();
            RollbackHistory = new Stack<string>();
        }

        public void Learn(string pi)
        {
            LearnHistory.Push(pi);
            RollbackHistory.Clear();
        }

        public void Rollback()
        {
            RollbackHistory.Push(LearnHistory.Last.Value);
            LearnHistory.Pop();
        }

        public void Relearn()
        {
            LearnHistory.Push(RollbackHistory.Last.Value);
            RollbackHistory.Pop();
        }

        public Clone CreateClone()
        {
            var clone = new Clone
            {
                RollbackHistory = RollbackHistory.Clone(),
                LearnHistory = LearnHistory.Clone()
            };
            return clone;
        }

        public string Check()
        {
            if (LearnHistory.Count != 0) return LearnHistory.Last.Value;
            return "basic";
        }
    }

    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Previous { get; set; }

        public StackItem(T value, StackItem<T> previous)
        {
            Value = value;
            Previous = previous;
        }
    }

    public class Stack<T>
    {
        public Stack<T> Clone() => new Stack<T> { Last = Last, Count = Count };
        public int Count { get; private set; }
        public StackItem<T> Last { get; private set; }

        public void Push(T value)
        {
            Last = new StackItem<T>(value, Last);
            Count++;
        }

        public void Pop()
        {
            Last = Last.Previous;
            Count--;
        }

        public void Clear()
        {
            Last = null;
            Count = 0;
        }
    }
}