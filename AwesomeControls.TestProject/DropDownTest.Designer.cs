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
			this.dropDownCustomControl1 = new AwesomeControls.TestProject.DropDownCustomControl();
			this.dropDownControl1 = new AwesomeControls.DropDown.DropDownControl();
			this.SuspendLayout();
			// 
			// dropDownCustomControl1
			// 
			this.dropDownCustomControl1.Location = new System.Drawing.Point(150, 101);
			this.dropDownCustomControl1.Name = "dropDownCustomControl1";
			this.dropDownCustomControl1.Size = new System.Drawing.Size(355, 134);
			this.dropDownCustomControl1.TabIndex = 1;
			this.dropDownCustomControl1.Visible = false;
			// 
			// dropDownControl1
			// 
			this.dropDownControl1.CustomDropDownControl = this.dropDownCustomControl1;
			this.dropDownControl1.DropDownVisible = false;
			this.dropDownControl1.Location = new System.Drawing.Point(40, 32);
			this.dropDownControl1.Name = "dropDownControl1";
			this.dropDownControl1.Size = new System.Drawing.Size(236, 23);
			this.dropDownControl1.TabIndex = 0;
			this.dropDownControl1.PaintContent += new System.Windows.Forms.PaintEventHandler(this.dropDownControl1_PaintContent);
			// 
			// DropDownTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 194);
			this.Controls.Add(this.dropDownCustomControl1);
			this.Controls.Add(this.dropDownControl1);
			this.Name = "DropDownTest";
			this.Text = "DropDownTest";
			this.ResumeLayout(false);

		}

		#endregion

		private DropDown.DropDownControl dropDownControl1;
		private DropDownCustomControl dropDownCustomControl1;
	}
}