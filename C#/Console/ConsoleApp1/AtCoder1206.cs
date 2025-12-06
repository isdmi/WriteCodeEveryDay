using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder1206
    {
        public void QuestionA()
        {
            var S1 = Console.ReadLine();
            int N = int.Parse(S1);

            var ans = Enumerable.Range(1, N).Sum();

            Console.WriteLine(ans);
        }

        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            int N = int.Parse(S1);

            var S2 = Console.ReadLine();
            var array1 = S2.Split(' ').Select(x => int.Parse(x)).ToList();

            var ans = 0;
            
            for (int i = 0; i < N; i++) 
            {
                for (int j = i; j < N; j++) 
                {
                    var range = array1.GetRange(i, j + 1 - i);
                    var sum = range.Sum();
                    var countflag = true;

                    foreach (var item in range) 
                    {
                        if (sum % item == 0) 
                        {
                            countflag = false;
                            break;
                        }
                    }

                    if (countflag) 
                    {
                        ans = ans + 1;
                    }
                }
            }

            Console.WriteLine(ans);
        }
    }
}
