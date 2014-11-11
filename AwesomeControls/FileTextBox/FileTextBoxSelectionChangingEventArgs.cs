using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.FileTextBox
{
	public delegate void FileTextBoxSelectionChangingEventHandler(object sender, FileTextBoxSelectionChangingEventArgs e);
	public class FileTextBoxSelectionChangingEventArgs : CancelEventArgs
	{
		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		public FileTextBoxSelectionChangingEventArgs(string fileName)
		{
			mvarFileName = fileName;
		}
	}
}
