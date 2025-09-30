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
            // Count By‚ÌŽÀ‘•
            int[] randomDiceNumbers = [1, 2, 2, 4, 2, 6, 1, 5, 3, 2, 1, 3, 4, 5, 6, 3, 1, 1, 3, 4, 2, 6, 5, 4, 1, 3, 4, 1, 3, 6, 4, 2, 1];

            var res = randomDiceNumbers.CountBy(x => x).ToList();
            res.ForEach(x => Debug.WriteLine(x));
        }
    }
}
