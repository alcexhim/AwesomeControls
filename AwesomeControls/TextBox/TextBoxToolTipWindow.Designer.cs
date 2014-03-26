namespace AwesomeControls.TextBox
{
    partial class TextBoxToolTipWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.BackColor = System.Drawing.SystemColors.Info;
            this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Name = "txt";
            this.txt.ReadOnly = true;
            this.txt.Size = new System.Drawing.Size(334, 140);
            this.txt.TabIndex = 0;
            this.txt.Text = "";
            // 
            // TextBoxToolTipWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 140);
            this.ControlBox = false;
            this.Controls.Add(this.txt);
            this.Name = "TextBoxToolTipWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.RichTextBox txt;
    }
}