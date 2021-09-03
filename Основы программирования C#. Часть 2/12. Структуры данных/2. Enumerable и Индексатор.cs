// Вставьте сюда финальное содержимое файла BinaryTree.cs

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class TreeNode<T>
    {
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public T Value { get; }

        public TreeNode(T value)
        {
            Value = value;
        }

        public int Size = 1;
    }


    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private TreeNode<T> treeNode;

        public void Add(T key)
        {
            var newBinaryTree = treeNode;
            if (treeNode == null)
                treeNode = new TreeNode<T>(key);
            else
                while (true)
                {
                    newBinaryTree.Size++;
                    if (newBinaryTree.Value.CompareTo(key) > 0)
                    {
                        if (newBinaryTree.Left == null)
                        {
                            newBinaryTree.Left = new TreeNode<T>(key);
                            break;
                        }

                        newBinaryTree = newBinaryTree.Left;
                    }
                    else
                    {
                        if (newBinaryTree.Right == null)
                        {
                            newBinaryTree.Right = new TreeNode<T>(key);
                            break;
                        }

                        newBinaryTree = newBinaryTree.Right;
                    }
                }
        }

        public bool Contains(T key)
        {
            var newTreeNode = treeNode;
            while (newTreeNode != null)
            {
                var result = newTreeNode.Value.CompareTo(key);
                if (result == 0)
                    return true;
                newTreeNode = result > 0
                    ? newTreeNode.Left
                    : newTreeNode.Right;
            }

            return false;
        }

        public T this[int i]
        {
            get
            {
                var tree = treeNode;
                while (true)
                {
                    if (tree == null) continue;
                    var leftSize = tree.Left?.Size ?? 0;
                    if (i == leftSize)
                        return tree.Value;
                    if (i < leftSize)
                        tree = tree.Left;
                    else if (i > leftSize)
                    {
                        tree = tree.Right;
                        i -= leftSize + 1;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetValues(treeNode).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<T> GetValues(TreeNode<T> treeNode)
        {
            while (true)
            {
                if (treeNode == null) yield break;

                foreach (var value in GetValues(treeNode.Left))
                    yield return value;

                yield return treeNode.Value;

                treeNode = treeNode.Right;
            }
        }
    }
}