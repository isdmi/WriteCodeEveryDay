using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AtCoder1004
    {
        public void Test() 
        {
            var S = Console.ReadLine();

            if (S == null) return;

            List<string> strings = new List<string>() { "Ocelot", "Serval", "Lynx" };

            string[] stringArray = S.Split(' ');

            var one = strings.IndexOf(stringArray[0]);
            var two = strings.IndexOf(stringArray[1]);


            if (one >= two)
            {
                Console.WriteLine("Yes");
                return;
            }

            Console.WriteLine("No");
        }

        public void OddOne() 
        {
            var S = Console.ReadLine();

            if (S == null) return;

            var result = S.ToCharArray()
                            .GroupBy(x => x)
                            .Select(strings => new { strings.Key, Count = strings.Count() })
                            .Single(x => x.Count == 1);

            Console.WriteLine(result.Key);
        }

        public void UpgradeRequired() 
        {
            var S = Console.ReadLine();

            if (S == null) return;

            string[] stringArray = S.Split(' ');

            int pcNumber = Convert.ToInt32(stringArray[0]);
            int count = Convert.ToInt32(stringArray[1]);
 
            string[] input = new string[count];
            int[] resultList = Enumerable.Range(1, pcNumber).ToArray();

            for (int i = 0; i < count; i++)
            {
                var N = Console.ReadLine();
                if (N == null) return;

                input[i] = N;
            }

            for (int i = 0; i < count; i++)
            {
                string[] operation = input[i].Split(' ');
                int oldVersion = Convert.ToInt32(operation[0]);
                int newVersion = Convert.ToInt32(operation[1]);

                int upgradeCount = 0;

                for (int j = 0; j < pcNumber; j++)
                {
                    if (resultList[j] <= oldVersion)
                    {
                        resultList[j] = newVersion;
                        upgradeCount = upgradeCount + 1;
                    }
                }

                Console.WriteLine(upgradeCount.ToString());
            }
        }
    }
}
