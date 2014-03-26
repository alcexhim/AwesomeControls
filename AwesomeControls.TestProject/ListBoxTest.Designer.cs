namespace AwesomeControls.TestProject
{
	partial class ListBoxTest
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
            this.components = new System.ComponentModel.Container();
            this.customizeThisFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new AwesomeControls.ListView.ListViewControl();
            this.cbContextMenu1 = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // customizeThisFolderToolStripMenuItem
            // 
            this.customizeThisFolderToolStripMenuItem.Name = "customizeThisFolderToolStripMenuItem";
            this.customizeThisFolderToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.customizeThisFolderToolStripMenuItem.Text = "Customize this Folder";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.propertiesToolStripMenuItem.Text = "P&roperties...";
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowColumnResize = true;
            this.listView1.AllowSorting = true;
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.ColumnBehavior = AwesomeControls.ListView.ListViewColumnBehavior.DetailOnly;
            this.listView1.ColumnHeaderHeight = 24;
            this.listView1.ContextMenuStrip = this.cbContextMenu1;
            this.listView1.DefaultItemHeight = 24;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.EnableFilter = false;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = null;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MergeContextMenuStrip = true;
            this.listView1.Mode = AwesomeControls.ListView.ListViewMode.Tiles;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.listView1.ShowGridLines = true;
            this.listView1.Size = new System.Drawing.Size(540, 299);
            this.listView1.SmallImageList = null;
            this.listView1.SortColumn = null;
            this.listView1.TabIndex = 0;
            // 
            // cbContextMenu1
            // 
            this.cbContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.propertiesToolStripMenuItem1});
            this.cbContextMenu1.Name = "cbContextMenu1";
            this.cbContextMenu1.Size = new System.Drawing.Size(137, 48);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newToolStripMenuItem.Text = "Ne&w";
            // 
            // propertiesToolStripMenuItem1
            // 
            this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
            this.propertiesToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.propertiesToolStripMenuItem1.Text = "P&roperties...";
            // 
            // ListBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 299);
            this.Controls.Add(this.listView1);
            this.Name = "ListBoxTest";
            this.Text = "ListBoxTest";
            this.ResumeLayout(false);

		}

		#endregion

		private ListView.ListViewControl listView1;
        private CommandBars.CBContextMenu cbContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem customizeThisFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
	}
}