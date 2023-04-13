namespace DemoApp
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
            Txt = new TextBox();
            SuspendLayout();
            // 
            // Txt
            // 
            Txt.Dock = DockStyle.Fill;
            Txt.Location = new Point(0, 0);
            Txt.Multiline = true;
            Txt.Name = "Txt";
            Txt.ReadOnly = true;
            Txt.ScrollBars = ScrollBars.Vertical;
            Txt.Size = new Size(1437, 1027);
            Txt.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(18F, 45F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1437, 1027);
            Controls.Add(Txt);
            Name = "Form1";
            Text = "Demo app";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Txt;
    }
}