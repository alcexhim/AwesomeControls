namespace AwesomeControls.TestProject
{
	partial class DropDownTest
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
			this.dropDownControl1 = new AwesomeControls.DropDown.DropDownControl();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// dropDownControl1
			// 
			this.dropDownControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dropDownControl1.CustomDropDownControl = null;
			this.dropDownControl1.DropDownVisible = false;
			this.dropDownControl1.Location = new System.Drawing.Point(98, 12);
			this.dropDownControl1.Name = "dropDownControl1";
			this.dropDownControl1.Size = new System.Drawing.Size(287, 23);
			this.dropDownControl1.TabIndex = 0;
			this.dropDownControl1.PaintContent += new System.Windows.Forms.PaintEventHandler(this.dropDownControl1_PaintContent);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "&Primary charact";
			// 
			// DropDownTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 194);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dropDownControl1);
			this.Name = "DropDownTest";
			this.Text = "DropDownTest";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DropDown.DropDownControl dropDownControl1;
		private System.Windows.Forms.Label label1;
	}
}