namespace AwesomeControls.CollectionListView
{
	partial class CollectionListViewControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionListViewControl));
			this.lv = new AwesomeControls.ListView.ListViewControl();
			this.tb = new AwesomeControls.CommandBars.CBToolBar();
			this.tsbAdd = new System.Windows.Forms.ToolStripButton();
			this.tsbModify = new System.Windows.Forms.ToolStripButton();
			this.tsbRemove = new System.Windows.Forms.ToolStripButton();
			this.tsbClear = new System.Windows.Forms.ToolStripButton();
			this.tsbSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
			this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
			this.tb.SuspendLayout();
			this.SuspendLayout();
			// 
			// lv
			// 
			this.lv.AllowSorting = true;
			this.lv.BackColor = System.Drawing.SystemColors.Window;
			this.lv.DefaultItemHeight = 24;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lv.LargeImageList = null;
			this.lv.Location = new System.Drawing.Point(0, 25);
			this.lv.Mode = AwesomeControls.ListView.ListViewMode.Details;
			this.lv.Name = "lv";
			this.lv.ShadeColor = System.Drawing.Color.WhiteSmoke;
			this.lv.Size = new System.Drawing.Size(443, 236);
			this.lv.SmallImageList = null;
			this.lv.SortColumn = null;
			this.lv.TabIndex = 0;
			this.lv.ItemActivate += new System.EventHandler(this.lv_ItemActivate);
			this.lv.SelectionChanged += new System.EventHandler(this.lv_SelectionChanged);
			// 
			// tb
			// 
			this.tb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbModify,
            this.tsbRemove,
            this.tsbClear,
            this.tsbSep1,
            this.tsbMoveUp,
            this.tsbMoveDown});
			this.tb.Location = new System.Drawing.Point(0, 0);
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(443, 25);
			this.tb.TabIndex = 1;
			this.tb.Text = "cbToolBar1";
			// 
			// tsbAdd
			// 
			this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
			this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAdd.Name = "tsbAdd";
			this.tsbAdd.Size = new System.Drawing.Size(49, 22);
			this.tsbAdd.Text = "Add";
			this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
			// 
			// tsbModify
			// 
			this.tsbModify.Image = ((System.Drawing.Image)(resources.GetObject("tsbModify.Image")));
			this.tsbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbModify.Name = "tsbModify";
			this.tsbModify.Size = new System.Drawing.Size(65, 22);
			this.tsbModify.Text = "Modify";
			this.tsbModify.Click += new System.EventHandler(this.tsbModify_Click);
			// 
			// tsbRemove
			// 
			this.tsbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemove.Image")));
			this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRemove.Name = "tsbRemove";
			this.tsbRemove.Size = new System.Drawing.Size(70, 22);
			this.tsbRemove.Text = "Remove";
			this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
			// 
			// tsbClear
			// 
			this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
			this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbClear.Name = "tsbClear";
			this.tsbClear.Size = new System.Drawing.Size(54, 22);
			this.tsbClear.Text = "Clear";
			this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
			// 
			// tsbSep1
			// 
			this.tsbSep1.Name = "tsbSep1";
			this.tsbSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbMoveUp
			// 
			this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
			this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbMoveUp.Name = "tsbMoveUp";
			this.tsbMoveUp.Size = new System.Drawing.Size(75, 22);
			this.tsbMoveUp.Text = "Move Up";
			this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
			// 
			// tsbMoveDown
			// 
			this.tsbMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveDown.Image")));
			this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbMoveDown.Name = "tsbMoveDown";
			this.tsbMoveDown.Size = new System.Drawing.Size(91, 22);
			this.tsbMoveDown.Text = "Move Down";
			this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
			// 
			// CollectionListViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lv);
			this.Controls.Add(this.tb);
			this.Name = "CollectionListViewControl";
			this.Size = new System.Drawing.Size(443, 261);
			this.tb.ResumeLayout(false);
			this.tb.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ListView.ListViewControl lv;
		private CommandBars.CBToolBar tb;
		private System.Windows.Forms.ToolStripButton tsbAdd;
		private System.Windows.Forms.ToolStripButton tsbModify;
		private System.Windows.Forms.ToolStripButton tsbRemove;
		private System.Windows.Forms.ToolStripButton tsbClear;
		private System.Windows.Forms.ToolStripSeparator tsbSep1;
		private System.Windows.Forms.ToolStripButton tsbMoveUp;
		private System.Windows.Forms.ToolStripButton tsbMoveDown;
	}
}
