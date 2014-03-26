using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AwesomeControls.TextBox
{
	public partial class TextBoxControl
	{
		private System.Windows.Forms.Timer tmrCaret;
		private IContainer components;
	
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tmrCaret = new System.Windows.Forms.Timer(this.components);
			this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextOutlining = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextOutliningToggleExpansion = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextOutliningToggleAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextOutliningStop = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextOutliningStopCurrent = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextOutliningCollapse = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrCaret
			// 
			this.tmrCaret.Tick += new System.EventHandler(this.tmrCaret_Tick);
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextUndo,
            this.mnuContextRedo,
            this.mnuContextSep1,
            this.mnuContextCut,
            this.mnuContextCopy,
            this.mnuContextPaste,
            this.mnuContextDelete,
            this.mnuContextSep2,
            this.mnuContextSelectAll,
            this.mnuContextSep3,
            this.mnuContextOutlining});
			this.mnuContext.Name = "mnuContext";
			this.mnuContext.Size = new System.Drawing.Size(157, 198);
			this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
			// 
			// mnuContextUndo
			// 
			this.mnuContextUndo.Name = "mnuContextUndo";
			this.mnuContextUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			this.mnuContextUndo.Size = new System.Drawing.Size(156, 22);
			this.mnuContextUndo.Text = "&Undo";
			// 
			// mnuContextRedo
			// 
			this.mnuContextRedo.Name = "mnuContextRedo";
			this.mnuContextRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			this.mnuContextRedo.Size = new System.Drawing.Size(156, 22);
			this.mnuContextRedo.Text = "&Redo";
			// 
			// mnuContextSep1
			// 
			this.mnuContextSep1.Name = "mnuContextSep1";
			this.mnuContextSep1.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextCut
			// 
			this.mnuContextCut.Name = "mnuContextCut";
			this.mnuContextCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.mnuContextCut.Size = new System.Drawing.Size(156, 22);
			this.mnuContextCut.Text = "Cu&t";
			// 
			// mnuContextCopy
			// 
			this.mnuContextCopy.Name = "mnuContextCopy";
			this.mnuContextCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.mnuContextCopy.Size = new System.Drawing.Size(156, 22);
			this.mnuContextCopy.Text = "&Copy";
			// 
			// mnuContextPaste
			// 
			this.mnuContextPaste.Name = "mnuContextPaste";
			this.mnuContextPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.mnuContextPaste.Size = new System.Drawing.Size(156, 22);
			this.mnuContextPaste.Text = "&Paste";
			// 
			// mnuContextDelete
			// 
			this.mnuContextDelete.Name = "mnuContextDelete";
			this.mnuContextDelete.ShortcutKeyDisplayString = "Del";
			this.mnuContextDelete.Size = new System.Drawing.Size(156, 22);
			this.mnuContextDelete.Text = "&Delete";
			// 
			// mnuContextSep2
			// 
			this.mnuContextSep2.Name = "mnuContextSep2";
			this.mnuContextSep2.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextSelectAll
			// 
			this.mnuContextSelectAll.Name = "mnuContextSelectAll";
			this.mnuContextSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.mnuContextSelectAll.Size = new System.Drawing.Size(156, 22);
			this.mnuContextSelectAll.Text = "Select &All";
			// 
			// mnuContextSep3
			// 
			this.mnuContextSep3.Name = "mnuContextSep3";
			this.mnuContextSep3.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextOutlining
			// 
			this.mnuContextOutlining.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextOutliningToggleExpansion,
            this.mnuContextOutliningToggleAll,
            this.mnuContextOutliningStop,
            this.mnuContextOutliningStopCurrent,
            this.mnuContextOutliningCollapse});
			this.mnuContextOutlining.Name = "mnuContextOutlining";
			this.mnuContextOutlining.Size = new System.Drawing.Size(156, 22);
			this.mnuContextOutlining.Text = "Out&lining";
			// 
			// mnuContextOutliningToggleExpansion
			// 
			this.mnuContextOutliningToggleExpansion.Name = "mnuContextOutliningToggleExpansion";
			this.mnuContextOutliningToggleExpansion.Size = new System.Drawing.Size(203, 22);
			this.mnuContextOutliningToggleExpansion.Text = "&Toggle Outlining Expansion";
			// 
			// mnuContextOutliningToggleAll
			// 
			this.mnuContextOutliningToggleAll.Name = "mnuContextOutliningToggleAll";
			this.mnuContextOutliningToggleAll.Size = new System.Drawing.Size(203, 22);
			this.mnuContextOutliningToggleAll.Text = "Toggle A&ll Outlining";
			// 
			// mnuContextOutliningStop
			// 
			this.mnuContextOutliningStop.Name = "mnuContextOutliningStop";
			this.mnuContextOutliningStop.Size = new System.Drawing.Size(203, 22);
			this.mnuContextOutliningStop.Text = "Sto&p Outlining";
			// 
			// mnuContextOutliningStopCurrent
			// 
			this.mnuContextOutliningStopCurrent.Name = "mnuContextOutliningStopCurrent";
			this.mnuContextOutliningStopCurrent.Size = new System.Drawing.Size(203, 22);
			this.mnuContextOutliningStopCurrent.Text = "Stop H&iding Current";
			// 
			// mnuContextOutliningCollapse
			// 
			this.mnuContextOutliningCollapse.Name = "mnuContextOutliningCollapse";
			this.mnuContextOutliningCollapse.Size = new System.Drawing.Size(203, 22);
			this.mnuContextOutliningCollapse.Text = "C&ollapse to Definitions";
			// 
			// TextBox
			// 
			this.mnuContext.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private CommandBars.CBContextMenu mnuContext;
		private System.Windows.Forms.ToolStripMenuItem mnuContextUndo;
		private System.Windows.Forms.ToolStripMenuItem mnuContextRedo;
		private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextCut;
		private System.Windows.Forms.ToolStripMenuItem mnuContextCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPaste;
		private System.Windows.Forms.ToolStripMenuItem mnuContextDelete;
		private System.Windows.Forms.ToolStripSeparator mnuContextSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuContextSelectAll;
		private System.Windows.Forms.ToolStripSeparator mnuContextSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutlining;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutliningToggleExpansion;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutliningToggleAll;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutliningStop;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutliningStopCurrent;
		private System.Windows.Forms.ToolStripMenuItem mnuContextOutliningCollapse;
	}
}
