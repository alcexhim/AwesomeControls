namespace AwesomeControls.TextBox
{
	partial class TextBoxAutoSuggestWindow
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
			this.lst = new System.Windows.Forms.ListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabCommon = new System.Windows.Forms.TabPage();
			this.tabAll = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lst
			// 
			this.lst.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lst.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lst.FormattingEnabled = true;
			this.lst.ItemHeight = 19;
			this.lst.Location = new System.Drawing.Point(0, 0);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(329, 133);
			this.lst.TabIndex = 0;
			this.lst.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lst_DrawItem);
			this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
			this.lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
			this.lst.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDown);
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabCommon);
			this.tabControl1.Controls.Add(this.tabAll);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControl1.Location = new System.Drawing.Point(0, 133);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(329, 24);
			this.tabControl1.TabIndex = 1;
			// 
			// tabCommon
			// 
			this.tabCommon.Location = new System.Drawing.Point(4, 4);
			this.tabCommon.Name = "tabCommon";
			this.tabCommon.Padding = new System.Windows.Forms.Padding(3);
			this.tabCommon.Size = new System.Drawing.Size(321, 0);
			this.tabCommon.TabIndex = 0;
			this.tabCommon.Text = "Common";
			this.tabCommon.UseVisualStyleBackColor = true;
			// 
			// tabAll
			// 
			this.tabAll.Location = new System.Drawing.Point(4, 4);
			this.tabAll.Name = "tabAll";
			this.tabAll.Padding = new System.Windows.Forms.Padding(3);
			this.tabAll.Size = new System.Drawing.Size(321, 0);
			this.tabAll.TabIndex = 1;
			this.tabAll.Text = "All";
			this.tabAll.UseVisualStyleBackColor = true;
			// 
			// TextBoxAutoSuggestWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(329, 157);
			this.ControlBox = false;
			this.Controls.Add(this.lst);
			this.Controls.Add(this.tabControl1);
			this.Name = "TextBoxAutoSuggestWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabCommon;
		private System.Windows.Forms.TabPage tabAll;
        internal System.Windows.Forms.ListBox lst;
	}
}