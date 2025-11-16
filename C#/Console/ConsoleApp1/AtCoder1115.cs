using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AtCoder1115
    {
        public void QuestionA() 
        {
            var S1 = Console.ReadLine();
            var array1 = S1.Split(' ').Select(x => int.Parse(x)).ToArray();

            Array.Sort(array1);

            var A = array1[0].ToString();
            var B = array1[1].ToString();
            var C = array1[2].ToString();

            Console.WriteLine(C + B + A);            
        }

        public void QuestionB() 
        {
            var S1 = Console.ReadLine();
            var x = S1.Split(' ').Select(x => int.Parse(x)).ToArray();
            var charList = x[0].ToString().ToCharArray().OrderBy(x => x).ToList();
            
            if (charList.Any(y => y == '0'))
            {
                var i = charList.Where(x => x != '0').Min();

                charList.RemoveAt(charList.IndexOf(i));
                charList.Insert(0, i);
            }

            Console.WriteLine(new String(charList.ToArray()));
        }

        public void QuestionC()
        {
            var S1 = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var S2 = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

            var n = S1[0];
            var x = S1[1];
            var y = S1[2];

            long minValue = S2.Min();
            long maxValue = S2.Max();

            if (y * minValue < x * maxValue)
            {
                Console.WriteLine("-1");
                return;
            }
            else 
            {
                long z = y - x;
                HashSet<long> modList = new HashSet<long>();

                for (int i = 0; i < n; i++)
                {
                    modList.Add((x * S2[i]) % z);
                }

                if (modList.Count > 1) 
                {
                    Console.WriteLine("-1");
                    return;
                }

                long g = minValue * y;

                long ans = 0;

                for (int i = 0; i < n; i++) 
                {
                    ans += (g - x * S2[i]) / z;
                }

                Console.WriteLine(ans);
            }

        }
    }
}
