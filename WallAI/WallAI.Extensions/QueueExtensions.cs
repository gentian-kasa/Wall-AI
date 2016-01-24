using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.Extensions
{
    public static class QueueExtensions
    {
        public static void EnqueueAll<TElem>(this Queue<TElem> queue, IEnumerable<TElem> items)
        {
            foreach(var item in items)
            {
                queue.Enqueue(item);
            }
        }
    }
}
