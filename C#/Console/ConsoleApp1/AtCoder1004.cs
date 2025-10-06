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

            int pcNumber = int.Parse(stringArray[0]);
            int count = int.Parse(stringArray[1]);
 
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
                int oldVersion = int.Parse(operation[0]);
                int newVersion = int.Parse(operation[1]);

                int upgradeCount = 0;

                for (int j = 0; j < pcNumber; j++)
                {
                    if (resultList[j] <= oldVersion)
                    {
                        resultList[j] = newVersion;
                        upgradeCount = upgradeCount + 1;
                    }

                    if (j > oldVersion) 
                    {
                        break;
                    }
                }

                Console.WriteLine(upgradeCount.ToString());
            }
        }

        public void UpgradeRequired2()
        {
            var S = Console.ReadLine();

            if (S == null) return;

            string[] stringArray = S.Split(' ');

            int pcNumber = int.Parse(stringArray[0]);
            int count = int.Parse(stringArray[1]);

            int[] resultList = Enumerable.Range(1, pcNumber).ToArray();
            int[] verupCount = new int[count];

            int minVersion = 0;

            for (int i = 0; i < count; i++)
            {
                var N = Console.ReadLine();
                if (N == null) return;

                string[] operation = N.Split(' ');
                int oldVersion = int.Parse(operation[0]);
                int newVersion = int.Parse(operation[1]);

                if (oldVersion < minVersion) 
                {
                    continue;
                }

                int upgradeCount = 0;

                for (int j = 0; j < pcNumber; j++)
                {
                    if (resultList[j] <= oldVersion)
                    {
                        resultList[j] = newVersion;
                        upgradeCount = upgradeCount + 1;
                    }

                    if (j > oldVersion)
                    {
                        break;
                    }
                }

                verupCount[i] = upgradeCount;

                if (minVersion < newVersion) 
                {
                    minVersion = newVersion;
                }
            }

            foreach (int i in verupCount) 
            {
                Console.WriteLine(i);
            }
        }
    }
}
