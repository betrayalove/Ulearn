// Вставьте сюда финальное содержимое файла ReadonlyBytes.cs

using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable
    {
        private readonly byte[] collection;
        private int hash = -1;

        public ReadonlyBytes(params byte[] args) => collection = args ?? throw new ArgumentNullException();

        public int Length => collection.Length;

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)collection).GetEnumerator();
		
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public byte this[int index] => collection[index];

        public override bool Equals(object obj) =>
            obj != null && GetHashCode() == obj.GetHashCode() && obj.GetType() == GetType();

        public override string ToString() => $"[{string.Join(", ", collection)}]";

        public override int GetHashCode()
        {
            unchecked
            {
                if (hash != -1) return hash;
                hash = 1;
                foreach (var collectionItem in collection)
                {
                    hash *= 24214217;
                    hash -= collectionItem;
                }
                return hash;
            }
        }
    }
}