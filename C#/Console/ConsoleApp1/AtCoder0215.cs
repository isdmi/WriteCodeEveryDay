using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder0215
    {
        public void QuestionA()
        {
            var S1 = Console.ReadLine();
            char[] N = S1.ToCharArray();

            if (N[0] == N[N.Count()-1])
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
            int N = int.Parse(S1);

            var array1 = new List<string>();

            for (int i = 0; i < N; i++) 
            {
                var S2 = Console.ReadLine();
                array1.Add(S2);
            }

            var maxLength = array1.Select(x => x.Length).Max();

            foreach (var item in array1) 
            {
                if (item.Length == maxLength) 
                {
                    Console.WriteLine(item);
                    continue;
                }

                var addLength = (maxLength - item.Length) / 2;
                string appendStr = new string('.', addLength);
                Console.WriteLine(appendStr + item + appendStr);                
            }
        }
    }
}
