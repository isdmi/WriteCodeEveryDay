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

        public void LightItUp() 
        {


        }
    }
}
