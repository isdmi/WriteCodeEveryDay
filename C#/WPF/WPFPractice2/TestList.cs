using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WPFPractice2
{
    public class TestList
    {
        // バインディングの指定先プロパティ
        public ObservableCollection<Test> Data { get; }

        // コンストラクタ(データ入力)
        public TestList()
        {
            Data = new ObservableCollection<Test> {
                new Test { Subj="国語", Points=90, Name="田中　一郎", ClassName="A" },
                new Test { Subj="数学", Points=50, Name="鈴木　二郎", ClassName="A" },
                new Test { Subj="英語", Points=90, Name="佐藤　三郎", ClassName="B" }
            };
        }
    }
}
