using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.NativeDialogs.Internal
{
	internal class Delegates
	{
		public delegate int BrowseCallbackProc(IntPtr hwnd, int uMsg, IntPtr lParam, IntPtr lpData);
	}
}
