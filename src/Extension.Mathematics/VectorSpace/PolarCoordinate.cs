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

        /// <summary>
        /// Gets the int vector which is defined by this polar coordinates
        /// </summary>
        /// <returns></returns>
        public IntVector GetIntVector()
        {
            int x = (int)Math.Round(Radius * Math.Cos(Angle));
            int y = (int)Math.Round(Radius * Math.Sin(Angle));
            return new IntVector(x, y);
        }

        /// <summary>
        /// Polar coordinates equal each other if they have the same angle and radius
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj switch
            {
                PolarCoordinate p => p.Angle == Angle && p.Radius == Radius,
                _ => false,
            };
        }

        /// <summary>
        /// Override hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            double hash = 17;
            hash = hash * 23 + Angle;
            hash = hash * 23 + Radius;
            return (int)hash;
        }

        public static bool operator ==(PolarCoordinate a, object b)
        {
            var asObj = (object)a;

            if (asObj == null)
            {
                return b == null;
            }
            else
            {
                return b switch
                {
                    PolarCoordinate p => p.Angle == a?.Angle && p.Radius == a?.Radius,
                    _ => false,
                };
            }
        }

        public static bool operator !=(PolarCoordinate a, object b)
        {
            return !(a == b);
        }

    }
}
