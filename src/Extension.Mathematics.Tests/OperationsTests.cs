using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extension.Mathematics.Tests
{
    [TestFixture]
    public class OperationsTests
    {

        [TestCase(20, 5, 5)]
        [TestCase(5, 20, 5)]
        [TestCase(30, 80, 10)]
        public void TestGCD(int a, int b, int expectedOutput)
        {
            var output = Operations.GCD(a, b);
            Assert.IsTrue(output == expectedOutput);
        }

        [TestCase(20, 5, 20)]
        [TestCase(5, 20, 20)]
        [TestCase(30, 80, 240)]
        public void TestLCM(int a, int b, int expectedOutput)
        {
            var output = Operations.LCM(a, b);
            Assert.IsTrue(output == expectedOutput);
        }
    }
}
