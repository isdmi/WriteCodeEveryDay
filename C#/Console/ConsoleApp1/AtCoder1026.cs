using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder1026
    {
        // 正整数 
        // N,M が与えられます。
        // N 行出力してください。
        // i 行目 
        // (1≤i≤N) には、
        // i≤M のとき OK を、 そうでないとき、Too Many Requests を出力してください。
        public void QuestionA()
        {
            var S = Console.ReadLine();

            if (S == null) return;

            var array = S.Split(' ').Select(x => int.Parse(x)).ToList();
            var N = array[0];
            var M = array[1];

            for (int i = 0; i < N; i++) 
            {
                if (M > i)
                {
                    Console.WriteLine("OK");
                }
                else 
                {
                    Console.WriteLine("Too Many Requests");
                }
            }
        }

        // 長さ N の整数列 A=(A 1 ,A 2 ,…,A N ) と整数 M が与えられます。
        // A の N 個の要素から 1 個を取り除くことで、残りの N−1 個の要素の和をちょうど M にできるか判定してください。
        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            var S2 = Console.ReadLine();

            if (S1 == null || S2 == null) return;

            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var N = array1[0];
            var M = array1[1];

            var array2 = S2.Split(' ').Select(x => int.Parse(x)).ToArray();
            var sum = array2.Sum();
            var result = false;
            for (int i = 0; i < N; i++) 
            {
                if (sum - array2[i] == M) 
                {
                    result = true;
                    break;
                }            
            }
            if (result) { 
                Console.WriteLine("Yes "); 
            }
            else {
                Console.WriteLine("No");
            }
        }

        // 長さ N の整数列 A=(A 1​ ,A 2​ ,…,A N​ ) が与えられます。1≤i<j<k≤N をみたす整数の組 (i,j,k) であって、 次の条件をみたすものの個数を求めてください。
        // A i​ ,A j​ ,A k​  の中にちょうど 2 種類の値が含まれる。すなわち、A i​ ,A j​ ,A k​  の中のいずれか 2 つは等しく、残りの 1 つは異なる。
        public void QuestionC() {
            var S1 = Console.ReadLine();
            var S2 = Console.ReadLine();

            if (S1 == null || S2 == null) return;

            var N = int.Parse(S1);
            var array = S2.Split(' ').Select(x => int.Parse(x)).ToArray();

            long[] count = new long[N + 1];

            foreach (int i in array)
            {
               count[i]++;
            }

            long ans = 0;

            for (int i = 0; i < N; i++)
            {
                ans += count[i] * (count[i] - 1) * (N - count[i]) / 2;
            }
            Console.WriteLine(ans);
        }
    }
}
