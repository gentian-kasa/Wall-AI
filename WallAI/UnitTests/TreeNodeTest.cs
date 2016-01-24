using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WallAI.DataStructures;

namespace UnitTests
{
    /// <summary>
    /// Test class for TreeNode objects
    /// </summary>
    [TestClass]
    public class TreeNodeTest
    {
        public TreeNodeTest()
        {
            //
            // TODO: aggiungere qui la logica del costruttore
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ottiene o imposta il contesto del test che fornisce
        ///le informazioni e le funzionalità per l'esecuzione del test corrente.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributi di test aggiuntivi
        //
        // È possibile utilizzare i seguenti attributi aggiuntivi per la scrittura dei test:
        //
        // Utilizzare ClassInitialize per eseguire il codice prima di eseguire il primo test della classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilizzare ClassCleanup per eseguire il codice dopo l'esecuzione di tutti i test della classe
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilizzare TestInitialize per eseguire il codice prima di eseguire ciascun test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilizzare TestCleanup per eseguire il codice dopo l'esecuzione di ciascun test
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestContstructorOfOrphanTreeNode()
        {
            TreeNode<int> node = new TreeNode<int>(4);

            Assert.IsNull(node.Parent);
            Assert.AreEqual(1, node.Count);
        }

        [TestMethod]
        public void TestConstructorOfChildTreeNode()
        {
            TreeNode<int> node = new TreeNode<int>(4);
            TreeNode<int> child1 = new TreeNode<int>(5, node);
            TreeNode<int> child2 = new TreeNode<int>(6, node);
            TreeNode<int> grandchild1 = new TreeNode<int>(7, child1);

            Assert.IsNotNull(child1.Parent);
            Assert.IsNotNull(child2.Parent);
            Assert.IsNotNull(grandchild1.Parent);
            Assert.AreEqual(node, child1.Parent);
            Assert.AreEqual(node, child2.Parent);
            Assert.AreEqual(child1, grandchild1.Parent);
        }

        [TestMethod]
        public void TestTreeNodeCount()
        {
            TreeNode<int> node = new TreeNode<int>(4);
            TreeNode<int> child1 = new TreeNode<int>(5, node);
            TreeNode<int> child2 = new TreeNode<int>(6, node);
            TreeNode<int> grandchild1 = new TreeNode<int>(7, child1);

            Assert.AreEqual(4, node.Count);
            Assert.AreEqual(2, child1.Count);
            Assert.AreEqual(1, child2.Count);
            Assert.AreEqual(1, grandchild1.Count);
        }

        [TestMethod]
        public void TestTreeNodeDepth()
        {
            var root = new TreeNode<int>(4);
            var child1 = new TreeNode<int>(5, root);
            var child2 = new TreeNode<int>(6, root);
            var grandchild1 = new TreeNode<int>(7, child1);

            Assert.AreEqual(3, root.Depth);
        }

        [TestMethod]
        public void TestTreeDFUnlimitedTraversal()
        {
            var root = new TreeNode<int>(4);
            var child1 = new TreeNode<int>(5, root);
            var child2 = new TreeNode<int>(6, root);
            var grandchild1 = new TreeNode<int>(7, child1);
            var grandchild2 = new TreeNode<int>(8, child1);
            var dfEnumerator = root.GetEnumerator(VisitType.DepthFirst);
            var elements = new List<int>();
            var expected = new int[] { 4, 5, 7, 8, 6 };

            while (dfEnumerator.MoveNext())
            {
                elements.Add(dfEnumerator.Current);
            }

            var actual = elements.ToArray();
            
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected.Length, root.Count);

            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void TestTreeDFLimitedTraversal()
        {
            var root = new TreeNode<int>(4);
            var child1 = new TreeNode<int>(5, root);
            var child2 = new TreeNode<int>(6, root);
            var grandchild1 = new TreeNode<int>(7, child1);
            var grandchild2 = new TreeNode<int>(8, child1);
            var grandGrandchild1 = new TreeNode<int>(9, grandchild1);
            var dfEnumerator = root.GetEnumerator(VisitType.DepthFirst, 2);
            var elements = new List<int>();
            var expected = new int[] { 4, 5, 7, 8, 6 };

            while (dfEnumerator.MoveNext())
            {
                elements.Add(dfEnumerator.Current);
            }

            var actual = elements.ToArray();

            Assert.AreEqual(expected.Length, actual.Length);

            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void TestTreeBFUnlimitedTraversal()
        {
            var root = new TreeNode<int>(4);
            var child1 = new TreeNode<int>(5, root);
            var child2 = new TreeNode<int>(6, root);
            var grandchild1 = new TreeNode<int>(7, child1);
            var grandchild2 = new TreeNode<int>(8, child1);
            var dfEnumerator = root.GetEnumerator(VisitType.BreadthFirst);
            var elements = new List<int>();
            var expected = new int[] { 4, 5, 6, 7, 8 };

            while (dfEnumerator.MoveNext())
            {
                elements.Add(dfEnumerator.Current);
            }

            var actual = elements.ToArray();

            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected.Length, root.Count);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void TestTreeBFLimitedTraversal()
        {
            var root = new TreeNode<int>(4);
            var child1 = new TreeNode<int>(5, root);
            var child2 = new TreeNode<int>(6, root);
            var grandchild1 = new TreeNode<int>(7, child1);
            var grandchild2 = new TreeNode<int>(8, child1);
            var grandGrandchild1 = new TreeNode<int>(9, grandchild1);
            var dfEnumerator = root.GetEnumerator(VisitType.BreadthFirst, 2);
            var elements = new List<int>();
            var expected = new int[] { 4, 5, 6, 7, 8 };

            while (dfEnumerator.MoveNext())
            {
                elements.Add(dfEnumerator.Current);
            }

            var actual = elements.ToArray();

            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void TestTreeNodeElementAddition()
        {
            var root = new TreeNode<int>(4);
            root.Add(5);
            root.Add(6);
            root.Children.ElementAt(0).Add(7);

            Assert.AreEqual(4, root.Count);
            Assert.AreEqual(2, root.Children.Count());
            Assert.AreEqual(root, root.Children.ElementAt(0).Parent);
            Assert.AreEqual(root, root.Children.ElementAt(1).Parent);
        }
    }
}
