/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 6/8/2013
 * Time: 12:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AwesomeControls.TestProject
{
	partial class ScrollBarTest
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.scrollBarControl1 = new AwesomeControls.ScrollBar.ScrollBarControl();
			this.scrollBarControl2 = new AwesomeControls.ScrollBar.ScrollBarControl();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.scrollBarControl3 = new AwesomeControls.ScrollBar.ScrollBarControl();
			this.SuspendLayout();
			// 
			// scrollBarControl1
			// 
			this.scrollBarControl1.Location = new System.Drawing.Point(96, 12);
			this.scrollBarControl1.Maximum = 100D;
			this.scrollBarControl1.Minimum = 0D;
			this.scrollBarControl1.Name = "scrollBarControl1";
			this.scrollBarControl1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.scrollBarControl1.Size = new System.Drawing.Size(232, 23);
			this.scrollBarControl1.TabIndex = 0;
			this.scrollBarControl1.Text = "scrollBarControl1";
			this.scrollBarControl1.Value = 0D;
			// 
			// scrollBarControl2
			// 
			this.scrollBarControl2.Location = new System.Drawing.Point(96, 40);
			this.scrollBarControl2.Maximum = 100D;
			this.scrollBarControl2.Minimum = 0D;
			this.scrollBarControl2.Name = "scrollBarControl2";
			this.scrollBarControl2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.scrollBarControl2.Size = new System.Drawing.Size(232, 23);
			this.scrollBarControl2.TabIndex = 0;
			this.scrollBarControl2.Text = "scrollBarControl1";
			this.scrollBarControl2.Value = 0D;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "DIVIDE 00";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "DIVIDE 01";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 20);
			this.label3.TabIndex = 1;
			this.label3.Text = "DIVIDE 02";
			// 
			// scrollBarControl3
			// 
			this.scrollBarControl3.Location = new System.Drawing.Point(96, 68);
			this.scrollBarControl3.Maximum = 100D;
			this.scrollBarControl3.Minimum = 0D;
			this.scrollBarControl3.Name = "scrollBarControl3";
			this.scrollBarControl3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.scrollBarControl3.Size = new System.Drawing.Size(232, 23);
			this.scrollBarControl3.TabIndex = 0;
			this.scrollBarControl3.Text = "scrollBarControl1";
			this.scrollBarControl3.Value = 0D;
			// 
			// ScrollBarTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 138);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.scrollBarControl3);
			this.Controls.Add(this.scrollBarControl2);
			this.Controls.Add(this.scrollBarControl1);
			this.Font = new System.Drawing.Font("FangSong", 10F);
			this.Name = "ScrollBarTest";
			this.Text = "ScrollBarTest";
			this.ResumeLayout(false);
		}
		private AwesomeControls.ScrollBar.ScrollBarControl scrollBarControl3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private AwesomeControls.ScrollBar.ScrollBarControl scrollBarControl2;
		private AwesomeControls.ScrollBar.ScrollBarControl scrollBarControl1;
	}
}
