using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TestClass
    {
        public void CamelCase()
        {
            // 英大文字および英小文字からなる文字列 
            // S が与えられます。
            // ここで、 
            // S のうちちょうど 
            // 1 文字だけが英大文字であり、それ以外は全て英小文字です。
            // 大文字が 
            // S の先頭から何文字目に登場するか出力してください。
            // ただし、
            // S の先頭の文字を 
            // 1 文字目とします。

            var S = Console.ReadLine();
            int i = 0;
            if (S != null)
            {
                foreach (var item in S.ToCharArray())
                {
                    i++;
                    if (Char.IsUpper(item))
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }

        public void NotOverflow()
        {
            // 整数 
            // N が与えられます。 
            // N が 
            // −2 
            // 31
            //   以上かつ 
            // 2 
            // 31
            //   未満ならば Yes を、そうでないならば No を出力してください。
            var S = Console.ReadLine();
            if (S == null) return;

            try
            {
                checked
                {
                    int Result = Convert.ToInt32(S);
                }
                Console.WriteLine("Yes");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("No");
            }
        }

        public void Sheeted() 
        {
            var N = Console.ReadLine();
            var S = Console.ReadLine();

            if (S == null || N == null) return;

            int count = Convert.ToInt32(N);
            char[] item = S.ToCharArray();
            int result = 0;

            for (int i = 0; i < count - 2; i++)
            {
                if (item[i] == '#' && item[i + 2] == '#' && item[i + 1] == '.')
                {
                    result++;
                }
            }
            Console.WriteLine(result);
        }

        public void ArithmeticProgression() 
        {
            var S = Console.ReadLine();

            if (S == null) return;

            string[] stringArray = S.Split(' ');
            int Start = Convert.ToInt32(stringArray[0]);
            int End = Convert.ToInt32(stringArray[1]);
            int Kosa = Convert.ToInt32(stringArray[2]);

            List<string> Result = new List<string>();

            for (int i = Start; i <= End; i = i + Kosa) 
            {
                Result.Add(i.ToString());
            }
            Console.WriteLine(string.Join(" ",Result ));
        }

        public void Racecar() 
        {
            var S = Console.ReadLine();

            if (S == null) return;
            int count = Convert.ToInt32(S);
            string[] strings = new string[count];
            bool Result = false;

            for (int i = 0; i < count; i++)
            {
                var N = Console.ReadLine();
                if (N == null) return;

                strings[i] = N;
            }

            for (int i = 0; i < count; i++) 
            {
                for (int j = 0; j < count; j++) 
                {
                    if (i == j) continue;

                    // 連結
                    var checkString = strings[i] + strings[j];
                    var reverseString = new string(checkString.Reverse().ToArray());
                    if (checkString == reverseString) 
                    {
                        Result = true;
                        break;
                    }
                }
            }

            if (Result) 
            {
                Console.WriteLine("Yes");
                return;
            }

            Console.WriteLine("No");
        }
    }
}
