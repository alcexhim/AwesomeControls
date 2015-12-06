using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.ChatBox
{
	public partial class ChatBoxControl : UserControl
	{
		public ChatBoxControl()
		{
			InitializeComponent();
		}

		public event ChatBoxMessageEventHandler MessageSent;
		protected virtual void OnMessageSent(ChatBoxMessageEventArgs e)
		{
			if (MessageSent != null) MessageSent(this, e);
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!e.Shift)
				{
					ChatBoxMessageEventArgs ee = new ChatBoxMessageEventArgs(txtInput.Text);
					OnMessageSent(ee);
					if (!ee.Cancel)
					{
						Color oldColor = txtOutput.SelectionColor;
						txtOutput.SelectionColor = Color.Blue;
						txtOutput.SelectionFont = new System.Drawing.Font(Font, FontStyle.Bold);
						txtOutput.SelectedText = "You: ";

						txtOutput.SelectionColor = oldColor;
						txtOutput.SelectionFont = Font;
						txtOutput.SelectedText = txtInput.Text;
						txtOutput.AppendText("\r\n\r\n");

						txtInput.Text = String.Empty;
					}
					e.SuppressKeyPress = true;
					e.Handled = true;
				}
			}
		}

		public delegate void ChatBoxMessageEventHandler(object sender, ChatBoxMessageEventArgs e);
		public class ChatBoxMessageEventArgs : CancelEventArgs
		{
			private string mvarMessage = String.Empty;
			public string Message { get { return mvarMessage; } set { mvarMessage = value; } }

			public ChatBoxMessageEventArgs(string message)
			{
				mvarMessage = message;
			}
		}

		public void ReceiveMessage(string username, string message)
		{
			Color oldColor = txtOutput.SelectionColor;
			txtOutput.SelectionColor = Color.Red;
			txtOutput.SelectionFont = new System.Drawing.Font(Font, FontStyle.Bold);
			txtOutput.SelectedText = username + ": ";

			txtOutput.SelectionColor = oldColor;
			txtOutput.SelectionFont = Font;
			txtOutput.SelectedText = message;
			txtOutput.AppendText("\r\n\r\n");
		}
	}
}
