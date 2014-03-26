using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeControls.CommandBars;

namespace AwesomeControls.DockingWindows
{
	partial class DockingContainerControl
	{
		private CBContextMenu mnuContextTab;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabSave;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabClose;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabCloseAllButThis;
		private System.Windows.Forms.ToolStripSeparator mnuContextTabSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabCopyFullPath;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabOpenContainingFolder;
		private System.Windows.Forms.ToolStripSeparator mnuContextTabSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabFloat;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabDockAsTabbedDocument;
		private System.Windows.Forms.ToolStripSeparator mnuContextTabSep4;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabNewHorizontalTabGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabNewVerticalTabGroup;

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tip = new System.Windows.Forms.ToolTip(this.components);
			this.mnuContextTab = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextTabSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabCloseAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTabCopyFullPath = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTabFloat = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabFloatAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabDockAsTabbedDocument = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTabPinTab = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTabNewHorizontalTabGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabNewVerticalTabGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTabSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTabCustomize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuContextTab
			// 
			this.mnuContextTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTabSave,
            this.mnuContextTabClose,
            this.mnuContextTabCloseAllDocuments,
            this.mnuContextTabCloseAllButThis,
            this.mnuContextTabSep1,
            this.mnuContextTabCopyFullPath,
            this.mnuContextTabOpenContainingFolder,
            this.mnuContextTabSep2,
            this.mnuContextTabFloat,
            this.mnuContextTabFloatAll,
            this.mnuContextTabDockAsTabbedDocument,
            this.mnuContextTabSep3,
            this.mnuContextTabPinTab,
            this.mnuContextTabSep4,
            this.mnuContextTabNewHorizontalTabGroup,
            this.mnuContextTabNewVerticalTabGroup,
            this.mnuContextTabSep5,
            this.mnuContextTabCustomize});
			this.mnuContextTab.Name = "mnuContextTab";
			this.mnuContextTab.Size = new System.Drawing.Size(218, 342);
			// 
			// mnuContextTabSave
			// 
			this.mnuContextTabSave.Name = "mnuContextTabSave";
			this.mnuContextTabSave.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabSave.Text = "&Save";
			this.mnuContextTabSave.Click += new System.EventHandler(this.mnuContextTabSave_Click);
			// 
			// mnuContextTabClose
			// 
			this.mnuContextTabClose.Name = "mnuContextTabClose";
			this.mnuContextTabClose.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabClose.Text = "C&lose";
			this.mnuContextTabClose.Click += new System.EventHandler(this.mnuContextTabClose_Click);
			// 
			// mnuContextTabCloseAllDocuments
			// 
			this.mnuContextTabCloseAllDocuments.Name = "mnuContextTabCloseAllDocuments";
			this.mnuContextTabCloseAllDocuments.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabCloseAllDocuments.Text = "Close &All Documents";
			this.mnuContextTabCloseAllDocuments.Click += new System.EventHandler(this.mnuContextTabCloseAllDocuments_Click);
			// 
			// mnuContextTabCloseAllButThis
			// 
			this.mnuContextTabCloseAllButThis.Name = "mnuContextTabCloseAllButThis";
			this.mnuContextTabCloseAllButThis.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabCloseAllButThis.Text = "Close All &But This";
			this.mnuContextTabCloseAllButThis.Click += new System.EventHandler(this.mnuContextTabCloseAllButThis_Click);
			// 
			// mnuContextTabSep1
			// 
			this.mnuContextTabSep1.Name = "mnuContextTabSep1";
			this.mnuContextTabSep1.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuContextTabCopyFullPath
			// 
			this.mnuContextTabCopyFullPath.Name = "mnuContextTabCopyFullPath";
			this.mnuContextTabCopyFullPath.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabCopyFullPath.Text = "Copy &Full Path";
			this.mnuContextTabCopyFullPath.Click += new System.EventHandler(this.mnuContextTabCopyFullPath_Click);
			// 
			// mnuContextTabOpenContainingFolder
			// 
			this.mnuContextTabOpenContainingFolder.Name = "mnuContextTabOpenContainingFolder";
			this.mnuContextTabOpenContainingFolder.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabOpenContainingFolder.Text = "&Open Containing Folder";
			this.mnuContextTabOpenContainingFolder.Click += new System.EventHandler(this.mnuContextTabOpenContainingFolder_Click);
			// 
			// mnuContextTabSep2
			// 
			this.mnuContextTabSep2.Name = "mnuContextTabSep2";
			this.mnuContextTabSep2.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuContextTabFloat
			// 
			this.mnuContextTabFloat.Name = "mnuContextTabFloat";
			this.mnuContextTabFloat.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabFloat.Text = "&Float";
			this.mnuContextTabFloat.Click += new System.EventHandler(this.mnuContextTabFloat_Click);
			// 
			// mnuContextTabFloatAll
			// 
			this.mnuContextTabFloatAll.Name = "mnuContextTabFloatAll";
			this.mnuContextTabFloatAll.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabFloatAll.Text = "Floa&t All";
			this.mnuContextTabFloatAll.Click += new System.EventHandler(this.mnuContextTabFloatAll_Click);
			// 
			// mnuContextTabDockAsTabbedDocument
			// 
			this.mnuContextTabDockAsTabbedDocument.Name = "mnuContextTabDockAsTabbedDocument";
			this.mnuContextTabDockAsTabbedDocument.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabDockAsTabbedDocument.Text = "Dock as &Tabbed Document";
			this.mnuContextTabDockAsTabbedDocument.Click += new System.EventHandler(this.mnuContextTabDockAsTabbedDocument_Click);
			// 
			// mnuContextTabSep3
			// 
			this.mnuContextTabSep3.Name = "mnuContextTabSep3";
			this.mnuContextTabSep3.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuContextTabPinTab
			// 
			this.mnuContextTabPinTab.Name = "mnuContextTabPinTab";
			this.mnuContextTabPinTab.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabPinTab.Text = "&Pin Tab";
			this.mnuContextTabPinTab.Click += new System.EventHandler(this.mnuContextTabPinTab_Click);
			// 
			// mnuContextTabSep4
			// 
			this.mnuContextTabSep4.Name = "mnuContextTabSep4";
			this.mnuContextTabSep4.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuContextTabNewHorizontalTabGroup
			// 
			this.mnuContextTabNewHorizontalTabGroup.Name = "mnuContextTabNewHorizontalTabGroup";
			this.mnuContextTabNewHorizontalTabGroup.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabNewHorizontalTabGroup.Text = "New Hori&zontal Tab Group";
			this.mnuContextTabNewHorizontalTabGroup.Click += new System.EventHandler(this.mnuContextTabNewHorizontalTabGroup_Click);
			// 
			// mnuContextTabNewVerticalTabGroup
			// 
			this.mnuContextTabNewVerticalTabGroup.Name = "mnuContextTabNewVerticalTabGroup";
			this.mnuContextTabNewVerticalTabGroup.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabNewVerticalTabGroup.Text = "New &Vertical Tab Group";
			this.mnuContextTabNewVerticalTabGroup.Click += new System.EventHandler(this.mnuContextTabNewVerticalTabGroup_Click);
			// 
			// mnuContextTabSep5
			// 
			this.mnuContextTabSep5.Name = "mnuContextTabSep5";
			this.mnuContextTabSep5.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuContextTabCustomize
			// 
			this.mnuContextTabCustomize.Name = "mnuContextTabCustomize";
			this.mnuContextTabCustomize.Size = new System.Drawing.Size(217, 22);
			this.mnuContextTabCustomize.Text = "&Customize...";
			this.mnuContextTabCustomize.Click += new System.EventHandler(this.mnuContextTabCustomize_Click);
			// 
			// DockingContainerControl
			// 
			this.Name = "DockingContainerControl";
			this.mnuContextTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabFloatAll;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabCloseAllDocuments;
		private System.Windows.Forms.ToolStripSeparator mnuContextTabSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabPinTab;
		private System.Windows.Forms.ToolStripSeparator mnuContextTabSep5;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTabCustomize;
	}
}
