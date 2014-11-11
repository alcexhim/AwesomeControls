namespace AwesomeControls.DockingWindows.Dialogs
{
	partial class WindowListDialog
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
			this.lv = new System.Windows.Forms.ListView();
			this.cmdActivate = new System.Windows.Forms.Button();
			this.cmdSaveSelected = new System.Windows.Forms.Button();
			this.cmdCloseSelected = new System.Windows.Forms.Button();
			this.cmdDone = new System.Windows.Forms.Button();
			this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdOpenContainingFolder = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lv
			// 
			this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chPath});
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(12, 12);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(369, 207);
			this.lv.TabIndex = 0;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// cmdActivate
			// 
			this.cmdActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdActivate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdActivate.Location = new System.Drawing.Point(387, 13);
			this.cmdActivate.Name = "cmdActivate";
			this.cmdActivate.Size = new System.Drawing.Size(140, 23);
			this.cmdActivate.TabIndex = 1;
			this.cmdActivate.Text = "&Activate";
			this.cmdActivate.UseVisualStyleBackColor = true;
			// 
			// cmdSaveSelected
			// 
			this.cmdSaveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSaveSelected.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSaveSelected.Location = new System.Drawing.Point(387, 89);
			this.cmdSaveSelected.Name = "cmdSaveSelected";
			this.cmdSaveSelected.Size = new System.Drawing.Size(140, 23);
			this.cmdSaveSelected.TabIndex = 3;
			this.cmdSaveSelected.Text = "&Save Selected";
			this.cmdSaveSelected.UseVisualStyleBackColor = true;
			// 
			// cmdCloseSelected
			// 
			this.cmdCloseSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCloseSelected.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCloseSelected.Location = new System.Drawing.Point(387, 118);
			this.cmdCloseSelected.Name = "cmdCloseSelected";
			this.cmdCloseSelected.Size = new System.Drawing.Size(140, 23);
			this.cmdCloseSelected.TabIndex = 4;
			this.cmdCloseSelected.Text = "&Close Selected";
			this.cmdCloseSelected.UseVisualStyleBackColor = true;
			// 
			// cmdDone
			// 
			this.cmdDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDone.Location = new System.Drawing.Point(387, 196);
			this.cmdDone.Name = "cmdDone";
			this.cmdDone.Size = new System.Drawing.Size(140, 23);
			this.cmdDone.TabIndex = 5;
			this.cmdDone.Text = "&Done";
			this.cmdDone.UseVisualStyleBackColor = true;
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			// 
			// chFileName
			// 
			this.chFileName.Text = "File name";
			this.chFileName.Width = 185;
			// 
			// chPath
			// 
			this.chPath.Text = "Path";
			this.chPath.Width = 123;
			// 
			// cmdOpenContainingFolder
			// 
			this.cmdOpenContainingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOpenContainingFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOpenContainingFolder.Location = new System.Drawing.Point(387, 42);
			this.cmdOpenContainingFolder.Name = "cmdOpenContainingFolder";
			this.cmdOpenContainingFolder.Size = new System.Drawing.Size(140, 23);
			this.cmdOpenContainingFolder.TabIndex = 2;
			this.cmdOpenContainingFolder.Text = "&Open Containing Folder";
			this.cmdOpenContainingFolder.UseVisualStyleBackColor = true;
			// 
			// WindowListDialog
			// 
			this.AcceptButton = this.cmdActivate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdDone;
			this.ClientSize = new System.Drawing.Size(539, 231);
			this.Controls.Add(this.cmdDone);
			this.Controls.Add(this.cmdCloseSelected);
			this.Controls.Add(this.cmdSaveSelected);
			this.Controls.Add(this.cmdOpenContainingFolder);
			this.Controls.Add(this.cmdActivate);
			this.Controls.Add(this.lv);
			this.Name = "WindowListDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Windows";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ColumnHeader chFileName;
		private System.Windows.Forms.ColumnHeader chPath;
		private System.Windows.Forms.Button cmdActivate;
		private System.Windows.Forms.Button cmdSaveSelected;
		private System.Windows.Forms.Button cmdCloseSelected;
		private System.Windows.Forms.Button cmdDone;
		private System.Windows.Forms.Button cmdOpenContainingFolder;
	}
}