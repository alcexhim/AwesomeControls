namespace AwesomeControls.TestProject
{
	partial class PropertyGridTest
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
			this.pg = new AwesomeControls.PropertyGrid.PropertyGridControl();
			this.SuspendLayout();
			// 
			// pg
			// 
			this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pg.Font = new System.Drawing.Font("Tahoma", 8F);
			this.pg.ItemHeight = 16;
			this.pg.Location = new System.Drawing.Point(0, 0);
			this.pg.Name = "pg";
			this.pg.PropertyListBackColor = System.Drawing.SystemColors.Window;
			this.pg.SelectedGroupIndex = -1;
			this.pg.ShowCommands = true;
			this.pg.ShowDescription = true;
			this.pg.Size = new System.Drawing.Size(345, 319);
			this.pg.TabIndex = 0;
			this.pg.PropertyChanged += new AwesomeControls.PropertyGrid.PropertyChangedEventHandler(this.pg_PropertyChanged);
			// 
			// PropertyGridTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 319);
			this.Controls.Add(this.pg);
			this.Name = "PropertyGridTest";
			this.Text = "Properties";
			this.ResumeLayout(false);

		}

		#endregion

		private PropertyGrid.PropertyGridControl pg;
	}
}