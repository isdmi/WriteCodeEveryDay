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
    public class AtCoder1129
    {
        public void QuestionA() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            var W = array1[0];
            var B = array1[1];

            var ans = (W * 1000 / B) + 1;

            Console.WriteLine(ans);
        }

        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            var N = array1[0];
            var M = array1[1];

            decimal[] birdWeight = new decimal[M];
            decimal[] birdCount = new decimal[M];

            for (int i = 0; i < N; i++) 
            {
                var S2 = Console.ReadLine();
                var array2 = S2.Split(' ').Select(x => int.Parse(x)).ToArray();

                var A = array2[0];
                var B = array2[1];

                birdWeight[A - 1] = birdWeight[A - 1] + B;
                birdCount[A - 1] = birdCount[A - 1] + 1;
            }

            for (int i = 0; i < M; i++) 
            {
                Console.WriteLine((birdWeight[i] / birdCount[i]).ToString("F20"));
            }

        }
    }
}
