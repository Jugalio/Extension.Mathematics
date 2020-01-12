using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.VectorSpace
{
    /// <summary>
    /// A mathematic vector with integer values
    /// </summary>
    public class IntVector
    {
        /// <summary>
        /// The elements in this vector
        /// </summary>
        public List<int> Elements { get; private set; }

        /// <summary>
        /// The dimension of this vector
        /// </summary>
        public int Dimension => Elements.Count;

        /// <summary>
        /// The orientation of this vector
        /// </summary>
        public VectorOrientation Orientation { get; private set; }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(IEnumerable<int> elements, VectorOrientation orientation = VectorOrientation.Column)
        {
            Elements = elements.ToList();
            Orientation = orientation;
        }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(int x, int y, VectorOrientation orientation = VectorOrientation.Column)
        {
            Elements.Add(x);
            Elements.Add(y);
            Orientation = orientation;
        }

        /// <summary>
        /// Initialize a integer vector
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="orientation"></param>
        public IntVector(int x, int y, int z, VectorOrientation orientation = VectorOrientation.Column)
        {
            Elements.Add(x);
            Elements.Add(y);
            Elements.Add(z);
            Orientation = orientation;
        }

        /// <summary>
        /// Adds a new element to the end of the vector
        /// </summary>
        /// <param name="elem"></param>
        public void AddElement(int elem)
        {
            Elements.Add(elem);
        }

        /// <summary>
        /// Adds an element at a specified index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="elem"></param>
        public void AddElementAt(int index, int elem)
        {
            Elements.Insert(index, elem);
        }

        /// <summary>
        /// Make the vector a normalized vector
        /// </summary>
        public void Normalize()
        {
            var gcd = Elements.Aggregate((a, b) => Operations.GCD(a, b));
            Elements = Elements.Select(e => e / gcd).ToList();
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

            var x = Elements[0];
            var y = Elements[1];
            var radius = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            //Within [0, 2Pi) ~ [0, 360°)
            var angle = (x, y) switch
            {
                _ when x > 0 => Math.Atan(y / x),
                _ when x < 0 => Math.Atan2(y, x),
                _ when x == 0 && y > 0 => Math.PI / 2,
                _ when x == 0 && y < 0 => Math.PI * 3/ 2,
                _ => default, //Only reached when the radius is 0 and the angle therefore undefined
            };

            return new PolarCoordinate(radius, angle);
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
                IntVector v => v.Elements.SequenceEqual(Elements),
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
            return string.Join(',', Elements).GetHashCode();
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
                result += (a.Elements[i] * b.Elements[i]);
            }

            return result;
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

            var newElements = a.Elements.Zip(b.Elements, (elemA, elemB) => elemA + elemB);

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

            var newElements = a.Elements.Zip(b.Elements, (elemA, elemB) => elemA - elemB);

            return new IntVector(newElements, a.Orientation);
        }

        /// <summary>
        /// Override the == operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator ==(IntVector obj1, IntVector obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Override the != operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(IntVector obj1, IntVector obj2)
        {
            return !(obj1 == obj2);
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
}
