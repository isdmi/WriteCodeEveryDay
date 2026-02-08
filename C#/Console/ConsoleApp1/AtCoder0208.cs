using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder0208
    {
        public void QuestionA()
        {
            var S1 = Console.ReadLine();
            char[] N = S1.ToCharArray();

            if (N[0] == N[1] && N[0] == N[2])
            {
                Console.WriteLine("Yes");
            }
            else 
            {
                Console.WriteLine("No");
            }
        }

        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToList();

            int result = Enumerable.Range(1, array1[0]).Where(x => SumRange(x) == array1[1]).Count();

            Console.WriteLine($"{result}");
        }

        public int SumRange(int x) 
        {
            int sum = 0;
            while (x > 0)
            {
                sum += x % 10;
                x /= 10;
            }
            return sum;
        }
    }
}
