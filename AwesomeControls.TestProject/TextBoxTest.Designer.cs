namespace AwesomeControls.TestProject
{
	partial class TextBoxTest
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
            this.textBox1 = new AwesomeControls.TextBox.TextBoxControl();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptReturn = true;
            this.textBox1.AutoSuggestFilter = true;
            this.textBox1.AutoSuggestMode = AwesomeControls.TextBox.TextBoxAutoSuggestMode.Popup;
            this.textBox1.CaretBlinkInterval = 530;
            this.textBox1.CaretColor = System.Drawing.Color.Black;
            this.textBox1.CaretOrientation = System.Windows.Forms.Orientation.Vertical;
            this.textBox1.CaretSize = 2;
            this.textBox1.CaseSensitive = true;
            this.textBox1.CharacterSpacing = 0;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.EnableCaret = true;
            this.textBox1.EnableCaretBlink = true;
            this.textBox1.EnableMultiSelection = true;
            this.textBox1.EnableOutlining = false;
            this.textBox1.EnableOverwrite = false;
            this.textBox1.EnableOverwriteShortcut = true;
            this.textBox1.EnableSelection = true;
            this.textBox1.EnableSyntaxHighlight = false;
            this.textBox1.HideSelection = true;
            this.textBox1.LineSeparator = AwesomeControls.TextBox.TextBoxLineSeparator.Default;
            this.textBox1.LineSeparatorString = "\r\n";
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = false;
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "";
            this.textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textBox1.SelectionStart = 0;
            this.textBox1.Size = new System.Drawing.Size(477, 262);
            this.textBox1.TabIndex = 0;
            this.textBox1.WordSpacing = 0;
            // 
            // TextBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 262);
            this.Controls.Add(this.textBox1);
            this.Name = "TextBoxTest";
            this.Text = "Code Window";
            this.ResumeLayout(false);

		}

		#endregion

        private AwesomeControls.TextBox.TextBoxControl textBox1;
	}
}