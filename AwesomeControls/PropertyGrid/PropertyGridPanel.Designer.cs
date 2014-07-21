namespace AwesomeControls.PropertyGrid
{
    partial class PropertyGridPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.vsc = new System.Windows.Forms.VScrollBar();
			this.pnlProperties = new AwesomeControls.PropertyGrid.DBPanel();
			this.txt = new System.Windows.Forms.TextBox();
			this.dummy = new System.Windows.Forms.Button();
			this.pnlProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// vsc
			// 
			this.vsc.Dock = System.Windows.Forms.DockStyle.Right;
			this.vsc.Location = new System.Drawing.Point(297, 0);
			this.vsc.Name = "vsc";
			this.vsc.Size = new System.Drawing.Size(16, 224);
			this.vsc.TabIndex = 2;
			this.vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsc_Scroll);
			// 
			// pnlProperties
			// 
			this.pnlProperties.Controls.Add(this.dummy);
			this.pnlProperties.Controls.Add(this.txt);
			this.pnlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlProperties.Location = new System.Drawing.Point(0, 0);
			this.pnlProperties.Name = "pnlProperties";
			this.pnlProperties.Size = new System.Drawing.Size(297, 224);
			this.pnlProperties.TabIndex = 3;
			this.pnlProperties.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProperties_Paint);
			this.pnlProperties.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlProperties_MouseDoubleClick);
			this.pnlProperties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlProperties_MouseDown);
			this.pnlProperties.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlProperties_MouseUp);
			this.pnlProperties.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pnlProperties_MouseWheel);
			// 
			// txt
			// 
			this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txt.Location = new System.Drawing.Point(140, 64);
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(70, 13);
			this.txt.TabIndex = 0;
			this.txt.Visible = false;
			this.txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
			this.txt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
			// 
			// dummy
			// 
			this.dummy.Location = new System.Drawing.Point(-180, 65);
			this.dummy.Name = "dummy";
			this.dummy.Size = new System.Drawing.Size(21, 10);
			this.dummy.TabIndex = 1;
			this.dummy.Text = "button1";
			this.dummy.UseVisualStyleBackColor = true;
			// 
			// PropertyGridPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlProperties);
			this.Controls.Add(this.vsc);
			this.Name = "PropertyGridPanel";
			this.Size = new System.Drawing.Size(313, 224);
			this.pnlProperties.ResumeLayout(false);
			this.pnlProperties.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vsc;
        private DBPanel pnlProperties;
        private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.Button dummy;
    }
}
