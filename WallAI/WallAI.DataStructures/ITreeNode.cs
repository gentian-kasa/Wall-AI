using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    /// <summary>
    /// The node of a generic tree.
    /// </summary>
    public interface ITreeNode<TElem> : INode<TElem>
    {
        /// <summary>
        /// Get or Set the parent of this ITreeNode.
        /// </summary>
        ITreeNode<TElem> Parent { get; set; }

        /// <summary>
        /// Get or Set the children of this ITreeNode.
        /// </summary>
        IEnumerable<ITreeNode<TElem>> Children { get; set; }

        /// <summary>
        /// Add a child ITreeNode to this ITreeNode.
        /// </summary>
        void Add(ITreeNode<TElem> child);
    }
}
