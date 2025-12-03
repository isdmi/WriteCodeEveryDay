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

        public void QuestionC() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            var T = array1[0];

            for (int i = 0; i < T; i++)
            {
                var S2 = Console.ReadLine();
                var array2 = S2.Split(' ').Select(x => int.Parse(x)).ToArray();

                var N = array2[0];  // テストケース数
                var H = array2[1];  // 最初の高度

                var TestCaseList = new List<int[]>();

                for (int j = 0; j < N; j++)
                {
                    var S3 = Console.ReadLine();
                    var array3 = S3.Split(' ').Select(x => int.Parse(x)).ToArray();
                    TestCaseList.Add(array3);
                }

                int l = H;
                int u = H;
                int t = 0;

                var CanFly = true;

                foreach (var array3 in TestCaseList) 
                {
                    var T1 = array3[0];  // 何秒後か
                    var L1 = array3[1];  // 最小高度
                    var U1 = array3[2];  // 最大高度

                    var distance = T1 - t;  // 計算する距離

                    l = l - distance;   // 現在の最低高度から取りうる最低高度を求める
                    u = u + distance;   // 現在の最高高度から取りうる最高高度を求める

                    l = Math.Max(l, L1);    // 目標最小値と取れる最低高度の比較
                    u = Math.Min(u, U1);    // 目標最大値と取れる最高高度の比較
                    t = T1;

                    if (l > u)  // 最低高度と最高高度の関係がおかしくなったら、失敗を返す
                    {
                        CanFly = false;
                        break;
                    }
                }

                if (CanFly)
                {
                    Console.WriteLine("Yes");
                }
                else 
                {
                    Console.WriteLine("No");
                }
            }
        }
    }
}
