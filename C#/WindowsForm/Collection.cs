using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Collection
    {
        public void Test() 
        {
            // コレクション式について

            // 今まではこう書いてた。
            List<int> oldList = new List<int>() { 1, 2, 3 };

            // C#12からはこう書くことができる。
            List<int> newList = [1, 2, 3];
            List<string> hogeList = ["AAA", "BBB", "CCC"];

            // Spanについて　C#7
            // 配列から一部だけ抜き出すようなものと解釈・・・
            // IEnumerableではないのでLINQは使えない。
            string[] stringArray = ["1", "2", "3", "4", "5"];
            Span<String> stringSpan = stringArray.AsSpan(0, 3);

            foreach (string Fuga in stringSpan) 
            {
                Debug.WriteLine(Fuga); // この結果は1,2,3になる。
            }
        }
    }
}
