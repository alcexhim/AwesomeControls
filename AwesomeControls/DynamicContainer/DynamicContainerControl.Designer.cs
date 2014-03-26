namespace AwesomeControls.DynamicContainer
{
    partial class DynamicContainerControl
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
			this.pnlContainer = new System.Windows.Forms.Panel();
			this.tb = new AwesomeControls.CommandBars.CBToolBar();
			this.lblWindowTitle = new System.Windows.Forms.ToolStripLabel();
			this.tsbWindowList = new System.Windows.Forms.ToolStripDropDownButton();
			this.tb.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlContainer
			// 
			this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContainer.Location = new System.Drawing.Point(0, 25);
			this.pnlContainer.Name = "pnlContainer";
			this.pnlContainer.Size = new System.Drawing.Size(291, 243);
			this.pnlContainer.TabIndex = 0;
			// 
			// tb
			// 
			this.tb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblWindowTitle,
            this.tsbWindowList});
			this.tb.Location = new System.Drawing.Point(0, 0);
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(291, 25);
			this.tb.TabIndex = 1;
			this.tb.Text = "cbToolBar1";
			this.tb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
			this.tb.MouseEnter += new System.EventHandler(this.tb_MouseEnter);
			this.tb.MouseLeave += new System.EventHandler(this.tb_MouseLeave);
			this.tb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tb_MouseMove);
			// 
			// lblWindowTitle
			// 
			this.lblWindowTitle.Name = "lblWindowTitle";
			this.lblWindowTitle.Size = new System.Drawing.Size(74, 22);
			this.lblWindowTitle.Text = "Window title";
			this.lblWindowTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
			// 
			// tsbWindowList
			// 
			this.tsbWindowList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbWindowList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbWindowList.Image = global::AwesomeControls.Properties.Resources.DropDown;
			this.tsbWindowList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbWindowList.Name = "tsbWindowList";
			this.tsbWindowList.ShowDropDownArrow = false;
			this.tsbWindowList.Size = new System.Drawing.Size(20, 22);
			this.tsbWindowList.Text = "Window List";
			this.tsbWindowList.MouseEnter += new System.EventHandler(this.tsbWindowList_MouseEnter);
			// 
			// DynamicContainerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlContainer);
			this.Controls.Add(this.tb);
			this.Name = "DynamicContainerControl";
			this.Size = new System.Drawing.Size(291, 268);
			this.tb.ResumeLayout(false);
			this.tb.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private CommandBars.CBToolBar tb;
        private System.Windows.Forms.ToolStripDropDownButton tsbWindowList;
        private System.Windows.Forms.ToolStripLabel lblWindowTitle;
		internal System.Windows.Forms.Panel pnlContainer;
    }
}
