// Вставьте сюда финальное содержимое файла ListModel.cs

using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit { get; }
        private readonly LimitedSizeStack<Tuple<ActionType, int, TItem>> stack;

        public enum ActionType
        {
            AddItem,
            RemoveItem
        }

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stack = new LimitedSizeStack<Tuple<ActionType, int, TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            stack.Push(Tuple.Create(ActionType.AddItem, Items.Count, item));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            stack.Push(Tuple.Create(ActionType.RemoveItem, index, Items[index]));
            Items.RemoveAt(index);
        }

        public bool CanUndo() => stack.Count > 0;

        public void Undo()
        {
            var (itemFirst, itemSecond, itemThird) = stack.Pop();
            if (itemFirst == ActionType.AddItem) Items.RemoveAt(stack.Count);
            else if (itemFirst == ActionType.RemoveItem)
            {
                if (stack.Count == 1) Items.Insert(itemSecond - 1, itemThird);
                else Items.Insert(itemSecond, itemThird);
            }
        }
    }
}