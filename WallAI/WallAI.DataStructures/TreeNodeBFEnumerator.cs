using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.DataStructures
{
    public class TreeNodeBFEnumerator<TElem> : IEnumerator<ITreeNode<TElem>>
    {
        private ITreeNode<TElem> _startingPoint;
        private Queue<ITreeNode<TElem>> _queue = new Queue<ITreeNode<TElem>>();

        public ITreeNode<TElem> Current => _queue.Peek();

        object IEnumerator.Current => _queue.Peek();

        public TreeNodeBFEnumerator(ITreeNode<TElem> startingPoint)
        {
            _startingPoint = startingPoint;
            _queue.Enqueue(startingPoint);
        }

        public void Dispose()
        {
            _startingPoint = null;
            _queue = null;
        }

        public bool MoveNext()
        {
            var currentItem = _queue.Dequeue();
            var children = currentItem.Children.ToList();

            foreach (var child in children)
            {
                _queue.Enqueue(child);
            }

            return _queue.Count > 0;
        }

        public void Reset()
        {
            _queue.Clear();
            _queue.Enqueue(_startingPoint);
        }
    }
}
