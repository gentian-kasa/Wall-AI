using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WallAI.DataStructures;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestTreeCreationWithoutRoot()
        {
            var tree = new Tree<int>();

            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void TestTreeCreationWithRoot()
        {
            
        }
    }
}
