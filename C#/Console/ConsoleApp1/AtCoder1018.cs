using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ConsoleApp1
{
    internal class AtCoder1018
    {
        public void QuestionA() 
        {
            var S = Console.ReadLine();

            if (S == null) return;

            var array = S.Split(' ').Select(x => int.Parse(x)).ToList();

            int total = array[3];
            int answer = 0;
            while (true) 
            {
                if (total >= 0 && total >= array[1])
                {
                    answer = answer + (array[0] * array[1]);
                    total = total - array[1];
                }
                else 
                {
                    answer = answer + (total * array[0]);
                    break;
                }
                if (total >= 0 && total >= array[2])
                {
                    total = total - array[2];
                }
                else 
                {
                    break;
                }
            }

            Console.WriteLine(answer);
        }

        public void QuestionB() 
        {
            var N = Console.ReadLine();
            var S = Console.ReadLine();

            var array = N.Split(' ').Select(x => int.Parse(x)).ToList();


            List<(string, int)> values = new List<(string, int)>();

            for (int i = 0; i < array[0] - array[1] + 1; i++) 
            {
                var searchString = S.Substring(i, array[1]);

                if (values.Any(x => x.Item1 == searchString)) 
                {
                    continue; 
                }

                var targetString = S;
                var count = 0;
                while (true) 
                {
                    var idx = targetString.IndexOf(searchString);
                    if (idx >= 0)
                    {
                        count++;
                        targetString = targetString.Substring(idx + 1);
                    }
                    else 
                    {
                        break;
                    }
                }

                values.Add((searchString, count));
            }
            if (values.Any())
            {
                int result = values.Max(x => x.Item2);
                var list = values.Where(x => x.Item2 == result).Select(x => x.Item1).ToList();
                list.Sort();
                Console.WriteLine(result);
                Console.WriteLine(string.Join(" ", list));
            }
            else 
            {
                Console.WriteLine(0);
                Console.WriteLine();
            }            
        }

        public void QuestionC() 
        {
            var S = int.Parse(Console.ReadLine());
            var stack = new Stack<char>();
            var results = new StringBuilder();
            int balance = 0;

            for (int i = 0; i < S; i++)
            {
                var N = Console.ReadLine().Split(' ');

                if (N[0] == "1")
                {
                    char pushed = N[1][0];
                    stack.Push(N[1][0]);
                    if (pushed == '(')
                    {
                        balance += 1;
                    }
                    else
                    {
                        balance -= 1;
                    }
                }
                else
                {
                    char removed = stack.Pop();
                    if (removed == '(')
                    {
                        balance -= 1;
                    }
                    else
                    {
                        balance += 1;
                    }
                }

                if (balance == 0)
                {
                    results.AppendLine("Yes");
                }
                else
                {
                    results.AppendLine("No");
                }
            }
            Console.WriteLine(results.ToString());
        }
    }
}
