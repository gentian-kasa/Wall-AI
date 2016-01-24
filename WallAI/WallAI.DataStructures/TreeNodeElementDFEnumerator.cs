using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WallAI.DataStructures
{
    public class TreeNodeElementDFSEnumerator<TElem> : IEnumerator<TElem>
    {
        private TreeNodeDFEnumerator<TElem> _backgroundEnumerator;

        public TElem Current => _backgroundEnumerator.Current.Element;
        object IEnumerator.Current => _backgroundEnumerator.Current.Element;
        
        public TreeNodeElementDFSEnumerator(ITreeNode<TElem> startingPoint)
            : this(startingPoint, -1)
        { }

        public TreeNodeElementDFSEnumerator(ITreeNode<TElem> startingPoint, int depthLimit)
        {
            _backgroundEnumerator = new TreeNodeDFEnumerator<TElem>(startingPoint, depthLimit);
        }

        public void Dispose()
        {
            _backgroundEnumerator.Dispose();
        }

        public bool MoveNext()
        {
            return _backgroundEnumerator.MoveNext();
        }

        public void Reset()
        {
            _backgroundEnumerator.Reset();
        }
    }
}
