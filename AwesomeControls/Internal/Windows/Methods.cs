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
		[DllImport("user32.dll")]
		public static extern int ReleaseCapture();


		#region DC
		[DllImport("User32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		#endregion

		[DllImport("user32.dll")]
		public static extern bool UpdateLayeredWindow
		(
			[In()] IntPtr hwnd,
			[In(), Optional()] IntPtr hdcDst,
			[In(), Optional()] ref Structures.POINT pptDst,
			[In(), Optional()] ref Structures.SIZE psize,
			[In(), Optional()] IntPtr hdcSrc,
			[In(), Optional()] ref Structures.POINT pptSrc,
			[In()] int /*COLORREF*/ crKey,
			[In(), Optional()] ref Structures.BLENDFUNCTION pblend,
			[In()] Constants.UpdateLayeredWindowFlags dwFlags
		);

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
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Constants.WindowMessage msg, int wParam, int lParam);
        #endregion
        #endregion

	}
}
