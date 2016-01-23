using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WallAI.DataStructures
{
    public class TreeNodeDFSEnumerator<TElem> : IEnumerator<ITreeNode<TElem>>
    {
        private ITreeNode<TElem> _startingPoint;
        private Stack<ITreeNode<TElem>> _stack = new Stack<ITreeNode<TElem>>();

        public ITreeNode<TElem> Current => _stack.Peek(); 

        object IEnumerator.Current => _stack.Peek(); 

        public TreeNodeDFSEnumerator(ITreeNode<TElem> startingPoint)
        {
            _startingPoint = startingPoint;
            _stack.Push(startingPoint);
        }

        public void Dispose()
        {
            _startingPoint = null;
            _stack = null;
        }

        public bool MoveNext()
        {
            var currentNode = _stack.Pop();
            var children = currentNode.Children.ToList();

            for(int i = children.Count - 1; i >= 0; i--)
            {
                _stack.Push(children[i]);
            }

            return _stack.Count > 0;
        }

        public void Reset()
        {
            _stack.Clear();
            _stack.Push(_startingPoint);
        }
    }
}
