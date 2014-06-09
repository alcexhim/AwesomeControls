namespace AwesomeControls.TestProject
{
	partial class BinaryDropDownTest
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
			this.binaryTextBoxControl1 = new AwesomeControls.BinaryTextBox.BinaryTextBoxControl();
			this.SuspendLayout();
			// 
			// binaryTextBoxControl1
			// 
			this.binaryTextBoxControl1.BackColor = System.Drawing.SystemColors.Window;
			this.binaryTextBoxControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.binaryTextBoxControl1.Location = new System.Drawing.Point(74, 44);
			this.binaryTextBoxControl1.Name = "binaryTextBoxControl1";
			this.binaryTextBoxControl1.Size = new System.Drawing.Size(221, 26);
			this.binaryTextBoxControl1.TabIndex = 0;
			// 
			// BinaryDropDownTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 209);
			this.Controls.Add(this.binaryTextBoxControl1);
			this.Name = "BinaryDropDownTest";
			this.Text = "BinaryDropDownTest";
			this.ResumeLayout(false);

		}

		#endregion

		private BinaryTextBox.BinaryTextBoxControl binaryTextBoxControl1;


	}
}