using System;
using System.Collections.Generic;
using System.Text;

namespace Extension.Mathematics.VectorSpace
{
    public class PolarCoordinate
    {
        /// <summary>
        /// The radius of the vector
        /// </summary>
        public double Radius;

        /// <summary>
        /// The angle within [0, 2PI) ~ [0°, 360°)
        /// </summary>
        public double Angle;

        /// <summary>
        /// Instanciates a vector in polar coordinate format
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        public PolarCoordinate(double radius, double angle)
        {
            Radius = radius;
            Angle = angle;
        }

    }
}
