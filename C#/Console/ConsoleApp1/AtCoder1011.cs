using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder1011
    {
        public void ABCAB() 
        {
            var S = Console.ReadLine();
            var target = S.Length / 2;

            Console.WriteLine(S.Remove(target, 1));
        }

        public void SumOfDigitsSequence() 
        {
            var N = Int32.Parse(Console.ReadLine());

            int SumOfDisits(int x) {
                int sum = 0;
                while (x > 0) 
                {
                    sum = sum + (x % 10);
                    x = x / 10;
                }
                return sum;
            };

            List<int> result = new List<int>();
            result.Add(1);

            for (int i = 1; i <= N; i++) 
            {
                int sum = 0;
                for (int j = 0; j < i; j++) 
                {
                    sum = sum + SumOfDisits(result[j]);
                }
                result.Add(sum);
            }
            Console.WriteLine(result[N]);
        }
    }
}
