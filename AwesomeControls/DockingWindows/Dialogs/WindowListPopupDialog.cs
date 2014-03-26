using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DockingWindows.Dialogs
{
	public partial class WindowListPopupDialog : Window
	{
		public WindowListPopupDialog(DockingContainerControl parentContainer)
		{
			InitializeComponent();

			base.Animate = true;
			base.UseThemeWindowBorder = false;
			base.DropShadow = true;
			base.ShowInTaskbar = false;
			base.ClientSize = new Size(389, 487);
			base.KeyPreview = true;
			mvarParentContainer = parentContainer;
		}

		private DockingContainerControl mvarParentContainer = null;

		private DockingWindow mvarSelectedWindow = null;
		public DockingWindow SelectedWindow { get { return mvarSelectedWindow; } set { mvarSelectedWindow = value; } }

		protected override bool ShowWithoutActivation { get { return true; } }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (mvarParentContainer == null || mvarSelectedWindow == null) return;
			if (e.KeyCode == Keys.Tab && e.Control)
			{
				CycleWindows(e.Shift);
			}
		}

		public void CycleWindows(bool reverse)
		{
			if (mvarParentContainer.Windows.Count == 0) return;

			int index = mvarSelectedWindow.ParentArea.Windows.IndexOf(mvarSelectedWindow);
			if (reverse)
			{
				index--;
			}
			else
			{
				index++;
			}
			if (index < 0) index = mvarSelectedWindow.ParentArea.Windows.Count - 1;
			if (index >= mvarSelectedWindow.ParentArea.Windows.Count) index = 0;
			mvarSelectedWindow = mvarSelectedWindow.ParentArea.Windows[index];
			Refresh();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (mvarParentContainer == null) return;
			if (mvarSelectedWindow == null)
			{
				mvarSelectedWindow = mvarParentContainer.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows[0];
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Theming.Theme.CurrentTheme.DrawDocumentSwitcherBackground(e.Graphics, new Rectangle(0, 0, Width, Height));
			if (mvarParentContainer != null)
			{
				Rectangle rectItem = new Rectangle(198, 82, 179, 20);
				Font font = Theming.Theme.CurrentTheme.FontTable.Default;
				
				Color backColor = Color.Empty;
				Color borderColor = Color.Empty;
				Color foreColor = Theming.Theme.CurrentTheme.ColorTable.DocumentSwitcherText;

				foreach (DockingWindow dw in mvarParentContainer.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows)
				{
					if (mvarSelectedWindow == dw)
					{
						foreColor = Theming.Theme.CurrentTheme.ColorTable.DocumentSwitcherSelectionText;
						backColor = Theming.Theme.CurrentTheme.ColorTable.DocumentSwitcherSelectionBackground;
						borderColor = Theming.Theme.CurrentTheme.ColorTable.DocumentSwitcherSelectionBorder;
					}
					else
					{
						foreColor = Theming.Theme.CurrentTheme.ColorTable.DocumentSwitcherText;
						backColor = Color.Empty;
						borderColor = Color.Empty;
					}

					e.Graphics.FillRectangle(new SolidBrush(backColor), rectItem);
					e.Graphics.DrawRectangle(new Pen(borderColor), rectItem);

					TextRenderer.DrawText(e.Graphics, dw.Title, font, rectItem, foreColor);
					rectItem.Y += (rectItem.Height + 1);
				}
			}
		}
	}
}
