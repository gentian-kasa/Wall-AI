using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    public class TreeNodeElementBFEnumerator<TElem> : IEnumerator<TElem>
    {
        private TreeNodeBFEnumerator<TElem> _backgroundEnumerator;

        public TElem Current => _backgroundEnumerator.Current.Element;

        object IEnumerator.Current => _backgroundEnumerator.Current.Element;

        public TreeNodeElementBFEnumerator(ITreeNode<TElem> startingPoint)
            : this(startingPoint, -1)
        { }

        public TreeNodeElementBFEnumerator(ITreeNode<TElem> startingPoint,
                                           int depthLimit)
        {
            _backgroundEnumerator = new TreeNodeBFEnumerator<TElem>(startingPoint, depthLimit);
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
