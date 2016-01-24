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
        ITreeNode<TElem> Parent { get; }

        /// <summary>
        /// Get or Set the children of this ITreeNode.
        /// </summary>
        IEnumerable<ITreeNode<TElem>> Children { get; }

        int Depth { get; }

        void Add(ITreeNode<TElem> child);
    }
}
