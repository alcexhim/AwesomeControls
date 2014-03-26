using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AwesomeControls.Theming
{
	public class ClassicTheme : Theme
	{
		private string mvarName = "Classic";
		public override string Name
		{
			get
			{
				return mvarName;
			}
		}

		protected override void InitCommonColors ()
		{
			base.InitCommonColors ();

			ColorTable.CommandBarControlBackground = Color.FromKnownColor (KnownColor.Control);
			ColorTable.CommandBarControlText = Color.FromKnownColor (KnownColor.ControlText);
			ColorTable.CommandBarControlTextHover = Color.FromKnownColor (KnownColor.HighlightText);
			ColorTable.CommandBarControlTextPressed = Color.FromKnownColor (KnownColor.HighlightText);
			ColorTable.CommandBarControlTextDisabled = Color.FromKnownColor(KnownColor.GrayText);

			ColorTable.CommandBarBackground = Color.FromKnownColor(KnownColor.Control);
			ColorTable.CommandBarPanelGradientBegin = Color.FromKnownColor(KnownColor.Control);
			ColorTable.CommandBarPanelGradientEnd = Color.FromKnownColor(KnownColor.Control);
			
			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
					ColorTable.CommandBarControlBackgroundHover = Color.FromKnownColor (KnownColor.ActiveCaption);
					break;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
					ColorTable.CommandBarControlBackgroundHover = Color.FromKnownColor (KnownColor.Highlight);
					break;
			}
			
			
			ColorTable.CommandBarMenuBorder = Color.FromKnownColor (KnownColor.ControlDark);
			ColorTable.CommandBarMenuBorderLight = Color.FromKnownColor (KnownColor.ControlLightLight);
			
			ColorTable.CommandBarMenuBackground = Color.FromKnownColor (KnownColor.Menu);
			ColorTable.CommandBarMenuControlText = ColorTable.CommandBarControlText;
			ColorTable.CommandBarMenuControlTextHighlight = ColorTable.CommandBarControlTextHover;
			ColorTable.CommandBarMenuControlTextPressed = ColorTable.CommandBarControlTextPressed;
			ColorTable.CommandBarMenuControlTextDisabled = ColorTable.CommandBarControlTextDisabled;
			
		}
		public override void DrawCommandBarBorder (Graphics graphics, System.Windows.Forms.ToolStrip toolStrip, Rectangle connectedArea)
		{
			Rectangle rect = new Rectangle (0, 0, toolStrip.Bounds.Width - 1, toolStrip.Bounds.Height - 1);
			if (toolStrip is System.Windows.Forms.MenuStrip)
			{
				graphics.DrawLine (new Pen (ColorTable.CommandBarMenuBorder), rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
				graphics.DrawLine (new Pen (ColorTable.CommandBarMenuBorderLight), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
			}
			else if (toolStrip is System.Windows.Forms.ToolStripDropDownMenu)
			{
				graphics.DrawRectangle (new Pen (ColorTable.CommandBarMenuBorder), rect);
			}
		}
		public override void DrawCommandBarBackground (Graphics graphics, System.Windows.Forms.ToolStrip parent)
		{
			if (parent is System.Windows.Forms.ToolStripDropDown)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarMenuBackground), new Rectangle(new Point(0, 0), parent.Bounds.Size));
			}
		}
		public override void DrawCommandBarPanelBackground(Graphics graphics, Rectangle rectangle)
		{
			if (rectangle.Width < 1 || rectangle.Height < 1) return;
			LinearGradientBrush brush = new LinearGradientBrush(rectangle, ColorTable.CommandBarPanelGradientBegin, ColorTable.CommandBarPanelGradientEnd, LinearGradientMode.Horizontal);
			graphics.FillRectangle(brush, rectangle);
		}
		public override void DrawMenuItemBackground (Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height);

			if (item.Selected || item.Pressed)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
			}
		}

		public override void DrawCommandButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height);
			if (item.Pressed)
			{
				DrawingTools.DrawSunkenBorderMini(graphics, rect);
			}
			else if (item.Selected)
			{
				DrawingTools.DrawRaisedBorderMini(graphics, rect);
			}
		}

		#region System Controls
		#region Button
		public override void DrawButtonBackground(Graphics g, Rectangle rect, ControlState state)
		{
			switch (state)
			{
				case ControlState.Normal:
				case ControlState.Hover:
				{
					DrawingTools.DrawRaisedBorder(g, rect);
					break;
				}
				case ControlState.Pressed:
				{
					DrawingTools.DrawSunkenBorder(g, rect);
					break;
				}
			}
		}
		#endregion
		#region TextBox
		public override void DrawTextBoxBackground(Graphics g, Rectangle rect, ControlState state)
		{
			DrawingTools.DrawSunkenBorder(g, rect);
		}
		#endregion
		#region ListView
		public override void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused)
		{
			if (selected)
			{
				g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), rect);
			}
		}
		public override void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted)
		{
			g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
			if (state == ControlState.Pressed)
			{
				DrawingTools.DrawSunkenBorder(g, rect);
			}
			else
			{
				DrawingTools.DrawRaisedBorder(g, rect);
			}
		}
		public override void DrawListSelectionRectangle(Graphics g, Rectangle rect)
		{
			Pen pen = new Pen(Color.Black);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			g.DrawRectangle(pen, rect);
		}
		public override void DrawListViewTreeGlyph(Graphics g, Rectangle rect, ControlState state, bool expanded)
		{
			rect.Y += 2;
			rect.Width = 8;
			rect.Height = 8;
			g.DrawRectangle(DrawingTools.Pens.ShadowPen, rect);

			// horizontal line
			g.DrawLine(DrawingTools.Pens.ControlTextPen, rect.X + 2, rect.Y + (rect.Height / 2), rect.Right - 2, rect.Y + (rect.Height / 2));

			if (!expanded)
			{
				// vertical line
				g.DrawLine(DrawingTools.Pens.ControlTextPen, rect.X + (rect.Width / 2), rect.Y + 2, rect.X + (rect.Width / 2), rect.Bottom - 2);
			}
		}
		#endregion
		#region ProgressBar
		public override void DrawProgressBarBackground(Graphics g, Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			DrawingTools.DrawSunkenBorder(g, rect);
		}
		public override void DrawProgressBarChunk(Graphics g, Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			g.FillRectangle(new SolidBrush(ColorTable.FocusedHighlightedBackground), rect);
		}
		public override void DrawProgressBarPulse(Graphics g, Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			// do nothing here, there are no progress bar pulses in Classic theme
		}
		#endregion
		#endregion
	}
}

