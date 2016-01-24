using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallAI.Extensions;

namespace WallAI.DataStructures
{
    public class TreeNodeDFEnumerator<TElem> : IEnumerator<ITreeNode<TElem>>
    {
        private ITreeNode<TElem> _startingPoint;
        private Stack<Tuple<int, ITreeNode<TElem>>> _stack
            = new Stack<Tuple<int, ITreeNode<TElem>>>();

        private ITreeNode<TElem> _currentNode;
        public ITreeNode<TElem> Current => _currentNode;
        object IEnumerator.Current => _currentNode;

        public int DepthLimit { get; private set; }

        public TreeNodeDFEnumerator(ITreeNode<TElem> startingPoint)
        {
            _startingPoint = startingPoint;
            InitializeStack();
            DepthLimit = -1;
        }

        public TreeNodeDFEnumerator(ITreeNode<TElem> startingPoint, int depthLimit)
            : this(startingPoint)
        {
            DepthLimit = depthLimit;
        }

        public void Dispose()
        {
            _startingPoint = null;
            _stack = null;
            _currentNode = null;
        }

        public bool MoveNext()
        {
            if(_stack.Count == 0)
            {
                return false;
            }

            var currentItem = _stack.Pop();
            var currentDepth = currentItem.Item1;
            _currentNode = currentItem.Item2;

            if(currentDepth != DepthLimit)
            {
                var itemsToPush = _currentNode
                    .Children
                    .Reverse()
                    .Select(child => Tuple.Create(currentDepth + 1, child));
                _stack.PushAll(itemsToPush);
            }

            return true;
        }

        public void Reset()
        {
            _stack.Clear();
            InitializeStack();
        }

        private void InitializeStack()
        {
            var firstItem = Tuple.Create(0, _startingPoint);
            _stack.Push(firstItem);
        }
    }
}
