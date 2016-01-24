using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallAI.Extensions;

namespace WallAI.DataStructures
{
    public class TreeNodeBFEnumerator<TElem> : IEnumerator<ITreeNode<TElem>>
    {
        private ITreeNode<TElem> _startingPoint;
        private Queue<Tuple<int, ITreeNode<TElem>>> _queue
            = new Queue<Tuple<int, ITreeNode<TElem>>>();

        private ITreeNode<TElem> _currentNode;
        public ITreeNode<TElem> Current => _currentNode;
        object IEnumerator.Current => _currentNode;

        public int DepthLimit { get; private set; }

        public TreeNodeBFEnumerator(ITreeNode<TElem> startingPoint)
        {
            _startingPoint = startingPoint;
            InitializeQueue();
            DepthLimit = -1;
        }

        public TreeNodeBFEnumerator(ITreeNode<TElem> startingPoint, int depthLimit)
            : this(startingPoint)
        {
            DepthLimit = depthLimit;
        }

        public void Dispose()
        {
            _startingPoint = null;
            _queue = null;
            _currentNode = null;
        }

        public bool MoveNext()
        {
            if(_queue.Count == 0)
            {
                return false;
            }

            var currentItem = _queue.Dequeue();
            var currentDepth = currentItem.Item1;
            _currentNode = currentItem.Item2;

            if(currentDepth != DepthLimit)
            {
                var itemsToEnqueue = _currentNode
                    .Children
                    .Select(child => Tuple.Create(currentDepth + 1, child));
                _queue.EnqueueAll(itemsToEnqueue);
            }

            return true;
        }

        public void Reset()
        {
            _queue.Clear();
            InitializeQueue();            
        }

        private void InitializeQueue()
        {
            var firstItem = Tuple.Create(0, _startingPoint);
            _queue.Enqueue(firstItem);
        }
    }
}
