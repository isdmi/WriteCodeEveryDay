using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    public class AtCoder1123
    {
        public void QuestionA() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            var X = array1[0];
            var Y = array1[1];
            var Z = array1[2];


            bool flag = false;

            for (int i = 0; i < X; i++) 
            {
                if ((X + i) == (Y + i) * Z) 
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
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
            var N = int.Parse(S1);

            var S2 = Console.ReadLine();
            var array1 = S2.Split(' ').Select(x => int.Parse(x)).ToArray();

            for (int i = 0; i < N; i++) 
            {
                var ans = -1;
                for (int j = i - 1; j >= 0; j--) 
                {
                    if (array1[j] > array1[i]) 
                    {
                        ans = j + 1;
                        break;
                    }
                }
                Console.WriteLine(ans.ToString());
            }


        }

        public void QuestionC()
        {
            var S1 = Console.ReadLine();

            int n = S1.Length;
            int ans = 0;

            for (int i = 0; i < n - 1; i++) 
            {
                int a = Int32.Parse(S1[i].ToString()) + 1;
                int b = Int32.Parse(S1[i + 1].ToString());

                if (a != b) 
                {
                    continue;
                }

                int j = i;

                while (j != -1 && S1[j] == S1[i]) 
                {
                    j--;
                }

                int k = i + 1;

                while (k != n && S1[k] == S1[i + 1]) 
                {
                    k++;
                }

                ans = ans + Math.Min(i - j, k - i - 1);
            }

            Console.WriteLine(ans);
        }
    }
}
