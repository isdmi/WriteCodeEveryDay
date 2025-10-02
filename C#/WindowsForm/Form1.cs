using System.Diagnostics;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Collection testClass = new Collection();
            testClass.Test();
        }
    }
}
