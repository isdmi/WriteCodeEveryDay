using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public class AtCoder1108
    {
        public void QuestionA()
        {
            var S1 = Console.ReadLine();
            if (S1 == null) return;

            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var H = array1[0];
            var B = array1[1];

            var ans = H - B;
            if (ans <= 0)
            {
                ans = 0;
            }

            Console.WriteLine(ans);
        }

        public void QuestionB()
        {
            var S1 = Console.ReadLine();
            var S2 = Console.ReadLine();
            var S3 = Console.ReadLine();
            var S4 = Console.ReadLine();

            var X = Int32.Parse(S1);
            var N = Int32.Parse(S2);
            var W = S3.Split(' ').Select(x => int.Parse(x)).ToArray();
            var Q = Int32.Parse(S4);

            List<int> queryList = new List<int>();
            bool[] flags = new bool[N];

            for (int i = 0; i < Q; i++)
            {
                var input = Console.ReadLine();
                queryList.Add(Int32.Parse(input));
            }

            foreach (int query in queryList)
            {
                int idx = query - 1;
                if (flags[idx])
                {
                    X = X - W[idx];
                    flags[idx] = false;
                }
                else 
                {
                    X = X + W[idx];
                    flags[idx] = true;
                }

                Console.WriteLine(X);
            }
        }

        public void QuestionC()
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var S2 = Console.ReadLine();
            var array2 = S2.Split(' ').Select(x => int.Parse(x)).ToArray();
            var S3 = Console.ReadLine();
            var array3 = S3.Split(' ').Select(x => int.Parse(x)).ToArray();

            var N = array1[0];
            var M = array1[1];
            var K = array1[2];

            Array.Sort(array2);
            Array.Sort(array3);

            int robot = 0;
            int j = 0;

            for (int i = 0; i < N && j < M; i++)
            {
                while (j < M && array3[j] < array2[i])
                {
                    j++;
                }

                if (j == M) break;

                robot++;
                j++;

                if (robot == K)
                {
                    Console.WriteLine("Yes");
                    return;
                }
            }

            Console.WriteLine("No");
        }
    }
}
