﻿namespace AwesomeControls.FileTextBox
{
	partial class FileTextBoxControl
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
			this.txt = new System.Windows.Forms.TextBox();
			this.cmd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt.Location = new System.Drawing.Point(0, 0);
			this.txt.Name = "txt";
			this.txt.ReadOnly = true;
			this.txt.Size = new System.Drawing.Size(159, 20);
			this.txt.TabIndex = 0;
			this.txt.Click += new System.EventHandler(this.txt_Click);
			// 
			// cmd
			// 
			this.cmd.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmd.Location = new System.Drawing.Point(159, 0);
			this.cmd.Name = "cmd";
			this.cmd.Size = new System.Drawing.Size(21, 21);
			this.cmd.TabIndex = 1;
			this.cmd.Text = "...";
			this.cmd.UseVisualStyleBackColor = true;
			this.cmd.Click += new System.EventHandler(this.txt_Click);
			// 
			// FileTextBoxControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txt);
			this.Controls.Add(this.cmd);
			this.Name = "FileTextBoxControl";
			this.Size = new System.Drawing.Size(180, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.Button cmd;
	}
}
