using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice2
{
    public class Test
    {
        public string Subj { get; set; }
        public int Points { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }

        public Test() 
        {
            Subj = string.Empty;
            Points = 0;
            Name = string.Empty;
            ClassName = string.Empty;
        }
    }
}
