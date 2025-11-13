using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomControl
{
    public class AiControlButton : Button
    {
        TextBox textBox;

        public AiControlButton()
        {
            // TextBox
            textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;

            this.Text = "貼り付け";
            this.Dock = DockStyle.Right;
            this.Width = 80;
            this.Click += PasteButton_Click;
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                textBox.Text = Clipboard.GetText();
            }
            else
            {
                MessageBox.Show("クリップボードにテキストがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 外部からTextBox内容にアクセスできるようにする
        public string TextValue
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }
    }
}
