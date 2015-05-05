namespace AwesomeControls.ListView
{
    partial class ListViewControl
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
			this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextViewThumbnails = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextViewTiles = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextViewIcons = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextViewList = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextViewDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.arrangeIconsByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.ascendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.descendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.moreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ascendingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.descendingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.moreToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuColumnContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuColumnContextSizeToFit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuColumnContextSizeAllToFit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuColumnContextSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuColumnContextSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuColumnContextMore = new System.Windows.Forms.ToolStripMenuItem();
			this.vsc = new System.Windows.Forms.VScrollBar();
			this.scrl = new System.Windows.Forms.Panel();
			this.hsc = new System.Windows.Forms.HScrollBar();
			this.tip = new System.Windows.Forms.ToolTip(this.components);
			this.txtRename = new System.Windows.Forms.TextBox();
			this.mnuContext.SuspendLayout();
			this.mnuColumnContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextView,
            this.arrangeIconsByToolStripMenuItem,
            this.groupByToolStripMenuItem,
            this.refreshToolStripMenuItem});
			this.mnuContext.Name = "mnuContext";
			this.mnuContext.Size = new System.Drawing.Size(124, 92);
			// 
			// mnuContextView
			// 
			this.mnuContextView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextViewThumbnails,
            this.mnuContextViewTiles,
            this.mnuContextViewIcons,
            this.mnuContextViewList,
            this.mnuContextViewDetails});
			this.mnuContextView.Name = "mnuContextView";
			this.mnuContextView.Size = new System.Drawing.Size(123, 22);
			this.mnuContextView.Text = "&View";
			// 
			// mnuContextViewThumbnails
			// 
			this.mnuContextViewThumbnails.Name = "mnuContextViewThumbnails";
			this.mnuContextViewThumbnails.Size = new System.Drawing.Size(137, 22);
			this.mnuContextViewThumbnails.Text = "T&humbnails";
			this.mnuContextViewThumbnails.Click += new System.EventHandler(this.mnuContextViewType_Click);
			// 
			// mnuContextViewTiles
			// 
			this.mnuContextViewTiles.Checked = true;
			this.mnuContextViewTiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuContextViewTiles.Name = "mnuContextViewTiles";
			this.mnuContextViewTiles.Size = new System.Drawing.Size(137, 22);
			this.mnuContextViewTiles.Text = "Tile&s";
			this.mnuContextViewTiles.Click += new System.EventHandler(this.mnuContextViewType_Click);
			// 
			// mnuContextViewIcons
			// 
			this.mnuContextViewIcons.Name = "mnuContextViewIcons";
			this.mnuContextViewIcons.Size = new System.Drawing.Size(137, 22);
			this.mnuContextViewIcons.Text = "Ico&ns";
			this.mnuContextViewIcons.Click += new System.EventHandler(this.mnuContextViewType_Click);
			// 
			// mnuContextViewList
			// 
			this.mnuContextViewList.Name = "mnuContextViewList";
			this.mnuContextViewList.Size = new System.Drawing.Size(137, 22);
			this.mnuContextViewList.Text = "&List";
			this.mnuContextViewList.Click += new System.EventHandler(this.mnuContextViewType_Click);
			// 
			// mnuContextViewDetails
			// 
			this.mnuContextViewDetails.Name = "mnuContextViewDetails";
			this.mnuContextViewDetails.Size = new System.Drawing.Size(137, 22);
			this.mnuContextViewDetails.Text = "&Details";
			this.mnuContextViewDetails.Click += new System.EventHandler(this.mnuContextViewType_Click);
			// 
			// arrangeIconsByToolStripMenuItem
			// 
			this.arrangeIconsByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.ascendingToolStripMenuItem,
            this.descendingToolStripMenuItem,
            this.toolStripMenuItem3,
            this.moreToolStripMenuItem});
			this.arrangeIconsByToolStripMenuItem.Name = "arrangeIconsByToolStripMenuItem";
			this.arrangeIconsByToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.arrangeIconsByToolStripMenuItem.Text = "S&ort by";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
			// 
			// ascendingToolStripMenuItem
			// 
			this.ascendingToolStripMenuItem.Name = "ascendingToolStripMenuItem";
			this.ascendingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.ascendingToolStripMenuItem.Text = "&Ascending";
			// 
			// descendingToolStripMenuItem
			// 
			this.descendingToolStripMenuItem.Name = "descendingToolStripMenuItem";
			this.descendingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.descendingToolStripMenuItem.Text = "&Descending";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 6);
			// 
			// moreToolStripMenuItem
			// 
			this.moreToolStripMenuItem.Name = "moreToolStripMenuItem";
			this.moreToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.moreToolStripMenuItem.Text = "&More...";
			// 
			// groupByToolStripMenuItem
			// 
			this.groupByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.ascendingToolStripMenuItem1,
            this.descendingToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.moreToolStripMenuItem1});
			this.groupByToolStripMenuItem.Name = "groupByToolStripMenuItem";
			this.groupByToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.groupByToolStripMenuItem.Text = "Grou&p by";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 6);
			// 
			// ascendingToolStripMenuItem1
			// 
			this.ascendingToolStripMenuItem1.Name = "ascendingToolStripMenuItem1";
			this.ascendingToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
			this.ascendingToolStripMenuItem1.Text = "&Ascending";
			// 
			// descendingToolStripMenuItem1
			// 
			this.descendingToolStripMenuItem1.Name = "descendingToolStripMenuItem1";
			this.descendingToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
			this.descendingToolStripMenuItem1.Text = "&Descending";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(133, 6);
			// 
			// moreToolStripMenuItem1
			// 
			this.moreToolStripMenuItem1.Name = "moreToolStripMenuItem1";
			this.moreToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
			this.moreToolStripMenuItem1.Text = "&More...";
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.refreshToolStripMenuItem.Text = "R&efresh";
			// 
			// mnuColumnContext
			// 
			this.mnuColumnContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuColumnContextSizeToFit,
            this.mnuColumnContextSizeAllToFit,
            this.mnuColumnContextSep1,
            this.mnuColumnContextSep2,
            this.mnuColumnContextMore});
			this.mnuColumnContext.Name = "cbContextMenu1";
			this.mnuColumnContext.Size = new System.Drawing.Size(193, 82);
			// 
			// mnuColumnContextSizeToFit
			// 
			this.mnuColumnContextSizeToFit.Name = "mnuColumnContextSizeToFit";
			this.mnuColumnContextSizeToFit.Size = new System.Drawing.Size(192, 22);
			this.mnuColumnContextSizeToFit.Text = "&Size Column to Fit";
			// 
			// mnuColumnContextSizeAllToFit
			// 
			this.mnuColumnContextSizeAllToFit.Name = "mnuColumnContextSizeAllToFit";
			this.mnuColumnContextSizeAllToFit.Size = new System.Drawing.Size(192, 22);
			this.mnuColumnContextSizeAllToFit.Text = "Size &All Columns to Fit";
			// 
			// mnuColumnContextSep1
			// 
			this.mnuColumnContextSep1.Name = "mnuColumnContextSep1";
			this.mnuColumnContextSep1.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuColumnContextSep2
			// 
			this.mnuColumnContextSep2.Name = "mnuColumnContextSep2";
			this.mnuColumnContextSep2.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuColumnContextMore
			// 
			this.mnuColumnContextMore.Name = "mnuColumnContextMore";
			this.mnuColumnContextMore.Size = new System.Drawing.Size(192, 22);
			this.mnuColumnContextMore.Text = "&More...";
			// 
			// vsc
			// 
			this.vsc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vsc.Location = new System.Drawing.Point(437, 0);
			this.vsc.Name = "vsc";
			this.vsc.Size = new System.Drawing.Size(17, 193);
			this.vsc.TabIndex = 2;
			this.vsc.Visible = false;
			this.vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsc_Scroll);
			// 
			// scrl
			// 
			this.scrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.scrl.BackColor = System.Drawing.SystemColors.Control;
			this.scrl.Location = new System.Drawing.Point(437, 195);
			this.scrl.Name = "scrl";
			this.scrl.Size = new System.Drawing.Size(17, 17);
			this.scrl.TabIndex = 3;
			this.scrl.Visible = false;
			// 
			// hsc
			// 
			this.hsc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hsc.Location = new System.Drawing.Point(0, 195);
			this.hsc.Name = "hsc";
			this.hsc.Size = new System.Drawing.Size(436, 17);
			this.hsc.TabIndex = 4;
			this.hsc.Visible = false;
			// 
			// txtRename
			// 
			this.txtRename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRename.Location = new System.Drawing.Point(276, 101);
			this.txtRename.Name = "txtRename";
			this.txtRename.Size = new System.Drawing.Size(100, 20);
			this.txtRename.TabIndex = 5;
			this.txtRename.Visible = false;
			this.txtRename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRename_KeyDown);
			// 
			// ListViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.txtRename);
			this.Controls.Add(this.hsc);
			this.Controls.Add(this.scrl);
			this.Controls.Add(this.vsc);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Name = "ListViewControl";
			this.Size = new System.Drawing.Size(454, 212);
			this.mnuContext.ResumeLayout(false);
			this.mnuColumnContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private CommandBars.CBContextMenu mnuContext;
		private System.Windows.Forms.ToolStripMenuItem mnuContextView;
		private System.Windows.Forms.ToolStripMenuItem mnuContextViewThumbnails;
		private System.Windows.Forms.ToolStripMenuItem mnuContextViewTiles;
		private System.Windows.Forms.ToolStripMenuItem mnuContextViewIcons;
		private System.Windows.Forms.ToolStripMenuItem mnuContextViewList;
        private System.Windows.Forms.ToolStripMenuItem mnuContextViewDetails;
		private System.Windows.Forms.ToolStripMenuItem arrangeIconsByToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private CommandBars.CBContextMenu mnuColumnContext;
        private System.Windows.Forms.ToolStripMenuItem mnuColumnContextSizeToFit;
        private System.Windows.Forms.ToolStripMenuItem mnuColumnContextSizeAllToFit;
        private System.Windows.Forms.ToolStripSeparator mnuColumnContextSep1;
        private System.Windows.Forms.ToolStripSeparator mnuColumnContextSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuColumnContextMore;
        private System.Windows.Forms.ToolStripMenuItem ascendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ascendingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem descendingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem1;
        private System.Windows.Forms.VScrollBar vsc;
        private System.Windows.Forms.Panel scrl;
		private System.Windows.Forms.HScrollBar hsc;
        private System.Windows.Forms.ToolTip tip;
		private System.Windows.Forms.TextBox txtRename;
    }
}
