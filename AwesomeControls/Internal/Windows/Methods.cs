using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AwesomeControls.Internal.Windows
{
	internal static class Methods
    {
        #region user32.dll
        [DllImport("user32.dll")]
		public static extern IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        [DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hWnd, Constants.WindowLongType flag);
        [DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, Constants.WindowLongType flag, int dwNewLong);

		#region DC
		[DllImport("User32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		#endregion


        #region SendMessage
        [DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewMessage msg, int wParam, int lParam);
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewMessage msg, int wParam, ref Structures.LVITEM hditem);
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewMessage msg, int wParam, ref Structures.RECT hditem);

        [DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewHeaderMessage msg, int wParam, int lParam);
        [DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewHeaderMessage msg, int wParam, ref Structures.HDITEM hditem);
        [DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.ListViewHeaderMessage msg, int wParam, ref Structures.RECT rect);
        #endregion
        #endregion
    }
}
