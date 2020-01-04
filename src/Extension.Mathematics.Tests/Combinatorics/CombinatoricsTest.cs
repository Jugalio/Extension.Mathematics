using Extension.Mathematics.Combinatorics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.Tests.Combinatorics
{
    [TestFixture]
    public class CombinatoricsTest
    {
        /// <summary>
        /// The simple test just tests, if the correct count is returned by both functions
        /// </summary>
        [TestCase(10, 3, 1000)]
        [TestCase(10, 4, 10000)]
        public void SimpleTestPermutationRepition(int n, int k, int expectedCount)
        {
            var testObject = new PermutationRepetition(n, k);
            testObject.GetCount().TryGetInt(out int count);
            Assert.IsTrue(count == expectedCount);
            Assert.IsTrue(testObject.GetIndexed().Count() == expectedCount);
        }

        /// <summary>
        /// The simple test just tests, if the correct count is returned by both functions
        /// </summary>
        [TestCase(8, 3, 336)]
        public void SimpleTestPermutationNoRepition(int n, int k, int expectedCount)
        {
            var testObject = new PermutationNoRepetition(n, k);
            testObject.GetCount().TryGetInt(out int count);
            Assert.IsTrue(count == expectedCount);
            Assert.IsTrue(testObject.GetIndexed().Count() == expectedCount);
        }

        /// <summary>
        /// The simple test just tests, if the correct count is returned by both functions
        /// </summary>
        [TestCase(99, 2, 4950)]
        public void SimpleTestCombinationRepition(int n, int k, long expectedCount)
        {
            var testObject = new CombinationRepetition(n, k);
            testObject.GetCount().TryGetInt(out int count);
            Assert.IsTrue(count == expectedCount);
            Assert.IsTrue(testObject.GetIndexed().Count() == expectedCount);
        }

        /// <summary>
        /// The simple test just tests, if the correct count is returned by both functions
        /// </summary>
        [TestCase(100, 2, 4950)]
        public void SimpleTestCombinationNoRepition(int n, int k, long expectedCount)
        {
            var testObject = new CombinationNoRepetition(n, k);
            testObject.GetCount().TryGetInt(out int count);
            var indexed = testObject.GetIndexed();
            var indexedCount = indexed.Count();
            Assert.IsTrue(count == expectedCount);
            Assert.IsTrue(indexedCount == expectedCount);
        }

    }
}
