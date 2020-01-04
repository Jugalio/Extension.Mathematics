using Extension.Mathematics.PrimeFactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics
{
    public static class Operations
    {
        /// <summary>
        /// Returns the faculty of n (n!) in prime factor format
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        /// <param name="n"></param>
        /// <returns></returns>
        public static PrimeFactorNumber Faculty(int n)
        {
            var multiplicants = Enumerable.Range(2, n - 1).Select(i => new PrimeFactorNumber(i));
            return multiplicants.Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// Return the binomial coefficient, of n over k with is the number of possible unique combinations
        /// of length k from a set of n elements
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static PrimeFactorNumber BinomialCoefficient(int n, int k)
        {
            var dividend = Faculty(n);
            var divisor = Faculty(k) * Faculty(n - k);
            var result = dividend / divisor;

            return result;
        }

        /// <summary>
        /// Multiplies the input values and makes sure the result does not overflow
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long SafeMultiplication(long a, long b)
        {
            var x = a * b;
            if (a != 0 && x / a != b)
            {
                throw new OverflowException($"The result of the multiplication {a} * {b} exeeds the limits of long");
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// An overflow safe pow operation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SafePow(int a, int b)
        {
            var x = (int)Math.Pow(a, b);
            var sqrt = 1.0 / b;
            var check = Math.Pow(x, sqrt);
            if (a != 0 && check != a)
            {
                throw new OverflowException($"The result of the multiplication {a} ^ {b} exeeds the limits of long");
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// An overflow safe pow operation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SafePow(long a, long b)
        {
            var x = (int)Math.Pow(a, b);
            if (a != 0 && Math.Pow(x, (1 / b)) != a)
            {
                throw new OverflowException($"The result of the multiplication {a} ^ {b} exeeds the limits of long");
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// Multiplies the input values and makes sure the result does not overflow
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SafeMultiplication(int a, int b)
        {
            var x = a * b;
            if (a != 0 && x / a != b)
            {
                throw new OverflowException($"The result of the multiplication {a} * {b} exeeds the limits of int");
            }
            else
            {
                return x;
            }
        }

    }
}
