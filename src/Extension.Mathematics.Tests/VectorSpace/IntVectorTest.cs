using Extension.Mathematics.VectorSpace;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.Tests.VectorSpace
{
    [TestFixture]
    public class IntVectorTest
    {
        private static readonly object[] sourceList1 = new object[]
        {
            new object[]
            {
                new List<int>{20, 10, 5},
                new List<int>{4, 2, 1},
            },
            new object[]
            {
                new List<int>{18, 6, 60, 66, 120},
                new List<int>{3, 1, 10, 11, 20},
            },
            new object[]
            {
                new List<int>{28, 56, 70, 140},
                new List<int>{2, 4, 5, 10},
            },
            new object[]
            {
                new List<int>{0, 0},
                new List<int>{0,0},
            },
        };

        private static readonly object[] sourceList2 = new object[]
        {
            new object[]
            {
                new List<int>{20, 10, 5},
                new List<int>{4, 2, 1},
                105
            },
            new object[]
            {
                new List<int>{18, 6, 60, 66, 120},
                new List<int>{3, 1, 10, 11, 20},
                3786
            },
            new object[]
            {
                new List<int>{28, 56, 70, 140},
                new List<int>{2, 4, 5, 10},
                2030
            },
        };

        private static readonly object[] sourceList3 = new object[]
        {
            new object[]
            {
                new List<int>{20, 10, 5},
                new List<int>{25, 15,4},
                new List<int>{45, 25,9},
            },
            new object[]
            {
                new List<int>{18, 6, 60, 66, 120},
                new List<int>{3, 1, 10, 11, 20},
                new List<int>{21, 7, 70, 77, 140},
            },
        };

        private static readonly object[] sourceList4 = new object[]
        {
            new object[]
            {
                new List<int>{20, 10, 5},
                new List<int>{25, 15,4},
                new List<int>{-5, -5,1},
            },
            new object[]
            {
                new List<int>{18, 6, 60, 66, 120},
                new List<int>{3, 1, 10, 11, 20},
                new List<int>{15, 5, 50, 55, 100},
            },
        };

        [TestCaseSource("sourceList1")]
        public void NormalizeTest(List<int> elements, List<int> expectedNormalization)
        {
            var vector = new IntVector(elements);
            var normalized = vector.Normalize();

            Assert.IsTrue(normalized.SequenceEqual(expectedNormalization));
        }

        [TestCase(0, 2, Math.PI / 2)]
        [TestCase(0, -2, Math.PI * 3 / 2)]
        [TestCase(2, 0, 0)]
        [TestCase(-2, 0, Math.PI)]
        [TestCase(1, 1, Math.PI / 4)]
        [TestCase(1, -1, Math.PI * 7 / 4)]
        [TestCase(-1, 1, Math.PI * 3 / 4)]
        [TestCase(-1, -1, Math.PI * 5 / 4)]
        public void GetPolarCoordinateTest(int x, int y, double angle)
        {
            var vector = new IntVector(x, y);
            var polar = vector.GetPolarCoordinate();

            Assert.IsTrue(polar.Angle == angle);

            var reverseVector = polar.GetIntVector();

            Assert.IsTrue(x == reverseVector.X);
            Assert.IsTrue(y == reverseVector.Y);
        }

        [TestCaseSource("sourceList2")]
        public void MultiplicationTest(List<int> elementsA, List<int> elementsB, int expectedResult)
        {
            var vectorA = new IntVector(elementsA);
            var vectorB = new IntVector(elementsB);

            Assert.IsTrue(expectedResult == vectorA * vectorB);
        }

        [TestCaseSource("sourceList3")]
        public void AdditionTest(List<int> elementsA, List<int> elementsB, List<int> expectedElements)
        {
            var vectorA = new IntVector(elementsA);
            var vectorB = new IntVector(elementsB);

            var result = vectorA + vectorB;

            Assert.IsTrue(result.SequenceEqual(expectedElements));
        }

        [TestCaseSource("sourceList4")]
        public void SubtractionTest(List<int> elementsA, List<int> elementsB, List<int> expectedElements)
        {
            var vectorA = new IntVector(elementsA);
            var vectorB = new IntVector(elementsB);

            var result = vectorA - vectorB;

            Assert.IsTrue(result.SequenceEqual(expectedElements));
        }

        [Test]
        public void EqualNullTest()
        {
            IntVector vec = null;

            Assert.IsTrue(vec == null);
        }

        [TestCase(1, 2, 1, 2, true)]
        [TestCase(1, 2, 4, 2, false)]
        [TestCase(1, 2, 1, 1, false)]
        public void EqualTest(int x, int y, int x1, int y1, bool expect)
        {
            var int1 = new PolarCoordinate(x, y);
            var int2 = new PolarCoordinate(x1, y1);

            var result = int1 == int2;

            Assert.IsTrue(result == expect);
            Assert.IsFalse(result != expect);
        }
    }
}
