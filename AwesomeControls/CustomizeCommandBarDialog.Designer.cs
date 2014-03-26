namespace AwesomeControls
{
	partial class CustomizeCommandBarDialog
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
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdKeyboard = new System.Windows.Forms.Button();
			this.tabToolbars = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.tabCommands = new System.Windows.Forms.TabPage();
			this.tabToolbars.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(341, 294);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(75, 23);
			this.cmdClose.TabIndex = 0;
			this.cmdClose.Text = "Close";
			this.cmdClose.UseVisualStyleBackColor = true;
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// cmdKeyboard
			// 
			this.cmdKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdKeyboard.Location = new System.Drawing.Point(260, 294);
			this.cmdKeyboard.Name = "cmdKeyboard";
			this.cmdKeyboard.Size = new System.Drawing.Size(75, 23);
			this.cmdKeyboard.TabIndex = 0;
			this.cmdKeyboard.Text = "&Keyboard...";
			this.cmdKeyboard.UseVisualStyleBackColor = true;
			// 
			// tabToolbars
			// 
			this.tabToolbars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabToolbars.Controls.Add(this.tabPage1);
			this.tabToolbars.Controls.Add(this.tabCommands);
			this.tabToolbars.Location = new System.Drawing.Point(12, 12);
			this.tabToolbars.Name = "tabToolbars";
			this.tabToolbars.SelectedIndex = 0;
			this.tabToolbars.Size = new System.Drawing.Size(404, 268);
			this.tabToolbars.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.checkedListBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(396, 242);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Toolbars";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button3.Location = new System.Drawing.Point(318, 61);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "button1";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(318, 32);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(318, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(3, 3);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(309, 229);
			this.checkedListBox1.TabIndex = 0;
			// 
			// tabCommands
			// 
			this.tabCommands.Location = new System.Drawing.Point(4, 22);
			this.tabCommands.Name = "tabCommands";
			this.tabCommands.Padding = new System.Windows.Forms.Padding(3);
			this.tabCommands.Size = new System.Drawing.Size(396, 250);
			this.tabCommands.TabIndex = 1;
			this.tabCommands.Text = "Commands";
			this.tabCommands.UseVisualStyleBackColor = true;
			// 
			// CustomizeCommandBarDialog
			// 
			this.AcceptButton = this.cmdClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(428, 329);
			this.Controls.Add(this.tabToolbars);
			this.Controls.Add(this.cmdKeyboard);
			this.Controls.Add(this.cmdClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CustomizeCommandBarDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Customize";
			this.tabToolbars.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdKeyboard;
		private System.Windows.Forms.TabControl tabToolbars;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabCommands;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
	}
}