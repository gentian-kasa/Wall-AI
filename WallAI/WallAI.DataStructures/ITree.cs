using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    /// <summary>
    /// A generic Tree.
    /// </summary>
    public interface ITree<TElem> : IGraph<TElem>
    {
        /// <summary>
        /// Get or Set the root of the ITree.
        /// </summary>
        ITreeNode<TElem> Root { get; set; }

        /// <summary>
        /// Find the ITreeNode of the given element.
        /// </summary>
        ITreeNode<TElem> Find(TElem element);
    }
}
