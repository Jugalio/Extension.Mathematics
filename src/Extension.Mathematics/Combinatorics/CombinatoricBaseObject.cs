using Extension.Mathematics.PrimeFactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.Combinatorics
{
    public abstract class CombinatoricBaseObject: ICombinatoricObject
    {
        protected HashSet<int> _range;

        /// <summary>
        /// Number of elements to choose from
        /// </summary>
        public int N;

        /// <summary>
        /// Number of elements in the combinations
        /// </summary>
        public int K;

        /// <summary>
        /// Creates a new combinatoric object
        /// </summary>
        /// <param name="n">Number of elements to choose from</param>
        /// <param name="k">Number of elements in the combinations</param>
        public CombinatoricBaseObject(int n, int k)
        {
            N = n;
            K = k;
            _range = new HashSet<int>(Enumerable.Range(0, n));
        }

        /// <summary>
        /// Get all combinations that can be selected from the choices
        /// </summary>
        /// <param name="choices"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<T>> Get<T>(List<T> choices)
        {
            return GetIndexed().Select(indexed => indexed.Select(index => choices[index]));
        }

        public IEnumerable<IEnumerable<int>> GetIndexed()
        {
            IEnumerable<IEnumerable<int>> combs = new List<List<int>>() { new List<int>() };

            for (int i = 0; i < K; i++)
            {
                combs = AddNextIndex(combs);
            }

            return combs;
        }

        /// <summary>
        /// Returns the number of possible combinations
        /// </summary>
        /// <returns></returns>
        public abstract PrimeFactorNumber GetCount();

        /// <summary>
        /// Produces a new set of combinations which is increased by 1 index
        /// </summary>
        /// <param name="combs"></param>
        /// <returns></returns>
        public abstract IEnumerable<IEnumerable<int>> AddNextIndex(IEnumerable<IEnumerable<int>> combs);
    }
}
