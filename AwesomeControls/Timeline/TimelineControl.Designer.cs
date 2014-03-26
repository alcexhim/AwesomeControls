namespace AwesomeControls.Timeline
{
	partial class TimelineControl
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
			this.vsc = new System.Windows.Forms.VScrollBar();
			this.mnuContextEntry = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextEntryCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntryCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntryPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntryDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntrySep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextEntrySelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntryInvertSelection = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntrySep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextEntryProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextEntry.SuspendLayout();
			this.SuspendLayout();
			// 
			// vsc
			// 
			this.vsc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vsc.Location = new System.Drawing.Point(412, 2);
			this.vsc.Name = "vsc";
			this.vsc.Size = new System.Drawing.Size(17, 236);
			this.vsc.TabIndex = 0;
			// 
			// mnuContextEntry
			// 
			this.mnuContextEntry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextEntryCut,
            this.mnuContextEntryCopy,
            this.mnuContextEntryPaste,
            this.mnuContextEntryDelete,
            this.mnuContextEntrySep1,
            this.mnuContextEntrySelectAll,
            this.mnuContextEntryInvertSelection,
            this.mnuContextEntrySep2,
            this.mnuContextEntryProperties});
			this.mnuContextEntry.Name = "mnuContextEntry";
			this.mnuContextEntry.Size = new System.Drawing.Size(220, 192);
			this.mnuContextEntry.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextEntry_Opening);
			// 
			// mnuContextEntryCut
			// 
			this.mnuContextEntryCut.Name = "mnuContextEntryCut";
			this.mnuContextEntryCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.mnuContextEntryCut.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryCut.Text = "Cu&t";
			// 
			// mnuContextEntryCopy
			// 
			this.mnuContextEntryCopy.Name = "mnuContextEntryCopy";
			this.mnuContextEntryCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.mnuContextEntryCopy.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryCopy.Text = "&Copy";
			// 
			// mnuContextEntryPaste
			// 
			this.mnuContextEntryPaste.Name = "mnuContextEntryPaste";
			this.mnuContextEntryPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.mnuContextEntryPaste.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryPaste.Text = "&Paste";
			// 
			// mnuContextEntryDelete
			// 
			this.mnuContextEntryDelete.Name = "mnuContextEntryDelete";
			this.mnuContextEntryDelete.ShortcutKeyDisplayString = "Del";
			this.mnuContextEntryDelete.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryDelete.Text = "&Delete";
			this.mnuContextEntryDelete.Click += new System.EventHandler(this.mnuContextEntryDelete_Click);
			// 
			// mnuContextEntrySep1
			// 
			this.mnuContextEntrySep1.Name = "mnuContextEntrySep1";
			this.mnuContextEntrySep1.Size = new System.Drawing.Size(216, 6);
			// 
			// mnuContextEntrySelectAll
			// 
			this.mnuContextEntrySelectAll.Name = "mnuContextEntrySelectAll";
			this.mnuContextEntrySelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.mnuContextEntrySelectAll.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntrySelectAll.Text = "Select &All";
			// 
			// mnuContextEntryInvertSelection
			// 
			this.mnuContextEntryInvertSelection.Name = "mnuContextEntryInvertSelection";
			this.mnuContextEntryInvertSelection.ShortcutKeyDisplayString = "Ctrl+Shift+A";
			this.mnuContextEntryInvertSelection.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryInvertSelection.Text = "&Invert Selection";
			// 
			// mnuContextEntrySep2
			// 
			this.mnuContextEntrySep2.Name = "mnuContextEntrySep2";
			this.mnuContextEntrySep2.Size = new System.Drawing.Size(216, 6);
			// 
			// mnuContextEntryProperties
			// 
			this.mnuContextEntryProperties.Name = "mnuContextEntryProperties";
			this.mnuContextEntryProperties.ShortcutKeyDisplayString = "Alt+Enter";
			this.mnuContextEntryProperties.Size = new System.Drawing.Size(219, 22);
			this.mnuContextEntryProperties.Text = "P&roperties...";
			// 
			// TimelineControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vsc);
			this.Name = "TimelineControl";
			this.Size = new System.Drawing.Size(430, 240);
			this.mnuContextEntry.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar vsc;
		private CommandBars.CBContextMenu mnuContextEntry;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryCut;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryPaste;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryDelete;
		private System.Windows.Forms.ToolStripSeparator mnuContextEntrySep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntrySelectAll;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryInvertSelection;
		private System.Windows.Forms.ToolStripSeparator mnuContextEntrySep2;
		private System.Windows.Forms.ToolStripMenuItem mnuContextEntryProperties;
	}
}
