// Вставьте сюда финальное содержимое файла LimitedSizeStack.cs

using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private readonly int limit;
        private readonly LinkedList<T> list = new LinkedList<T>();
        public LimitedSizeStack(int limit) => this.limit = limit;

        public void Push(T item)
        {
            list.AddLast(item);
            if (list.Count > limit)
                list.RemoveFirst();
        }

        public T Pop()
        {
            var value = list.Last.Value;
            list.RemoveLast();
            return value;
        }

        public int Count => list.Count;
    }
}