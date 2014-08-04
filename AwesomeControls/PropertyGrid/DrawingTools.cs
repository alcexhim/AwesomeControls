using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	internal class DrawingTools
	{
		private static Pen mvarHighlightPen = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));
		private static Pen mvarLightShadowPen = new Pen(Color.FromKnownColor(KnownColor.ControlLight));
		private static Pen mvarShadowPen = new Pen(Color.FromKnownColor(KnownColor.ControlDark));
		private static Pen mvarDarkShadowPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));

		public static void DrawRaisedBorder(Graphics g, Rectangle rect)
		{
			// Outer Top
			g.DrawLine(mvarLightShadowPen, rect.Left, rect.Top, rect.Right - 2, rect.Top);
			// Outer Left
			g.DrawLine(mvarLightShadowPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 2);
			// Inner Top
			g.DrawLine(mvarHighlightPen, rect.Left + 1, rect.Top + 1, rect.Right - 3, rect.Top + 1);
			// Inner Left
			g.DrawLine(mvarHighlightPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 3);

			// Outer Bottom
			g.DrawLine(mvarDarkShadowPen, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
			// Outer Right
			g.DrawLine(mvarDarkShadowPen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
			// Inner Bottom
			g.DrawLine(mvarShadowPen, rect.Left + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
			// Inner Right
			g.DrawLine(mvarDarkShadowPen, rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 2);
		}
		public static void DrawSunkenBorder(Graphics g, Rectangle rect)
		{
			// Outer Top
			g.DrawLine(mvarDarkShadowPen, rect.Left, rect.Top, rect.Right - 2, rect.Top);
			// Outer Left
			g.DrawLine(mvarDarkShadowPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 2);
			// Inner Top
			g.DrawLine(mvarShadowPen, rect.Left + 1, rect.Top + 1, rect.Right - 3, rect.Top + 1);
			// Inner Left
			g.DrawLine(mvarShadowPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 3);

			// Outer Bottom
			g.DrawLine(mvarLightShadowPen, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
			// Outer Right
			g.DrawLine(mvarLightShadowPen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
			// Inner Bottom
			g.DrawLine(mvarHighlightPen, rect.Left + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
			// Inner Right
			g.DrawLine(mvarHighlightPen, rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 2);
		}
		public static void DrawFocusRectangle(Graphics g, Rectangle rect)
		{
			Pen p = new Pen(Color.Black);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
			g.DrawRectangle(p, rect);
		}
		public static void DrawArrow(Graphics g, ArrowDirection direction, int x, int y)
		{
			DrawArrow(g, direction, x, y, 4);
		}
		public static void DrawArrow(Graphics g, ArrowDirection direction, int x, int y, int maxWidth)
		{
			switch (direction)
			{
				case ArrowDirection.Down:
				{
					int o = 0;
					while (true)
					{
						g.DrawLine(Pens.Black, x + o, y + o, x + maxWidth - o - 1, y + o);
						o++;
						if ((maxWidth - o - o) < 1)
						{
							g.DrawLine(Pens.Black, x + ((int)(maxWidth / 2)), y, x + ((int)(maxWidth / 2)), y + o - 1);
							break;
						}
					}
					break;
				}
			}
		}

		public static void PrepareGraphics(Graphics graphics)
		{
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault; // AntiAliasGridFit;
		}
	}
}
