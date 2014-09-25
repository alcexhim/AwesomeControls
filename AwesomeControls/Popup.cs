using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls
{
	public partial class Popup : Form
	{
		public Popup()
		{
			InitializeComponent();
		}


		private const int WS_EX_NOACTIVATE = 0x08000000;

		private const int WM_MOUSEACTIVATE = 0x0021;
		private const int MA_NOACTIVATE = 0x0003;

		private const int WM_NCACTIVATE = 0x0086;
		
		[DllImport("user32.dll")]
		private static extern uint SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (Owner != null)
			{
				SendMessage(Owner.Handle, WM_NCACTIVATE, new IntPtr(1), IntPtr.Zero);
			}
		}

		protected override void OnDeactivate(EventArgs e)
		{
			// this is an ultra ugly hack to prevent the parent form from disappearing when the
			// popup is closed (which only happens 1/10 of the time...)
			if (Owner != null) Owner.TopMost = true;
			Hide();
			if (Owner != null) Owner.TopMost = false;
		}
	}
}
