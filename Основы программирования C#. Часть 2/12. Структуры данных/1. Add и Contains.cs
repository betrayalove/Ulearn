// Вставьте сюда финальное содержимое файла BinaryTree.cs

using System;

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
    }


    public class BinaryTree<T> where T : IComparable
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
    }
}