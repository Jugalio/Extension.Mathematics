using Extension.Mathematics.VectorSpace;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extension.Mathematics.Tests.VectorSpace
{
    [TestFixture]
    public class PolarCoordinateTest
    {

        [Test]
        public void EqualNullTest()
        {
            PolarCoordinate coordinate = null;
            Assert.IsTrue(coordinate == null);
        }

        [TestCase(1,2,1,2, true)]
        [TestCase(1, 2, 4, 2, false)]
        [TestCase(1, 2, 1, 1, false)]
        public void EqualTest(int radius, int angle, int radius1, int angle1, bool expect)
        {
            var pol = new PolarCoordinate(radius, angle);
            var pol1 = new PolarCoordinate(radius1, angle1);

            var result = pol == pol1;

            Assert.IsTrue(result == expect);
            Assert.IsFalse(result != expect);
        }

    }
}
