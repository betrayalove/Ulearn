// Вставьте сюда финальное содержимое файла Channel.cs

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> list = new List<T>();
        private readonly object obj = new object();

        public T this[int index]
        {
            get
            {
                lock (obj)
                {
                    return Count <= index ? null : list[index];
                }
            }
            set
            {
                lock (obj)
                {
                    if (Count > index)
                    {
                        list[index] = value;
                        list.RemoveRange(index + 1, list.Count - 1 - index);
                    }
                    else if (index == list.Count)
                    {
                        list.Add(value);
                    }
                }
            }
        }

        public T LastItem()
        {
            lock (obj)
            {
                return list.Count == 0 ? null : list[list.Count - 1];
            }
        }

        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (obj)
            {
                if (LastItem() == knownLastItem)
                    list.Add(item);
            }
        }

        public int Count
        {
            get
            {
                lock (obj)
                {
                    return list.Count;
                }
            }
        }
    }
}