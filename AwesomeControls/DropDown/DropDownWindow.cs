using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DropDown
{
	public partial class DropDownWindow : Popup
	{
		public DropDownWindow()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Theming.Theme.CurrentTheme.DrawDropDownMenuBackground(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Hide();
		}
	}
}
