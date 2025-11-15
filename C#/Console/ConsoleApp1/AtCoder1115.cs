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
    }
}
