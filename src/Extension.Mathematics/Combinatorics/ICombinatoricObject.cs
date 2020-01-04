using Extension.Mathematics.PrimeFactors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extension.Mathematics.Combinatorics
{
    public interface ICombinatoricObject
    {

        /// <summary>
        /// Returns the number of possible combinations
        /// </summary>
        /// <returns></returns>
        PrimeFactorNumber GetCount();

        /// <summary>
        /// Get all combinations that can be selected from the choices
        /// </summary>
        /// <param name="choices"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<T>> Get<T>(List<T> choices);

    }
}
