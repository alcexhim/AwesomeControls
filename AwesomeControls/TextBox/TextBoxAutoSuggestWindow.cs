using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TextBox
{
	public partial class TextBoxAutoSuggestWindow : Form
	{
		public TextBoxAutoSuggestWindow()
		{
			InitializeComponent();
			ClientSize = new System.Drawing.Size(275, 174);
			Font = SystemFonts.MenuFont;
		}

		#region Creation Parameters

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndParent);

		public new void Show(IWin32Window parent)
		{
			// if (this.Handle == IntPtr.Zero) base.CreateControl();
			// SetParent(base.Handle, parent.Handle);
			base.Show(parent);
		}
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x86)  //WM_NCACTIVATE
			{
				if (m.WParam != IntPtr.Zero) //activate
				{
					SendMessage(this.Handle, 0x86, (IntPtr)1, IntPtr.Zero);
				}
				this.DefWndProc(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		protected override bool ShowWithoutActivation { get { return true; } }
		#endregion

		internal TextBoxControl parent = null;
		
		private void lst_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(lst.BackColor), e.Bounds);
			if (e.Index > -1)
			{
				// draw the background
				Rectangle rect = e.Bounds;
				rect.X += 18;
				rect.Width -= 18;
				AwesomeControls.Theming.Theme.CurrentTheme.DrawListItemBackground(e.Graphics, rect, ControlState.Normal, ((e.State & DrawItemState.Selected) == DrawItemState.Selected), true);

				TextBoxAutoSuggestTermItem term = (lst.Items[e.Index] as TextBoxAutoSuggestTermItem);
				if (term == null) return;	

				if (term.Image != null)
				{
					e.Graphics.DrawImage(term.Image, e.Bounds.X, e.Bounds.Y, e.Bounds.Height - 2, e.Bounds.Height);
				}
				TextRenderer.DrawText(e.Graphics, term.Value, Font, new Rectangle(e.Bounds.X + e.Bounds.Height + 2, e.Bounds.Y + 2, e.Bounds.Width - e.Bounds.Height - 4, e.Bounds.Height), AwesomeControls.Theming.Theme.CurrentTheme.ColorTable.ListViewItemSelectedForeground, TextFormatFlags.Left);
			}

			if (tip != null && tip.Visible) ShowTextBoxToolTipWindow();
			// e.DrawFocusRectangle();
		}

		private TextBoxToolTipWindow tip = null;
		private void ShowTextBoxToolTipWindow(string text = null)
		{
			if (tip == null) tip = new TextBoxToolTipWindow();
			if (tip.IsDisposed) tip = new TextBoxToolTipWindow();

			Point pt = new Point(this.Width, (lst.ItemHeight * (lst.SelectedIndex - lst.TopIndex)) - SystemInformation.FrameBorderSize.Height);
			pt = PointToScreen(pt);

			tip.Left = pt.X;
			tip.Top = pt.Y;
			if (text != null)
			{
				tip.txt.Text = text;
			}
			if (!tip.Visible)
			{
				tip.Show(this);
			}
		}
		private void HideTextBoxToolTipWindow()
		{
			if (tip == null) tip = new TextBoxToolTipWindow();
			if (tip.IsDisposed) tip = new TextBoxToolTipWindow();
			tip.Hide();
		}

		private void lst_SelectedIndexChanged(object sender, EventArgs e)
		{
			HideTextBoxToolTipWindow();

			if (lst.SelectedIndex < 0 || lst.SelectedIndex >= lst.Items.Count)
			{
				HideTextBoxToolTipWindow();
				return;
			}

			TextBoxAutoSuggestTermItem term = (lst.Items[lst.SelectedIndex] as TextBoxAutoSuggestTermItem);
			if (term == null)
			{
				HideTextBoxToolTipWindow();
				return;
			}

			if (String.IsNullOrEmpty(term.Description))
			{
				HideTextBoxToolTipWindow();
			}
			else
			{
				ShowTextBoxToolTipWindow(term.Description);
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			HideTextBoxToolTipWindow();
		}

		private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (parent != null) parent.ACAcceptAutoCompleteList();
		}

		private void lst_MouseDown(object sender, MouseEventArgs e)
		{
		}
	}
}
