using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    public class TreeNode<TElem> : ITreeNode<TElem>
    {
        /// <inheritdoc cref="INode{T}"></inheritdoc>
        public TElem Element { get; set; }

        /// <inheritdoc cref="ITreeNode{T}"></inheritdoc>
        public ITreeNode<TElem> Parent { get; set; }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ITreeNode<TElem>> Children
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

        /// <summary>
        /// Create a TreeNode without a parent.
        /// </summary>
        public TreeNode(TElem element) : this(element, null) { }

        /// <summary>
        /// Create a TreeNode with a specific parent.
        /// </summary>
        public TreeNode(TElem element, ITreeNode<TElem> parent)
        {
            Element = element;
            Parent = parent;
        }

        /// <inheritdoc cref="ITreeNode{T}"></inheritdoc>
        public void Add(ITreeNode<TElem> child)
        {
            child.Parent = this;
        }

        /// <inheritdoc cref="IEnumerable{T}"></inheritdoc>
        public IEnumerator<TElem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IEnumerable"></inheritdoc>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(TElem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TElem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TElem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TElem item)
        {
            throw new NotImplementedException();
        }
    }
}
