using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AwesomeControls.MultipleDocumentContainer.Dialogs;

namespace AwesomeControls.MultipleDocumentContainer
{
    public class MultipleDocumentContainerMessageFilter : System.Windows.Forms.IMessageFilter
    {
        private const int WM_KEYDOWN = 256;
        private const int WM_KEYUP = 257;

        private MultipleDocumentContainerControl mvarParent = null;
        public MultipleDocumentContainerMessageFilter(MultipleDocumentContainerControl parent)
        {
            mvarParent = parent;
        }

        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (mvarParent == null) return false;

            if (!mvarParent.IsWindowListDialogVisible && m.Msg == WM_KEYDOWN)
            {
                KeyEventArgs e = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
                if (e.KeyCode == Keys.Tab && e.Control)
                {
                    if (!mvarParent.IsActive) return false;
                    mvarParent.ShowWindowListDialog(true);
                    return true;
                }
            }
            else if (mvarParent.IsWindowListDialogVisible && m.Msg == WM_KEYUP)
            {
                KeyEventArgs e = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
                if (e.KeyCode == Keys.ControlKey)
                {
                    mvarParent.HideWindowListDialog();
                    return true;
                }
            }
            return false;
        }
    }
}
