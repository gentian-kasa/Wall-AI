using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    /// <summary>
    /// The interface of a generic node. 
    /// </summary>
    public interface INode<TElem> : ICollection<TElem>
    {
        /// <summary>
        /// Get or Set the element of the node.
        /// </summary>
        TElem Element { get; set; }
    }
}
