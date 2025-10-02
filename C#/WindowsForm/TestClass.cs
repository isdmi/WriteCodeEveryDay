using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class TestClass
    {
        public void Test() 
        {
            // Count Byの実装
            int[] randomDiceNumbers = [1, 2, 2, 4, 2, 6, 1, 5, 3, 2, 1, 3, 4, 5, 6, 3, 1, 1, 3, 4, 2, 6, 5, 4, 1, 3, 4, 1, 3, 6, 4, 2, 1];

            var res = randomDiceNumbers.CountBy(x => x).ToList();
            res.ForEach(x => Debug.WriteLine(x));

        }
    }
}
