namespace AwesomeControls.MultipleDocumentContainer
{
    partial class MultipleDocumentContainerControl
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
            this.components = new System.ComponentModel.Container();
            this.tbWindowSwitcher = new AwesomeControls.CommandBars.CBToolBar();
            this.mnuDocumentTabContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.mnuDocumentTabContextSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDocumentTabContextClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDocumentTabContextCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDocumentTabContextCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyFullPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.floatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floatAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.pinTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.newHorizontalTabGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newVerticalTabGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlControlContainer = new System.Windows.Forms.Panel();
            this.mnuDocumentTabContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbWindowSwitcher
            // 
            this.tbWindowSwitcher.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tbWindowSwitcher.Location = new System.Drawing.Point(0, 0);
            this.tbWindowSwitcher.Name = "tbWindowSwitcher";
            this.tbWindowSwitcher.ShowItemToolTips = false;
            this.tbWindowSwitcher.Size = new System.Drawing.Size(553, 25);
            this.tbWindowSwitcher.TabIndex = 1;
            this.tbWindowSwitcher.Text = "cbToolBar1";
            // 
            // mnuDocumentTabContext
            // 
            this.mnuDocumentTabContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDocumentTabContextSave,
            this.mnuDocumentTabContextClose,
            this.mnuDocumentTabContextCloseAll,
            this.mnuDocumentTabContextCloseAllButThis,
            this.toolStripMenuItem1,
            this.copyFullPathToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem,
            this.toolStripMenuItem2,
            this.floatToolStripMenuItem,
            this.floatAllToolStripMenuItem,
            this.toolStripMenuItem3,
            this.pinTabToolStripMenuItem,
            this.toolStripMenuItem4,
            this.newHorizontalTabGroupToolStripMenuItem,
            this.newVerticalTabGroupToolStripMenuItem});
            this.mnuDocumentTabContext.Name = "cbContextMenu1";
            this.mnuDocumentTabContext.Size = new System.Drawing.Size(216, 270);
            // 
            // mnuDocumentTabContextSave
            // 
            this.mnuDocumentTabContextSave.Name = "mnuDocumentTabContextSave";
            this.mnuDocumentTabContextSave.Size = new System.Drawing.Size(215, 22);
            this.mnuDocumentTabContextSave.Text = "&Save [filename]";
            // 
            // mnuDocumentTabContextClose
            // 
            this.mnuDocumentTabContextClose.Name = "mnuDocumentTabContextClose";
            this.mnuDocumentTabContextClose.Size = new System.Drawing.Size(215, 22);
            this.mnuDocumentTabContextClose.Text = "&Close";
            this.mnuDocumentTabContextClose.Click += new System.EventHandler(this.mnuDocumentTabContextClose_Click);
            // 
            // mnuDocumentTabContextCloseAll
            // 
            this.mnuDocumentTabContextCloseAll.Name = "mnuDocumentTabContextCloseAll";
            this.mnuDocumentTabContextCloseAll.Size = new System.Drawing.Size(215, 22);
            this.mnuDocumentTabContextCloseAll.Text = "C&lose All Documents";
            this.mnuDocumentTabContextCloseAll.Click += new System.EventHandler(this.mnuDocumentTabContextCloseAll_Click);
            // 
            // mnuDocumentTabContextCloseAllButThis
            // 
            this.mnuDocumentTabContextCloseAllButThis.Name = "mnuDocumentTabContextCloseAllButThis";
            this.mnuDocumentTabContextCloseAllButThis.Size = new System.Drawing.Size(215, 22);
            this.mnuDocumentTabContextCloseAllButThis.Text = "Close &All But This";
            this.mnuDocumentTabContextCloseAllButThis.Click += new System.EventHandler(this.mnuDocumentTabContextCloseAllButThis_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
            // 
            // copyFullPathToolStripMenuItem
            // 
            this.copyFullPathToolStripMenuItem.Name = "copyFullPathToolStripMenuItem";
            this.copyFullPathToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.copyFullPathToolStripMenuItem.Text = "Copy F&ull Path";
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.openContainingFolderToolStripMenuItem.Text = "&Open Containing Folder";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(212, 6);
            // 
            // floatToolStripMenuItem
            // 
            this.floatToolStripMenuItem.Name = "floatToolStripMenuItem";
            this.floatToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.floatToolStripMenuItem.Text = "&Float";
            // 
            // floatAllToolStripMenuItem
            // 
            this.floatAllToolStripMenuItem.Name = "floatAllToolStripMenuItem";
            this.floatAllToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.floatAllToolStripMenuItem.Text = "Floa&t All";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(212, 6);
            // 
            // pinTabToolStripMenuItem
            // 
            this.pinTabToolStripMenuItem.Name = "pinTabToolStripMenuItem";
            this.pinTabToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.pinTabToolStripMenuItem.Text = "&Pin Tab";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(212, 6);
            // 
            // newHorizontalTabGroupToolStripMenuItem
            // 
            this.newHorizontalTabGroupToolStripMenuItem.Name = "newHorizontalTabGroupToolStripMenuItem";
            this.newHorizontalTabGroupToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.newHorizontalTabGroupToolStripMenuItem.Text = "New Hori&zontal Tab Group";
            // 
            // newVerticalTabGroupToolStripMenuItem
            // 
            this.newVerticalTabGroupToolStripMenuItem.Name = "newVerticalTabGroupToolStripMenuItem";
            this.newVerticalTabGroupToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.newVerticalTabGroupToolStripMenuItem.Text = "New &Vertical Tab Group";
            // 
            // pnlControlContainer
            // 
            this.pnlControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControlContainer.Location = new System.Drawing.Point(0, 25);
            this.pnlControlContainer.Name = "pnlControlContainer";
            this.pnlControlContainer.Size = new System.Drawing.Size(553, 303);
            this.pnlControlContainer.TabIndex = 2;
            this.pnlControlContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlControlContainer_Paint);
            // 
            // MultipleDocumentContainerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlControlContainer);
            this.Controls.Add(this.tbWindowSwitcher);
            this.Name = "MultipleDocumentContainerControl";
            this.Size = new System.Drawing.Size(553, 328);
            this.mnuDocumentTabContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AwesomeControls.CommandBars.CBToolBar tbWindowSwitcher;
        private AwesomeControls.CommandBars.CBContextMenu mnuDocumentTabContext;
        private System.Windows.Forms.ToolStripMenuItem mnuDocumentTabContextSave;
        private System.Windows.Forms.ToolStripMenuItem mnuDocumentTabContextClose;
        private System.Windows.Forms.ToolStripMenuItem mnuDocumentTabContextCloseAll;
        private System.Windows.Forms.ToolStripMenuItem mnuDocumentTabContextCloseAllButThis;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyFullPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem floatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floatAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem pinTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem newHorizontalTabGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newVerticalTabGroupToolStripMenuItem;
        private System.Windows.Forms.Panel pnlControlContainer;
    }
}
