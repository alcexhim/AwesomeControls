namespace AwesomeControls.GuidTextBox
{
	partial class GuidTextBoxControl
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
			this.cmdGuidEmpty = new System.Windows.Forms.Button();
			this.cmdGuidNew = new System.Windows.Forms.Button();
			this.txtGuid = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdGuidEmpty
			// 
			this.cmdGuidEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGuidEmpty.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGuidEmpty.Location = new System.Drawing.Point(304, 3);
			this.cmdGuidEmpty.Name = "cmdGuidEmpty";
			this.cmdGuidEmpty.Size = new System.Drawing.Size(55, 23);
			this.cmdGuidEmpty.TabIndex = 2;
			this.cmdGuidEmpty.Text = "Clear";
			this.cmdGuidEmpty.UseVisualStyleBackColor = true;
			this.cmdGuidEmpty.Click += new System.EventHandler(this.cmdGuidEmpty_Click);
			// 
			// cmdGuidNew
			// 
			this.cmdGuidNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGuidNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGuidNew.Location = new System.Drawing.Point(243, 3);
			this.cmdGuidNew.Name = "cmdGuidNew";
			this.cmdGuidNew.Size = new System.Drawing.Size(55, 23);
			this.cmdGuidNew.TabIndex = 1;
			this.cmdGuidNew.Text = "New";
			this.cmdGuidNew.UseVisualStyleBackColor = true;
			this.cmdGuidNew.Click += new System.EventHandler(this.cmdGuidNew_Click);
			// 
			// txtGuid
			// 
			this.txtGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGuid.Location = new System.Drawing.Point(3, 5);
			this.txtGuid.Name = "txtGuid";
			this.txtGuid.Size = new System.Drawing.Size(234, 20);
			this.txtGuid.TabIndex = 0;
			this.txtGuid.Text = "{42B583F5-EAAF-4336-ADC0-83D0F1238A98}";
			this.txtGuid.Validating += new System.ComponentModel.CancelEventHandler(this.txtGuid_Validating);
			// 
			// GuidTextBoxControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdGuidEmpty);
			this.Controls.Add(this.cmdGuidNew);
			this.Controls.Add(this.txtGuid);
			this.MinimumSize = new System.Drawing.Size(362, 29);
			this.Name = "GuidTextBoxControl";
			this.Size = new System.Drawing.Size(362, 29);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdGuidEmpty;
		private System.Windows.Forms.Button cmdGuidNew;
		private System.Windows.Forms.TextBox txtGuid;
	}
}
