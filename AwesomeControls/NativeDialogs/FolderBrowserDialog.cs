using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.NativeDialogs
{
	public class FolderBrowserDialog
	{
		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private string mvarSelectedPath = String.Empty;
		public string SelectedPath { get { return mvarSelectedPath; } set { mvarSelectedPath = value; } }
	
		private bool mvarShowNewFolderButton = true;
		public bool ShowNewFolderButton { get { return mvarShowNewFolderButton; } set { mvarShowNewFolderButton = value; } }

		public DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}
		public DialogResult ShowDialog(IWin32Window parent)
		{
			if (Environment.OSVersion.Version.Major >= 6)
			{
				Internal.FolderBrowserDialog.V2.FolderSelectDialog dlg = new Internal.FolderBrowserDialog.V2.FolderSelectDialog();
				dlg.Title = mvarDescription;
				dlg.InitialDirectory = mvarSelectedPath;
				if (dlg.ShowDialog(parent) == DialogResult.OK)
				{
					mvarSelectedPath = dlg.FileName;
					return DialogResult.OK;
				}
				return DialogResult.Cancel;
			}
			else
			{
				Internal.FolderBrowserDialog.V1.FolderBrowserDialogOld dlg = new Internal.FolderBrowserDialog.V1.FolderBrowserDialogOld();
				dlg.Description = mvarDescription;
				dlg.SelectedPath = mvarSelectedPath;
				dlg.ShowNewFolderButton = mvarShowNewFolderButton;

				if (dlg.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK)
				{
					mvarSelectedPath = dlg.SelectedPath;
					return DialogResult.OK;
				}
			}
			return DialogResult.Cancel;
		}
	}
}
