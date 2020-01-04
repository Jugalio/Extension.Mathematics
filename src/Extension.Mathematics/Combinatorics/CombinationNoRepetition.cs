using Extension.Mathematics.PrimeFactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Mathematics.Combinatorics
{
    public class CombinationNoRepetition : CombinatoricBaseObject
    {
        /// <summary>
        /// Creates a new combinatoric object
        /// </summary>
        /// <param name="n">Number of elements to choose from</param>
        /// <param name="k">Number of elements in the combinations</param>
        public CombinationNoRepetition(int n, int k) : base(n, k)
        {
        }

        /// <summary>
        /// Returns the number of possible combinations
        /// </summary>
        /// <returns></returns>
        public override PrimeFactorNumber GetCount()
        {
            return Operations.BinomialCoefficient(N, K);
        }

        /// <summary>
        /// Produces a new set of combinations which is increased by 1 index
        /// </summary>
        /// <param name="combs"></param>
        /// <returns></returns>
        public override IEnumerable<IEnumerable<int>> AddNextIndex(IEnumerable<IEnumerable<int>> combs)
        {
            return combs.SelectMany(c =>
            {
                var skip = c.Count() == 0 ? 0 : c.LastOrDefault() + 1;
                return _range.Skip(skip).Select(nextObject =>
                {
                    var newComb = c.ToList();
                    newComb.Add(nextObject);
                    return newComb;
                });
            });
        }
    }
}
