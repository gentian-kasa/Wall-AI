using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallAI.Extensions;

namespace WallAI.DataStructures
{
    public class TreeNode<TElem> : ITreeNode<TElem>
    {
        /// <inheritdoc cref="INode{T}"></inheritdoc>
        public TElem Element { get; set; }

        private TreeNode<TElem> _parent;
        /// <inheritdoc cref="ITreeNode{T}"></inheritdoc>
        public ITreeNode<TElem> Parent => _parent;
        
        public int Count { get; private set; }

        public int Depth { get; private set; }

        public bool IsReadOnly => false;

        public readonly ICollection<TreeNode<TElem>> _children
            = new List<TreeNode<TElem>>();
        public IEnumerable<ITreeNode<TElem>> Children => _children;

        /// <summary>
        /// Create a TreeNode without a parent.
        /// </summary>
        public TreeNode(TElem element)
        {
            Element = element;
            Count = 1;
            Depth = 1;
        }

        /// <summary>
        /// Create a TreeNode with a specific parent.
        /// </summary>
        public TreeNode(TElem element, TreeNode<TElem> parent)
            : this(element)
        {
            parent.Add(this);
        }
        
        /// <inheritdoc cref="ITreeNode{T}"></inheritdoc>
        public void Add(ITreeNode<TElem> child)
        {
            if(!(child is TreeNode<TElem>))
            {
                throw new ArgumentException(
                    $"{nameof(child)} is not an instance of {typeof(TreeNode<TElem>)}.");
            }

            Add(child as TreeNode<TElem>);
        }

        public void Add(TreeNode<TElem> child)
        {
            child._parent = this;
            _children.Add(child);
            UpdateCount();
            UpdateDepth();
        }

        /// <inheritdoc cref="IEnumerable{T}"></inheritdoc>
        public IEnumerator<TElem> GetEnumerator()
        {
            return GetEnumerator(VisitType.DepthFirst);
        }

        public IEnumerator<TElem> GetEnumerator(VisitType visitType)
        {
            return GetEnumerator(visitType, -1);
        }
        
        public IEnumerator<TElem> GetEnumerator(VisitType visitType, int depth)
        {
            if(visitType == VisitType.BreadthFirst)
            {
                return new TreeNodeElementBFEnumerator<TElem>(this, depth);
            }

            return new TreeNodeElementDFSEnumerator<TElem>(this, depth);
        }

        /// <inheritdoc cref="IEnumerable"></inheritdoc>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        
        public void Add(TElem item)
        {
            Add(new TreeNode<TElem>(item));
        }

        public void Clear()
        {
            Clear(RemovalMode.Shallow);
        }

        public void Clear(RemovalMode removalMode)
        {
            if(removalMode == RemovalMode.Shallow)
            {
                ClearInShallowMode();
            }
            else
            {
                ClearInDeepMode();
            }
        }

        private void ClearInShallowMode()
        {
            _children.Clear();
        }

        private void ClearInDeepMode()
        {
            var bfEnumerator = GetEnumerator(VisitType.BreadthFirst);
            var queue = new Queue<TreeNode<TElem>>();
            TreeNode<TElem> currentChild;

            queue.EnqueueAll(_children);

            while(queue.Count > 0)
            {
                currentChild = queue.Dequeue();
                queue.EnqueueAll(currentChild._children);
                currentChild.ClearInShallowMode();
            }
        }

        public bool Contains(TElem item)
        {
            return Contains(item, SearchMode.DepthFirstSearch);
        }

        public bool Contains(TElem item, SearchMode searchMode)
        {
            return Contains(item, searchMode, Depth);
        }

        public bool Contains(TElem item, SearchMode searchMode, int depthLimit)
        {
            return FindAll(item, searchMode, depthLimit).Count() > 0;
        }

        public IEnumerable<ITreeNode<TElem>> FindAll(TElem item)
        {
            return FindAll(item, Depth);
        }

        public IEnumerable<ITreeNode<TElem>> FindAll(TElem item, int depthLimit)
        {
            return FindAll(item, SearchMode.IterativeDeepening, depthLimit);
        }

        public IEnumerable<ITreeNode<TElem>> FindAll(TElem item, SearchMode searchMode, int depthLimit)
        {
            if(searchMode == SearchMode.BreadthFirstSearch)
            {
                return FindAllWithBFS(item, depthLimit);
            }

            if(searchMode == SearchMode.DepthFirstSearch)
            {
                return FindAllWithDFS(item, depthLimit);
            }

            return FindAllWithIterativeDeepening(item, depthLimit);
        }

        private IEnumerable<ITreeNode<TElem>> FindAllWithBFS(TElem item, int depthLimit)
        {
            return FindAll(item, new TreeNodeBFEnumerator<TElem>(this, depthLimit));
        }

        private IEnumerable<ITreeNode<TElem>> FindAllWithDFS(TElem item, int depthLimit)
        {
            return FindAll(item, new TreeNodeDFEnumerator<TElem>(this, depthLimit));
        }

        private IEnumerable<ITreeNode<TElem>> FindAllWithIterativeDeepening(TElem item, int depthLimit)
        {
            var result = new Dictionary<ITreeNode<TElem>, ITreeNode<TElem>>();
            IEnumerable<ITreeNode<TElem>> currentNodes;
            
            for(int i = 1; i <= depthLimit; i++)
            {
                currentNodes = FindAll(item, new TreeNodeDFEnumerator<TElem>(this, i));
                result.AddNonPresent(
                    currentNodes.Select(
                        node => new KeyValuePair<ITreeNode<TElem>, ITreeNode<TElem>>(node, node)));
            }

            return result.Select(d => d.Key);
        }

        private IEnumerable<ITreeNode<TElem>> FindAll(TElem item, IEnumerator<ITreeNode<TElem>> enumerator)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if(enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Element.Equals(item))
                {
                    yield return enumerator.Current;
                }
            }

            yield break;
        }

        public IEnumerable<ITreeNode<TElem>> GetAllNodes()
        {
            return GetAllNodes(VisitType.BreadthFirst);
        }

        public IEnumerable<ITreeNode<TElem>> GetAllNodes(VisitType visitType)
        {
            if(visitType == VisitType.BreadthFirst)
            {
                return GetAllNodes(new TreeNodeBFEnumerator<TElem>(this));
            }

            return GetAllNodes(new TreeNodeDFEnumerator<TElem>(this));
        }
        
        public IEnumerable<ITreeNode<TElem>> GetAllNodes(IEnumerator<ITreeNode<TElem>> enumerator)
        {
            if (enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            yield break;
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public void CopyTo(TElem[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, VisitType.BreadthFirst);
        }

        public void CopyTo(TElem[] array, int arrayIndex, VisitType visitType)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            var items = GetAllNodes(visitType).ToArray();

            if (items.Length > array.Length - arrayIndex)
            {
                throw new ArgumentException(
                    $"The number of elements in this ICollection is greater than the available space from {arrayIndex} to the end of the destination array.");
            }

            Array.Copy(sourceArray: items,
                       sourceIndex: 0,
                       destinationArray: array,
                       destinationIndex: arrayIndex,
                       length: items.Length);
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public bool Remove(TElem item)
        {
            return Remove(item, VisitType.BreadthFirst);
        }

        public bool Remove(TElem item, VisitType visitType)
        {
            if(visitType == VisitType.BreadthFirst)
            {
                return Remove(item, new TreeNodeBFEnumerator<TElem>(this));
            }

            return Remove(item, new TreeNodeDFEnumerator<TElem>(this));
        }

        public bool Remove(TElem item, IEnumerator<ITreeNode<TElem>> enumerator)
        {
            if (TryRemoveFromChildren(item))
            {
                return true;
            }

            while (enumerator.MoveNext())
            {
                if ((enumerator.Current is TreeNode<TElem>) 
                    && (enumerator.Current as TreeNode<TElem>).TryRemoveFromChildren(item))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Updates this node's - and its parent's - children count. 
        /// </summary>
        private void UpdateCount()
        {
            var currentNode = this;
            
            while(currentNode != null)
            {
                currentNode.Count = currentNode.Children.Sum(child => child.Count) + 1;
                currentNode = currentNode._parent;
            }
        }

        /// <summary>
        /// Updates this node's - and its parent's - depth.
        /// </summary>
        private void UpdateDepth()
        {
            var currentNode = this;

            while(currentNode != null)
            {
                currentNode.Depth = currentNode.Children.Count() > 0
                    ? currentNode.Children.Max(child => child.Depth) + 1
                    : 0;
                currentNode = currentNode._parent;
            }
        }

        private bool TryRemoveFromChildren(TElem item)
        {
            TreeNode<TElem> child = Children.FirstOrDefault(c => c.Element.Equals(item)) as TreeNode<TElem>;

            if(child != null && !child.Equals(default(TElem)))
            {
                _children.Remove(child);
                var parent = child._parent;
                child._parent = null;
                parent.UpdateCount();
                parent.UpdateDepth();

                return true;
            }

            return false;
        }
    }
}
