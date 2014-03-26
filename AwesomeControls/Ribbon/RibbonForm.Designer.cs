namespace AwesomeControls.Ribbon
{
    partial class RibbonForm
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
            this.ribbon1 = new AwesomeControls.Ribbon.Ribbon();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.ApplicationButtonImage = null;
            this.ribbon1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbon1.IsMinimized = true;
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.MinimumParentHeight = 251;
            this.ribbon1.MinimumTabWidth = 54;
            this.ribbon1.Name = "ribbon1";
            this.ribbon1.SelectedTab = null;
            this.ribbon1.Size = new System.Drawing.Size(292, 24);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.TabSpacing = 4;
            this.ribbon1.Text = "ribbon1";
            // 
            // RibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.ribbon1);
            this.Name = "RibbonForm";
            this.Text = "RibbonForm";
            this.ResumeLayout(false);
        }

        #endregion

        private Ribbon ribbon1;
    }
}