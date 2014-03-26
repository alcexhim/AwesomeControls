using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DockingWindows
{
	public class DockingContainerMessageFilter : IMessageFilter
	{
		private const int WM_KEYDOWN = 256;
		private const int WM_KEYUP = 257;

		private DockingContainerControl mvarParent = null;
		public DockingContainerMessageFilter(DockingContainerControl parent)
		{
			mvarParent = parent;
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (mvarParent == null) return false;

			if (!mvarParent.IsWindowListPopupDialogVisible && m.Msg == WM_KEYDOWN)
			{
				KeyEventArgs e = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
				if (e.KeyCode == Keys.Tab && e.Control)
				{
					if (!mvarParent.IsActive) return false;
					mvarParent.ShowWindowListPopupDialog();
					return true;
				}
			}
			else if (mvarParent.IsWindowListPopupDialogVisible && m.Msg == WM_KEYUP)
			{
				KeyEventArgs e = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
				if (e.KeyCode == Keys.ControlKey)
				{
					mvarParent.HideWindowListPopupDialog();
					return true;
				}
			}
			else if (mvarParent.IsWindowListPopupDialogVisible && m.Msg == WM_KEYDOWN)
			{
				KeyEventArgs e = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
				if (e.KeyCode == Keys.Tab && e.Control)
				{
					mvarParent.CycleWindowListPopupDialog(e.Shift);
					return true;
				}
			}
			return false;
		}
	}
}
