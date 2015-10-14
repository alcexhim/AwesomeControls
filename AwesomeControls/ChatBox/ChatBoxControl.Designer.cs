namespace AwesomeControls.ChatBox
{
	partial class ChatBoxControl
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
			this.scChat = new System.Windows.Forms.SplitContainer();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.txtInput = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.scChat)).BeginInit();
			this.scChat.Panel1.SuspendLayout();
			this.scChat.Panel2.SuspendLayout();
			this.scChat.SuspendLayout();
			this.SuspendLayout();
			// 
			// scChat
			// 
			this.scChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scChat.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scChat.Location = new System.Drawing.Point(0, 0);
			this.scChat.Name = "scChat";
			this.scChat.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scChat.Panel1
			// 
			this.scChat.Panel1.Controls.Add(this.txtOutput);
			// 
			// scChat.Panel2
			// 
			this.scChat.Panel2.Controls.Add(this.txtInput);
			this.scChat.Size = new System.Drawing.Size(366, 267);
			this.scChat.SplitterDistance = 159;
			this.scChat.TabIndex = 1;
			// 
			// txtOutput
			// 
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(0, 0);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(366, 159);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			// 
			// txtInput
			// 
			this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInput.HideSelection = false;
			this.txtInput.Location = new System.Drawing.Point(0, 0);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(366, 104);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
			// 
			// ChatBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scChat);
			this.Name = "ChatBox";
			this.Size = new System.Drawing.Size(366, 267);
			this.scChat.Panel1.ResumeLayout(false);
			this.scChat.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scChat)).EndInit();
			this.scChat.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scChat;
		private System.Windows.Forms.RichTextBox txtOutput;
		private System.Windows.Forms.RichTextBox txtInput;
	}
}
