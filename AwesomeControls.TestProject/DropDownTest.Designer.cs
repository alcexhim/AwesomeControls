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
			this.SuspendLayout();
			// 
			// dropDownControl1
			// 
			this.dropDownControl1.Location = new System.Drawing.Point(40, 32);
			this.dropDownControl1.Name = "dropDownControl1";
			this.dropDownControl1.Size = new System.Drawing.Size(236, 23);
			this.dropDownControl1.TabIndex = 0;
			// 
			// DropDownTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 194);
			this.Controls.Add(this.dropDownControl1);
			this.Name = "DropDownTest";
			this.Text = "DropDownTest";
			this.ResumeLayout(false);

		}

		#endregion

		private DropDown.DropDownControl dropDownControl1;
	}
}