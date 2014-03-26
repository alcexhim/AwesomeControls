using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using System.Drawing;
using TextRenderer = System.Windows.Forms.TextRenderer;

namespace AwesomeControls.ActionBar
{
	public partial class ActionBarControl : System.Windows.Forms.Control
	{
		public ActionBarControl()
		{
			base.Dock = System.Windows.Forms.DockStyle.Top;
			base.DoubleBuffered = true;
			ResetFont();
		}

		public override void ResetFont()
		{
			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					{
						if (System.Environment.OSVersion.Version.Major >= 6)
						{
							// Use Segoe UI 9pt
							Font = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
						}
						else
						{
							// Use Tahoma 8pt
							Font = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point);
						}
						break;
					}
			}
		}

		protected virtual void ResetDock()
		{
			base.Dock = System.Windows.Forms.DockStyle.Top;
		}

		private System.Drawing.Size mvarDefaultSize = new System.Drawing.Size(128, 31);
		protected override System.Drawing.Size DefaultSize
		{
			get { return mvarDefaultSize; }
		}

		private ActionBarItem.ActionBarItemCollection mvarItems = new ActionBarItem.ActionBarItemCollection();
		public ActionBarItem.ActionBarItemCollection Items { get { return mvarItems; } }

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			UpdateItemRects();
			Refresh();
		}



		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);
			UpdateItemStates(e.Location, e.Button);
		}
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			UpdateItemStates(e.Location, e.Button);
		}
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);
			UpdateItemStates(e.Location, e.Button);

			ActionBarButton btn = (HitTest(e.Location) as ActionBarButton);
			if (btn != null) btn.OnClick(EventArgs.Empty);
		}

		private void UpdateItemStates()
		{
			foreach (ActionBarItem item in mvarItems)
			{
				item.State = ControlState.Normal;
			}
		}

		private System.Windows.Forms.ToolTip tip = new System.Windows.Forms.ToolTip();

		private ActionBarItem mvarCurrentPressedControl = null;
		private ActionBarItem mvarCurrentHoverControl = null;
		private ActionBarItem mvarCurrentTooltipControl = null;

		private void UpdateItemStates(Point point, System.Windows.Forms.MouseButtons buttons)
		{
			foreach (ActionBarItem item in mvarItems)
			{
				Rectangle itemRect = GetItemRect(item);

				if (itemRect.Contains(point))
				{
					if (buttons == System.Windows.Forms.MouseButtons.Left)
					{
						item.State = ControlState.Pressed;
						mvarCurrentPressedControl = item;
					}
					else
					{
						item.State = ControlState.Hover;
						mvarCurrentHoverControl = item;
					}
				}
				else
				{
					item.State = ControlState.Normal;
					if (item == mvarCurrentHoverControl)
					{
						mvarCurrentHoverControl = null;
					}
					if (item == mvarCurrentPressedControl)
					{
						mvarCurrentPressedControl = null;
					}
				}
				Invalidate(itemRect);
			}

			if (mvarCurrentHoverControl != null)
			{
				if (mvarCurrentHoverControl is ActionBarLabel)
				{
					if (mvarCurrentTooltipControl != mvarCurrentHoverControl)
					{
                        tip.Show((mvarCurrentHoverControl as ActionBarLabel).ToolTipText, this, point.X, point.Y + (System.Windows.Forms.Cursor.Current.Size.Height / 2), 0, tip.InitialDelay);
						mvarCurrentTooltipControl = mvarCurrentHoverControl;
					}
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			foreach (ActionBarItem item in mvarItems)
			{
				item.State = ControlState.Normal;
			}
			mvarCurrentTooltipControl = null;
			mvarCurrentHoverControl = null;
			mvarCurrentPressedControl = null;
			tip.Hide(this);
			Refresh();
		}

		public ActionBarItem HitTest(Point point)
		{
			foreach (ActionBarItem item in mvarItems)
			{
				Rectangle itemRect = GetItemRect(item);
				if (itemRect.Contains(point))
				{
					return item;
				}
			}
			return null;
		}

		private Dictionary<ActionBarItem, Rectangle> itemRects = new Dictionary<ActionBarItem, Rectangle>();
		public Rectangle GetItemRect(ActionBarItem item)
		{
			if (itemRects.Count == 0)
			{
				UpdateItemRects();
			}

			if (!itemRects.ContainsKey(item))
			{
				return default(Rectangle);
			}
			return itemRects[item];
		}

		private void UpdateItemRects()
		{
			Rectangle itemRect = new Rectangle(4, 3, 0, base.Height - 7);
			int offsetFromLeft = 4;
			int offsetFromRight = 4;

			foreach (ActionBarItem item1 in mvarItems)
			{
				Font font = item1.Font;
				if (font == null) font = base.Font;

				if (item1 is ActionBarButton)
				{
					ActionBarButton btn = (item1 as ActionBarButton);
					itemRect.Width = TextRenderer.MeasureText(btn.Text, font).Width + mvarItemSpacing;
				}

				if (item1.Alignment == ContentAlignment.BottomRight || item1.Alignment == ContentAlignment.MiddleRight || item1.Alignment == ContentAlignment.TopRight)
				{
					itemRect.X = base.Width - itemRect.Width - offsetFromRight;
					offsetFromRight += (itemRect.X + 4);
				}
				else
				{
					itemRect.X = offsetFromLeft;
					offsetFromLeft += (itemRect.Width + 4);
				}

				if (itemRects.ContainsKey(item1))
				{
					itemRects[item1] = itemRect;
				}
				else
				{
					itemRects.Add(item1, itemRect);
				}
			}
		}

		private bool mvarItemsSorted = false;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Theming.Theme.CurrentTheme.DrawActionBarBackground(e.Graphics, new System.Drawing.Rectangle(0, 0, base.Width, base.Height));

			if (!mvarItemsSorted)
			{
				mvarItems.Sort();
				mvarItemsSorted = true;
			}

			foreach (ActionBarItem item in mvarItems)
			{
				Font font = item.Font;
				if (font == null) font = base.Font;

				Rectangle itemRect = GetItemRect(item);
				OnDrawItem(new DrawActionBarItemEventArgs(e.Graphics, itemRect, item));
			}
		}

		private int mvarItemSpacing = 30;

		public event DrawActionBarItemEventHandler DrawItem;
		protected virtual void OnDrawItem(DrawActionBarItemEventArgs e)
		{
			Color color = Color.FromArgb(30, 57, 91);

			Rectangle rect = e.ClipRectangle;
			Font font = e.Item.Font;
			if (font == null) font = base.Font;

			if (e.Item is ActionBarButton)
			{
				ActionBarButton btn = (e.Item as ActionBarButton);

				Theming.Theme.CurrentTheme.DrawActionBarButtonBackground(e.Graphics, e.ClipRectangle, e.Item.State);
				TextRenderer.DrawText(e.Graphics, btn.Text, font, rect, color, System.Windows.Forms.TextFormatFlags.VerticalCenter | System.Windows.Forms.TextFormatFlags.HorizontalCenter);
			}
			else if (e.Item is ActionBarLabel)
			{
				ActionBarLabel lbl = (e.Item as ActionBarLabel);
				TextRenderer.DrawText(e.Graphics, lbl.Text, font, rect, color, System.Windows.Forms.TextFormatFlags.VerticalCenter | System.Windows.Forms.TextFormatFlags.HorizontalCenter);
			}

			if (DrawItem != null) DrawItem(this, e);
		}
	}
}
