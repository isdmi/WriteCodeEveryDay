using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder1005
    {
        public void SigmaCubes() 
        {
            var N = Console.ReadLine();
            int count = int.Parse(N);
            double result = 0;

            for (int i = 1; i <= count; i++) 
            {
                double calc = Math.Pow(-1,i) * Math.Pow(i,3);
                result = result + calc;
            }

            Console.WriteLine(result);
        }
    }
}
