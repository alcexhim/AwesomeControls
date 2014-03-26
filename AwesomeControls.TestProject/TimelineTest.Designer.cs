namespace AwesomeControls.TestProject
{
	partial class TimelineTest
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
			this.tc = new AwesomeControls.Timeline.TimelineControl();
			this.SuspendLayout();
			// 
			// tc
			// 
			this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc.GroupContextMenuStrip = null;
			this.tc.Location = new System.Drawing.Point(0, 0);
			this.tc.Name = "tc";
			this.tc.Size = new System.Drawing.Size(540, 233);
			this.tc.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 233);
			this.Controls.Add(this.tc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MainWindow";
			this.Text = "Timeline";
			this.ResumeLayout(false);

		}

		#endregion

		private Timeline.TimelineControl tc;


	}
}