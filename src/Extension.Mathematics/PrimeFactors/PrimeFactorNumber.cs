using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.PrimeFactors
{
    /// <summary>
    /// A number in prime factor format
    /// </summary>
    public class PrimeFactorNumber
    {
        /// <summary>
        /// Prime numbers in (0,100)
        /// </summary>
        private static List<int> PRIMESTO100 = new List<int>() {2, 3, 5, 7, 11, 13, 17, 19,
        23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

        private bool IsZero = false;
        private bool IsOne = false;
        private bool IsNegative = false;

        /// <summary>
        /// The prime factors the represent the number
        /// </summary>
        public IDictionary<int, int> NumeratorPrimeFactors { get; private set; } = new Dictionary<int, int>();

        /// <summary>
        /// The prime factors the represent the number
        /// </summary>
        public IDictionary<int, int> DenominatorPrimeFactors { get; private set; } = new Dictionary<int, int>();

        /// <summary>
        /// Creates a prime factor number from a long
        /// </summary>
        /// <param name="i"></param>
        private PrimeFactorNumber(IDictionary<int, int> numeratorPrimeFactors, IDictionary<int, int> denominatorPrimeFactors, bool isNegative)
        {
            NumeratorPrimeFactors = numeratorPrimeFactors;
            DenominatorPrimeFactors = denominatorPrimeFactors;
            IsNegative = isNegative;
        }

        /// <summary>
        /// Creates a prime factor number from a long
        /// </summary>
        /// <param name="i"></param>
        public PrimeFactorNumber(long i)
        {
            if (i == 0)
            {
                IsZero = true;
            }
            else if (i == 1 || i == -1)
            {
                IsOne = true;
            }
            else
            {
                Split(i);
            }

            if (i < 0)
            {
                IsNegative = true;
            }
        }

        /// <summary>
        /// Try to get the int value back
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool TryGetInt(out int output)
        {
            try
            {
                var num = GetIntFromFactors(NumeratorPrimeFactors);
                var den = GetIntFromFactors(DenominatorPrimeFactors);
                output = num / den;
                return true;
            }
            catch(OverflowException _)
            {
                output = default;
                return false;
            }
        }

        /// <summary>
        /// Try to get the long value back
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool TryGetLong(out long output)
        {
            try
            {
                var num = GetLongFromFactors(NumeratorPrimeFactors);
                var den = GetLongFromFactors(DenominatorPrimeFactors);
                output = num / den;
                return true;
            }
            catch (OverflowException)
            {
                output = default;
                return false;
            }
            catch (InvalidCastException)
            {
                output = default;
                return false;
            }
        }

        /// <summary>
        /// Returns the result of all factors multiplied as integer
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        private int GetIntFromFactors(IDictionary<int, int> factors)
        {
            if (factors.Count == 0)
            {
                return 1;
            }
            else
            {
                return factors.Select(a => Operations.SafePow(a.Key, a.Value)).Aggregate((a, b) => Operations.SafeMultiplication(a, b));
            }
        }

        /// <summary>
        /// Returns the result of all factors multiplied as long
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        private long GetLongFromFactors(IDictionary<int, int> factors)
        {
            if (factors.Count == 0)
            {
                return 1;
            }
            else
            {
                return factors.Select(a => Operations.SafePow(a.Key, a.Value)).Aggregate((a, b) => Operations.SafeMultiplication(a, b));
            }
        }

        /// <summary>
        /// Splits a given number into its prime factors
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private void Split(long x)
        {
            x = SplitIntoPrimeFactorsFastStart(x);

            int div = 101;
            while (div <= x && x != 1)
            {
                while (x % div == 0)
                {
                    x = x / div;
                    AddNumeratorPrimeFactor(div);
                }
                div++;
            }
        }

        /// <summary>
        /// Splits a given number into its prime factors which are within (0,100)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private long SplitIntoPrimeFactorsFastStart(long x)
        {
            foreach (int div in PRIMESTO100)
            {
                while (x % div == 0)
                {
                    x = x / div;
                    AddNumeratorPrimeFactor(div);
                }
            }
            return x;
        }

        /// <summary>
        /// Adds a prime factor
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="count"></param>
        private void AddNumeratorPrimeFactor(int factor, int count = 1)
        {
            if (NumeratorPrimeFactors.ContainsKey(factor))
            {
                NumeratorPrimeFactors[factor] = NumeratorPrimeFactors[factor] + count;
            }
            else
            {
                NumeratorPrimeFactors.Add(factor, count);
            }
        }

        /// <summary>
        /// Removes a prime factor
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="count"></param>
        private void RemoveNumeratorPrimeFactor(int factor, int count)
        {
            var newCount = NumeratorPrimeFactors[factor] - count;
            if (newCount <= 0)
            {
                NumeratorPrimeFactors.Remove(factor);
            }
            else
            {
                NumeratorPrimeFactors[factor] = newCount;
            }
        }

        /// <summary>
        /// Adds all prime factors provided
        /// </summary>
        private void AddNumeratorPrimeFactors(IDictionary<int, int> factors)
        {
            foreach ((int factor, int count) in factors)
            {
                AddNumeratorPrimeFactor(factor, count);
            }
        }

        /// <summary>
        /// Adds a prime factor
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="count"></param>
        private void AddDenominatorPrimeFactor(int factor, int count)
        {
            if (DenominatorPrimeFactors.ContainsKey(factor))
            {
                DenominatorPrimeFactors[factor] = DenominatorPrimeFactors[factor] + count;
            }
            else
            {
                DenominatorPrimeFactors.Add(factor, count);
            }
        }

        /// <summary>
        /// Removes a prime factor
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="count"></param>
        private void RemoveDenominatorPrimeFactor(int factor, int count)
        {
            var newCount = DenominatorPrimeFactors[factor] - count;
            if (newCount <= 0)
            {
                DenominatorPrimeFactors.Remove(factor);
            }
            else
            {
                DenominatorPrimeFactors[factor] = newCount;
            }
        }

        /// <summary>
        /// Adds all prime factors provided
        /// </summary>
        private void AddDenominatorPrimeFactors(IDictionary<int, int> factors)
        {
            foreach ((int factor, int count) in factors)
            {
                AddDenominatorPrimeFactor(factor, count);
            }
        }

        /// <summary>
        /// Simplify by reducing the nominator and denominator by common primes
        /// </summary>
        private void Simplify()
        {
            var common = NumeratorPrimeFactors.Keys.Intersect(DenominatorPrimeFactors.Keys).ToList();
            foreach (int factor in common)
            {
                var min = Math.Min(NumeratorPrimeFactors[factor], DenominatorPrimeFactors[factor]);
                RemoveNumeratorPrimeFactor(factor, min);
                RemoveDenominatorPrimeFactor(factor, min);
            }
        }

        /// <summary>
        /// Set the equality operation for prime numbers
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj switch
            {
                PrimeFactorNumber f => EqualFactors(NumeratorPrimeFactors, f.NumeratorPrimeFactors) && EqualFactors(DenominatorPrimeFactors, f.DenominatorPrimeFactors),
                int i => new PrimeFactorNumber(i).Equals(this),
                long i => new PrimeFactorNumber(i).Equals(this),
                _ => false,
            };
        }

        /// <summary>
        /// Compares two prime factor dictionaries to eachother
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool EqualFactors(IDictionary<int, int> a, IDictionary<int, int> b)
        {
            var commonKeys = a.Keys.Intersect(b.Keys);
            if (commonKeys.Count() != a.Keys.Count || commonKeys.Count() != b.Keys.Count)
            {
                return false;
            }

            return commonKeys.All(key => a[key] == b[key]);
        }

        /// <summary>
        /// Returns the hascode of the prime code number
        /// Since we constructed this class in order to handle number bigger then integer
        /// this is a operation that can cause colisions
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (string.Join(',', NumeratorPrimeFactors.Values) + string.Join(',', DenominatorPrimeFactors.Values)).GetHashCode();
        }

        /// <summary>
        /// Mulitplication for prime factor numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static PrimeFactorNumber operator *(PrimeFactorNumber a, PrimeFactorNumber b)
        {
            var isNegativ = (a.IsNegative && !b.IsNegative) || (!a.IsNegative && b.IsNegative);

            if (a.IsZero || b.IsZero)
            {
                return new PrimeFactorNumber(0);
            }
            else if (a.IsOne)
            {
                b.IsNegative = isNegativ;
                return b;
            }
            else if (b.IsOne)
            {
                a.IsNegative = isNegativ;
                return a;
            }
            else
            {
                var result = new PrimeFactorNumber(a.NumeratorPrimeFactors, a.DenominatorPrimeFactors, isNegativ);
                result.AddNumeratorPrimeFactors(b.NumeratorPrimeFactors);
                result.AddDenominatorPrimeFactors(b.DenominatorPrimeFactors);
                return result;
            }
        }

        /// <summary>
        /// Division for prime factor numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static PrimeFactorNumber operator /(PrimeFactorNumber a, PrimeFactorNumber b)
        {
            var isNegativ = (a.IsNegative && !b.IsNegative) || (!a.IsNegative && b.IsNegative);

            if (a.IsZero)
            {
                return new PrimeFactorNumber(0);
            }
            else if (b.IsZero)
            {
                throw new DivideByZeroException();
            }
            else if (a.IsOne)
            {
                a.DenominatorPrimeFactors = b.NumeratorPrimeFactors;
                return a;
            }
            else if (b.IsOne)
            {
                a.IsNegative = isNegativ;
                return a;
            }
            else
            {
                var inverseB = new PrimeFactorNumber(b.DenominatorPrimeFactors, b.NumeratorPrimeFactors, b.IsNegative);
                var inverseMultiplication = a * inverseB;
                inverseMultiplication.Simplify();
                return inverseMultiplication;
            }
        }

        /// <summary>
        /// Override the == operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator ==(PrimeFactorNumber obj1, PrimeFactorNumber obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Override the != operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(PrimeFactorNumber obj1, PrimeFactorNumber obj2)
        {
            return !(obj1 == obj2);
        }

    }
}
