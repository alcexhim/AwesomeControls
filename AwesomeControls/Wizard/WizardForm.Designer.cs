namespace AwesomeControls.Wizard
{
	partial class WizardForm
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
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdBack = new System.Windows.Forms.Button();
			this.cmdNext = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.lblBrandingText = new System.Windows.Forms.Label();
			this.ContentArea = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCancel.Location = new System.Drawing.Point(12, 13);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdBack
			// 
			this.cmdBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBack.Location = new System.Drawing.Point(320, 13);
			this.cmdBack.Name = "cmdBack";
			this.cmdBack.Size = new System.Drawing.Size(75, 23);
			this.cmdBack.TabIndex = 2;
			this.cmdBack.Text = "< &Back";
			this.cmdBack.UseVisualStyleBackColor = true;
			this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
			// 
			// cmdNext
			// 
			this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNext.Location = new System.Drawing.Point(401, 13);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(75, 23);
			this.cmdNext.TabIndex = 1;
			this.cmdNext.Text = "&Next >";
			this.cmdNext.UseVisualStyleBackColor = true;
			this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.lblDescription);
			this.panel1.Controls.Add(this.lblTitle);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(488, 63);
			this.panel1.TabIndex = 4;
			// 
			// lblDescription
			// 
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDescription.Location = new System.Drawing.Point(31, 35);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(395, 13);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.Text = "Wizard panel description goes here";
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(12, 15);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(414, 13);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Wizard panel title goes here";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.pictureBox1.Location = new System.Drawing.Point(432, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(0, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(488, 2);
			this.label1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.cmdCancel);
			this.panel2.Controls.Add(this.cmdNext);
			this.panel2.Controls.Add(this.cmdBack);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 295);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(488, 48);
			this.panel2.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(488, 2);
			this.label4.TabIndex = 1;
			// 
			// lblBrandingText
			// 
			this.lblBrandingText.AutoSize = true;
			this.lblBrandingText.Enabled = false;
			this.lblBrandingText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBrandingText.Location = new System.Drawing.Point(14, 289);
			this.lblBrandingText.Name = "lblBrandingText";
			this.lblBrandingText.Size = new System.Drawing.Size(146, 13);
			this.lblBrandingText.TabIndex = 6;
			this.lblBrandingText.Text = "Awesome Wizard Component";
			// 
			// ContentArea
			// 
			this.ContentArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ContentArea.Location = new System.Drawing.Point(12, 69);
			this.ContentArea.Name = "ContentArea";
			this.ContentArea.Size = new System.Drawing.Size(464, 207);
			this.ContentArea.TabIndex = 7;
			// 
			// WizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 343);
			this.Controls.Add(this.ContentArea);
			this.Controls.Add(this.lblBrandingText);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "WizardForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WizardForm";
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdBack;
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblBrandingText;
		private System.Windows.Forms.Panel ContentArea;
	}
}