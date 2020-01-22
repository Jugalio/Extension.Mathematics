using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.VectorSpace
{
    /// <summary>
    /// A mathematic vector with integer values
    /// </summary>
    public class IntVector : List<int>
    {
        /// <summary>
        /// The dimension of this vector
        /// </summary>
        public int Dimension => this.Count;

        /// <summary>
        /// Easy acces to the first element
        /// </summary>
        public int X => this[0];

        /// <summary>
        /// Easy acces to the second element
        /// </summary>
        public int Y => this[1];

        /// <summary>
        /// Easy acces to the third element
        /// </summary>
        public int Z => this[2];

        /// <summary>
        /// The orientation of this vector
        /// </summary>
        public VectorOrientation Orientation { get; private set; }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(IEnumerable<int> elements, VectorOrientation orientation = VectorOrientation.Column) : base(elements)
        {
            Orientation = orientation;
        }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(int x, int y, VectorOrientation orientation = VectorOrientation.Column) : base()
        {
            Add(x);
            Add(y);
            Orientation = orientation;
        }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(int x, int y, int z, VectorOrientation orientation = VectorOrientation.Column) : base()
        {
            Add(x);
            Add(y);
            Add(z);
            Orientation = orientation;
        }

        /// <summary>
        /// Make the vector a normalized vector
        /// </summary>
        public IntVector Normalize()
        {
            var gcd = this.Aggregate((a, b) => Operations.GCD(a, b));
            return gcd == 0 ? this : this.Select(e => e / gcd).ToIntVector(Orientation);
        }

        /// <summary>
        /// Gets the polar coordinate from the vector, but only if it is a vector with the dimension 2
        /// </summary>
        public PolarCoordinate GetPolarCoordinate()
        {
            if (Dimension != 2)
            {
                throw new InvalidOperationException($"Polar coordinates are only defined for 2d vectors");
            }

            var radius = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            var quotient = X == 0 ? 0 : Y / (double)X;

            //Within [0, 2Pi) ~ [0, 360°)
            var angle = (X, Y) switch
            {
                _ when X > 0 && Y >= 0 => Math.Atan(quotient),
                _ when X > 0 && Y < 0 => Math.Atan(quotient) + 2 * Math.PI,
                _ when X < 0 => Math.Atan(quotient) + Math.PI,
                _ when X == 0 && Y > 0 => Math.PI / 2,
                _ when X == 0 && Y < 0 => Math.PI * 3 / 2,
                _ => default, //Only reached when the radius is 0 and the angle therefore undefined
            };

            return new PolarCoordinate(radius, angle);
        }

        /// <summary>
        /// Calculates the distance to another vector based on the selected metric
        /// </summary>
        /// <param name="b"></param>
        /// <param name="metric"></param>
        /// <returns></returns>
        public int DistanceTo(IntVector b, Metric metric = Metric.Manhatten)
        {
            if (Dimension != b.Dimension)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            return metric switch
            {
                Metric.Manhatten => this.Zip(b, (a, b) => Math.Abs(a - b)).Sum(),
                _ => throw new ArgumentException("Metric unknown"),
            };
        }

        /// <summary>
        /// Override equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj switch
            {
                IntVector v => v.SequenceEqual(this),
                _ => false,
            };
        }

        /// <summary>
        /// Returns the hascode of the prime code number
        /// Since we constructed this class in order to handle number bigger then integer
        /// this is a operation that can cause colisions
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 17;
            return this.Aggregate(hash, (a, b) => a * 23 + b);
        }

        /// <summary>
        /// Mulitplication for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int operator *(IntVector a, IntVector b)
        {
            if (a.Orientation != VectorOrientation.Row && b.Orientation != VectorOrientation.Column)
            {
                throw new InvalidOperationException($"One can only multiply a column with a row vector. You tried ro multiply {a.Orientation} with {b.Orientation}");
            }

            if (a.Dimension != b.Dimension)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            int result = 0;

            for (int i = 0; i < a.Dimension; i++)
            {
                result += (a[i] * b[i]);
            }

            return result;
        }

        /// <summary>
        /// Scaling of vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator *(IntVector a, int b)
        {
            var newElements = a.Select(e => e * b);

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Addition for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator +(IntVector a, IntVector b)
        {
            if (a.Orientation != b.Orientation)
            {
                throw new InvalidOperationException($"Both vectors have to be in the same orientation");
            }

            if (a.Dimension != b.Dimension)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = a.Zip(b, (elemA, elemB) => elemA + elemB);

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Addition for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator +(IntVector a, (int x, int y) b)
        {
            if (a.Dimension != 2)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = new List<int> { a.X + b.x, a.Y + b.y };
            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Addition for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator +(IntVector a, (int x, int y, int z) b)
        {
            if (a.Dimension != 3)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = new List<int> { a.X + b.x, a.Y + b.y, a.Z + b.z };
            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Subtraction for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator -(IntVector a, IntVector b)
        {
            if (a.Orientation != b.Orientation)
            {
                throw new InvalidOperationException($"Both vectors have to be in the same orientation");
            }

            if (a.Dimension != b.Dimension)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = a.Zip(b, (elemA, elemB) => elemA - elemB);

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Subtraction for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator -(IntVector a, (int x, int y) b)
        {
            if (a.Dimension != 2)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = new List<int> { a.X - b.x, a.Y - b.y };

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Subtraction for IntVector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntVector operator -(IntVector a, (int x, int y, int z) b)
        {
            if (a.Dimension != 3)
            {
                throw new InvalidOperationException($"Both vectors have to be of the same dimension");
            }

            var newElements = new List<int> { a.X - b.x, a.Y - b.y, a.Z - b.z };

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Override the == operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator ==(IntVector obj1, object obj2)
        {
            var asObj = (object)obj1;

            if (asObj == null)
            {
                return obj2 == null;
            }
            else
            {
                return obj2 switch
                {
                    IntVector v => v.SequenceEqual(obj1),
                    _ => false,
                };
            }
        }

        /// <summary>
        /// Override the != operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(IntVector obj1, object obj2)
        {
            return !(obj1 == obj2);
        }
    }

    /// <summary>
    /// Extensions for linq
    /// </summary>
    public static class IntVectorLINQExtension
    {
        /// <summary>
        /// Extension for linq
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IntVector ToIntVector(this IEnumerable<int> source, VectorOrientation orientation = VectorOrientation.Column)
        {
            return new IntVector(source, orientation);
        }
    }

    /// <summary>
    /// The vector orientation
    /// </summary>
    public enum VectorOrientation
    {
        Column,
        Row
    }

    /// <summary>
    /// Supported metric for distance measuring
    /// </summary>
    public enum Metric
    {
        Manhatten,
    }
}
