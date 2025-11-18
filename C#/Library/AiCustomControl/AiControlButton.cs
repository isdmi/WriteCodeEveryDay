using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomControl
{
    public class AiControlButton : Button
    {
        List<AiTextBox> _AiTextBoxList = new List<AiTextBox>();

        public AiControlButton()
        {
            this.Click += AiControlButton_Click;
        }

        [Browsable(true)]
        [Description("BindTextBox")]
        [Category("Custom")]
        public List<AiTextBox> AiTextBoxList
        {
            get { return _AiTextBoxList; }
            set { _AiTextBoxList = value; }
        }

        private void AiControlButton_Click(object? sender, EventArgs e)
        {
            if (_AiTextBoxList.Any()) 
            {
                if (Clipboard.ContainsText())
                {
                    string Text = Clipboard.GetText();


                }
            }

        }
    }
}
