﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSA.Algorithms.Sorting
{
    public static partial class ParallelMergeSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSort<T>(this LinkedList<T> list)
        {
            return ParallelMergeSort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSortDescending<T>(this LinkedList<T> list)
        {
            return ParallelMergeSortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSort<T>(this LinkedList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");

            return ParallelMergeSort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSortDescending<T>(this LinkedList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");

            return ParallelMergeSortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSort<T>(this LinkedList<T> list, IComparer<T> comparer)
        {
            if (list.Count > 0)
                return ParallelMergeSort(list, list.First, list.Last, comparer);
            else
                return list;
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="LinkedList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSortDescending<T>(this LinkedList<T> list, IComparer<T> comparer)
        {
            if (list.Count > 0)
                return ParallelMergeSortDescending(list, list.First, list.Last, comparer);
            else
                return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="LinkedList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSort<T>(this LinkedList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException("index");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            LinkedListNode<T> startNode = null;
            LinkedListNode<T> endNode = null;

            int curNodeIndex = 0;
            int nodesTraversedAfterStartNode = 0;
            var curNode = list.First;

            while (curNode != null)
            {
                // if we at the node on the given index save the start node
                if (curNodeIndex == index)
                    startNode = curNode;

                // if we are at the node or after the given index, increment the node counter
                if (curNodeIndex >= index)
                    nodesTraversedAfterStartNode++;

                // if we traversed enought nodes as the given count save the end node and break
                if (nodesTraversedAfterStartNode == count)
                {
                    endNode = curNode;
                    break;
                }

                // go onwards
                curNodeIndex++;
                curNode = curNode.Next;
            }

            return ParallelMergeSort(list, startNode, endNode, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="LinkedList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSortDescending<T>(this LinkedList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException("index");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            LinkedListNode<T> startNode = null;
            LinkedListNode<T> endNode = null;

            int curNodeIndex = 0;
            int nodesTraversedAfterStartNode = 0;
            var curNode = list.First;

            while (curNode != null)
            {
                // if we at the node on the given index save the start node
                if (curNodeIndex == index)
                    startNode = curNode;

                // if we are at the node or after the given index, increment the node counter
                if (curNodeIndex >= index)
                    nodesTraversedAfterStartNode++;

                // if we traversed enought nodes as the given count save the end node and break
                if (nodesTraversedAfterStartNode == count)
                {
                    endNode = curNode;
                    break;
                }

                // go onwards
                curNodeIndex++;
                curNode = curNode.Next;
            }

            return ParallelMergeSortDescending(list, startNode, endNode, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="LinkedList{T}"/> in ascending order using the specified comparer beginning from the start node and ending at the end node.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSort<T>(this LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException("startNode");
            if (endNode == null) throw new ArgumentNullException("endNode");
            if (startNode.List != list) throw new ArgumentException("startNode doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException("startNode doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            int depth = Environment.ProcessorCount;

            if (list.Last == endNode)// the end node is the last in the list
                ParallelSplitMerge(list, startNode, endNode, null, depth, comparer);// the node after it is null
            else
                ParallelSplitMerge(list, startNode, endNode, endNode.Next, depth, comparer);

            return list;
        }

        /// <summary>
        /// Parallel algorithm. In-place sorting of the given <see cref="LinkedList{T}"/> elements. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelSplitMerge<T>(LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, LinkedListNode<T> nodeAfterEndNode, int depthRemaining, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new LinkedList<T>();
            var right = new LinkedList<T>();

            var curNode = startNode;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the curent node from the list
                    left.AddLast(node);// add the saved node to the left list
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the current node from the list
                    right.AddLast(node);// add the saved node to the right list
                }
            }

            // Recursively sort both sublists
            // NOTE: node after endNode is null because in the left and right splits
            // we work with all nodes from the list.

            // if depth is low enough, we create new tasks for the spliting
            if (depthRemaining > 0)
            {
                Parallel.Invoke
                    (
                        () => ParallelSplitMerge(left, left.First, left.Last, null, depthRemaining - 1, comparer),
                        () => ParallelSplitMerge(right, right.First, right.Last, null, depthRemaining - 1, comparer)
                    );
            }
            else // if max depth level is exceeded we continue with sequential spliting
            {
                SequentialSplitMerge(left, left.First, left.Last, null, comparer);
                SequentialSplitMerge(right, right.First, right.Last, null, comparer);
            }

            // Merge the two splits into the given list
            Merge(list, left, right, nodeAfterEndNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Sequential algorithm. In-place sorting of the given <see cref="LinkedList{T}"/> elements. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialSplitMerge<T>(LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, LinkedListNode<T> nodeAfterEndNode, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new LinkedList<T>();
            var right = new LinkedList<T>();

            var curNode = startNode;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the curent node from the list
                    left.AddLast(node);// add the saved node to the left list
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the current node from the list
                    right.AddLast(node);// add the saved node to the right list
                }
            }

            // Recursively sort both sublists
            // NOTE: node after endNode is null because in the left and right splits
            // we work with all nodes from the list.
            SequentialSplitMerge(left, left.First, left.Last, null, comparer);
            SequentialSplitMerge(right, right.First, right.Last, null, comparer);

            // Merge the two splits into the given list
            Merge(list, left, right, nodeAfterEndNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Merging two lists into a given <see cref="LinkedList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="mergeList">The <see cref="LinkedList{T}"/> for merging the left and right lists into.</param>
        /// <param name="left">The left <see cref="LinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="right">The right <see cref="LinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> for the merged elements from the left <see cref="LinkedList{T}"/> and right <see cref="LinkedList{T}"/>.</returns>
        private static void Merge<T>(LinkedList<T> mergeList, LinkedList<T> left, LinkedList<T> right, LinkedListNode<T> nodeAfterEndNode, IComparer<T> comparer)
        {
            bool nodeAfterEndNodeIsNull = nodeAfterEndNode == null;

            // merging until one of the lists becomes empty
            while (left.Count > 0 && right.Count > 0)
            {
                if (comparer.Compare(left.First.Value, right.First.Value) <= 0)
                {
                    var node = left.First;
                    left.RemoveFirst();
                    if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                        mergeList.AddLast(node);// we add at the end of the list
                    else// if the end node is not the last
                        mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
                }
                else
                {
                    var node = right.First;
                    right.RemoveFirst();
                    if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                        mergeList.AddLast(node);// we add at the end of the list
                    else// if the end node is not the last
                        mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
                }
            }

            // add the remaining elements from the left or the right list
            while (left.Count > 0)
            {
                var node = left.First;
                left.RemoveFirst();
                if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                    mergeList.AddLast(node);// we add at the end of the list
                else// if the end node is not the last
                    mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
            }

            while (right.Count > 0)
            {
                var node = right.First;
                right.RemoveFirst();
                if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                    mergeList.AddLast(node);// we add at the end of the list
                else// if the end node is not the last
                    mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
            }
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="LinkedList{T}"/> in descending order using the specified comparer beginning from the start node and ending at the end node.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> when sorted.</returns>
        public static LinkedList<T> ParallelMergeSortDescending<T>(this LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException("startNode");
            if (endNode == null) throw new ArgumentNullException("endNode");
            if (startNode.List != list) throw new ArgumentException("startNode doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException("startNode doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            int depth = Environment.ProcessorCount;

            if (list.Last == endNode)// the end node is the last in the list
                ParallelSplitMergeDescending(list, startNode, endNode, null, depth, comparer);// the node after it is null
            else
                ParallelSplitMergeDescending(list, startNode, endNode, endNode.Next, depth, comparer);

            return list;
        }

        /// <summary>
        /// Parallel algorithm. In-place sorting of the given <see cref="LinkedList{T}"/> elements. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelSplitMergeDescending<T>(LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, LinkedListNode<T> nodeAfterEndNode, int depthRemaining, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new LinkedList<T>();
            var right = new LinkedList<T>();

            var curNode = startNode;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the curent node from the list
                    left.AddLast(node);// add the saved node to the left list
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the current node from the list
                    right.AddLast(node);// add the saved node to the right list
                }
            }

            // Recursively sort both sublists
            // NOTE: node after endNode is null because in the left and right splits
            // we work with all nodes from the list.

            // if depth is low enough, we create new tasks for the spliting
            if (depthRemaining > 0)
            {
                Parallel.Invoke
                    (
                        () => ParallelSplitMergeDescending(left, left.First, left.Last, null, depthRemaining - 1, comparer),
                        () => ParallelSplitMergeDescending(right, right.First, right.Last, null, depthRemaining - 1, comparer)
                    );
            }
            else // if max depth level is exceeded we continue with sequential spliting
            {
                SequentialSplitMergeDescending(left, left.First, left.Last, null, comparer);
                SequentialSplitMergeDescending(right, right.First, right.Last, null, comparer);
            }

            // Merge the two splits into the given list
            MergeDescending(list, left, right, nodeAfterEndNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Sequential algorithm. In-place sorting of the given <see cref="LinkedList{T}"/> elements. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the current split.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialSplitMergeDescending<T>(LinkedList<T> list, LinkedListNode<T> startNode, LinkedListNode<T> endNode, LinkedListNode<T> nodeAfterEndNode, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new LinkedList<T>();
            var right = new LinkedList<T>();

            var curNode = startNode;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the curent node from the list
                    left.AddLast(node);// add the saved node to the left list
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    list.Remove(node);// remove the current node from the list
                    right.AddLast(node);// add the saved node to the right list
                }
            }

            // Recursively sort both sublists
            // NOTE: node after endNode is null because in the left and right splits
            // we work with all nodes from the list.
            SequentialSplitMergeDescending(left, left.First, left.Last, null, comparer);
            SequentialSplitMergeDescending(right, right.First, right.Last, null, comparer);

            // Merge the two splits into the given list
            MergeDescending(list, left, right, nodeAfterEndNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Merging two lists into a given <see cref="LinkedList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="mergeList">The <see cref="LinkedList{T}"/> for merging the left and right lists into.</param>
        /// <param name="left">The left <see cref="LinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="right">The right <see cref="LinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="nodeAfterEndNode">The <see cref="LinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="LinkedList{T}"/> for the merged elements from the left <see cref="LinkedList{T}"/> and right <see cref="LinkedList{T}"/>.</returns>
        private static void MergeDescending<T>(LinkedList<T> mergeList, LinkedList<T> left, LinkedList<T> right, LinkedListNode<T> nodeAfterEndNode, IComparer<T> comparer)
        {
            bool nodeAfterEndNodeIsNull = nodeAfterEndNode == null;

            // merging until one of the lists becomes empty
            while (left.Count > 0 && right.Count > 0)
            {
                if (comparer.Compare(left.First.Value, right.First.Value) >= 0)
                {
                    var node = left.First;
                    left.RemoveFirst();
                    if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                        mergeList.AddLast(node);// we add at the end of the list
                    else// if the end node is not the last
                        mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
                }
                else
                {
                    var node = right.First;
                    right.RemoveFirst();
                    if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                        mergeList.AddLast(node);// we add at the end of the list
                    else// if the end node is not the last
                        mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
                }
            }

            // add the remaining elements from the left or the right list
            while (left.Count > 0)
            {
                var node = left.First;
                left.RemoveFirst();
                if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                    mergeList.AddLast(node);// we add at the end of the list
                else// if the end node is not the last
                    mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
            }

            while (right.Count > 0)
            {
                var node = right.First;
                right.RemoveFirst();
                if (nodeAfterEndNodeIsNull)// if the end node is the last in the list
                    mergeList.AddLast(node);// we add at the end of the list
                else// if the end node is not the last
                    mergeList.AddBefore(nodeAfterEndNode, node);// we add before the node which is after the end node
            }
        }
    }
}
