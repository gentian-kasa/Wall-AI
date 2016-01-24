using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.Extensions
{
    public static class StackExtensions
    {
        public static void PushAll<TElem>(this Stack<TElem> stack, IEnumerable<TElem> elements)
        {
            foreach(var element in elements)
            {
                stack.Push(element);
            }
        }
    }
}
