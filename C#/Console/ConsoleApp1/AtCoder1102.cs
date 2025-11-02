using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AtCoder1102
    {
        public void QuestionA() 
        {
            var S1 = Console.ReadLine();
            if (S1 == null) return;

            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var A = array1[0];
            var B = array1[1];
            var C = array1[2];
            var D = array1[3];
            
            if ((C >= A && D >= B) ||
                (C < A))
            { 
                Console.WriteLine("No"); return; 
            }

            Console.WriteLine("Yes");
        }

        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            var N = array1[0];
            var M = array1[1];

            List<string> input = new List<string>();

            for (int i = 0; i < N; i++)
            {
                input.Add(Console.ReadLine());
            }

            var gridSet = new HashSet<string>();

            for (int i = 0; i <= N - M; i++) 
            {
                for (int j = 0; j <= N - M; j++) 
                {
                    var parts = new List<string>();
                    for (int k = i; k < i + M; k++)
                    {
                        parts.Add(input[k].Substring(j, M));
                    }

                    string grid = string.Join("\n", parts);
                    gridSet.Add(grid);
                }
            }

            Console.WriteLine(gridSet.Count);
        }

        public void QuestionC() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var S2 = Console.ReadLine();

            var N = array1[0];
            var A = array1[1];
            var B = array1[2];

            int[] sumA = new int[N + 1];
            int[] sumB = new int[N + 1];

            for (int i = 0; i < N; i++) 
            {
                if (S2[i] == 'a')
                {
                    sumA[i + 1]++;
                }
                else 
                {
                    sumB[i + 1]++;
                }

                sumA[i + 1] += sumA[i];
                sumB[i + 1] += sumB[i];
            }

            var ans = 0L;
            int AR = 0;
            int BR = 0;

            for (int L = 0; L < N; L++) 
            {
                while (AR <= N && sumA[AR] - sumA[L] < A) 
                {
                    AR++;
                }

                while (BR <= N && sumB[BR] - sumB[L] < B) 
                {
                    BR++;
                } 
                BR--;

                ans += Math.Max(0, BR - AR + 1);
            }
            Console.WriteLine(ans);
        }
    }
}
