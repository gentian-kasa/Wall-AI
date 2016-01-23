using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace WallAI.DataStructures
{
    /// <summary>
    /// The base class of a tree.
    /// </summary>
    public class Tree<TElem> : ITree<TElem>
    {
        // Keep track of nodes and if they are leafs or not
        private readonly Dictionary<ITreeNode<TElem>, bool> _nodes
            = new Dictionary<ITreeNode<TElem>, bool>();
        // Keep track of the elements present in the various nodes
        private readonly Dictionary<TElem, ICollection<ITreeNode<TElem>>> _elements
            = new Dictionary<TElem, ICollection<ITreeNode<TElem>>>();

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public int Count
        {
            get { return _nodes.Count; }
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <inheritdoc cref="ITree{TElem}"></inheritdoc>
        public ITreeNode<TElem> Root
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public void Add(TElem item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public bool Contains(TElem item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public void CopyTo(TElem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ITree{TElem}"></inheritdoc>
        public ITreeNode<TElem> Find(TElem element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITreeNode<TElem>> FindAll(TElem element)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public IEnumerator<TElem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        public bool Remove(TElem item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ICollection{T}"></inheritdoc>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
