namespace Casino777
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            bindingBalance = new BindingSource(components);
            timerSlots = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)bindingBalance).BeginInit();
            SuspendLayout();
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 761);
            Font = new Font("Impact", 14.25F);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "Casino777";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)bindingBalance).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BindingSource bindingBalance;
        private System.Windows.Forms.Timer timerSlots;
    }
}
