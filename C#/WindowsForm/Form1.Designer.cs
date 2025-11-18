namespace Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            aiControlButton1 = new AiCustomControl.AiControlButton();
            aiTextBox1 = new AiCustomControl.AiTextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(20, 21);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 20);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // aiControlButton1
            // 
            aiControlButton1.Location = new Point(354, 118);
            aiControlButton1.Name = "aiControlButton1";
            aiControlButton1.Size = new Size(75, 23);
            aiControlButton1.TabIndex = 1;
            aiControlButton1.Text = "aiControlButton1";
            aiControlButton1.UseVisualStyleBackColor = true;
            // 
            // aiTextBox1
            // 
            aiTextBox1.Location = new Point(384, 173);
            aiTextBox1.Name = "aiTextBox1";
            aiTextBox1.Size = new Size(100, 23);
            aiTextBox1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(aiTextBox1);
            Controls.Add(aiControlButton1);
            Controls.Add(button1);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private AiCustomControl.AiControlButton aiControlButton1;
        private AiCustomControl.AiTextBox aiTextBox1;
    }
}
