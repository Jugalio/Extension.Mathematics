using Extension.Mathematics.PrimeFactors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extension.Mathematics.Tests.PrimeFactors
{
    [TestFixture]
    public class PrimeFactorTests
    {

        [TestCase(5)]
        [TestCase(5000)]
        [TestCase(158682)]
        [TestCase(1897135418)]
        [TestCase(1891324)]
        public void TestEqual(int input)
        {
            var a = new PrimeFactorNumber(input);
            var b = new PrimeFactorNumber(input);
            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
        }

        [TestCase(5)]
        [TestCase(5000)]
        [TestCase(158682)]
        [TestCase(1897135418)]
        [TestCase(1891324)]
        public void TestGetInt(int input)
        {
            var a = new PrimeFactorNumber(input);
            a.TryGetInt(out int b);

            Assert.AreEqual(input, b);
        }

        [TestCase(5, 300, 1500)]
        [TestCase(5000, 8, 40000)]
        [TestCase(158682, 815, 129325830)]
        public void TestMultiplication(int inputA, int inputB, int expectedOutput)
        {
            var a = new PrimeFactorNumber(inputA);
            var b = new PrimeFactorNumber(inputB);
            var result = a * b;
            result.TryGetInt(out int output);
            Assert.IsTrue(expectedOutput == output);
        }

        [TestCase(1500, 300, 5)]
        [TestCase(40000, 5000, 8)]
        [TestCase(129325830, 158682, 815)]
        public void TestDivision(int inputA, int inputB, int expectedOutput)
        {
            var a = new PrimeFactorNumber(inputA);
            var b = new PrimeFactorNumber(inputB);
            var result = a / b;
            result.TryGetInt(out int output);
            Assert.IsTrue(expectedOutput == output);
        }
    }
}
