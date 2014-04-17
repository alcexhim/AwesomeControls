using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PieMenu
{
    internal sealed class PieMenuMessageFilter : IMessageFilter
    {
        private PieMenuMessageFilter()
        {
        }

        private static PieMenuMessageFilter mvarInstance = new PieMenuMessageFilter();
        public static PieMenuMessageFilter Instance { get { return mvarInstance; } }

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        private static PieMenuWindow wnd = new PieMenuWindow();

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                if (m.WParam.ToInt32() == 0x20 && ((Control.ModifierKeys & Keys.Control) == Keys.Control)) /*VK_SPACE*/
                {
                    if (!wnd.Visible) wnd.ShowDialog();
                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (m.WParam.ToInt32() == 0x20 && ((Control.ModifierKeys & Keys.Control) == Keys.Control)) /*VK_SPACE*/
                {
                    wnd.Hide();
                }
            }
            return false;
        }
    }
}
