using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PieMenu
{
	internal partial class PieMenuWindow : Form
	{
		public PieMenuWindow()
		{
			InitializeComponent();
			Font = new Font(SystemFonts.MenuFont, FontStyle.Bold);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// draw initial menu at center
			DrawMenuItem(e.Graphics, PieMenuManager.Title);

			int belowY = 64, aboveY = -64, x = 0, y = 0;
			foreach (PieMenuItemGroup group in PieMenuManager.Groups)
			{
				if (group.Position == PieMenuItemGroupPosition.Above)
				{
					y = aboveY;
				}
				else if (group.Position == PieMenuItemGroupPosition.Below)
				{
					y = belowY;
				}


				int totalWidth = 0;
				foreach (PieMenuItem item in group.Items)
				{
					totalWidth += 32 + (TextRenderer.MeasureText(item.Title, Font).Width);
				}
				x = (totalWidth / 2);

				foreach (PieMenuItem item in group.Items)
				{
					DrawMenuItem(e.Graphics, item.Title, new Point(x, y), item.Hover, item.Image);
					x += (TextRenderer.MeasureText(item.Title, Font).Width + 8);
				}


				if (group.Position == PieMenuItemGroupPosition.Above)
				{
					y -= 64;
					aboveY = y;
				}
				else if (group.Position == PieMenuItemGroupPosition.Below)
				{
					y += 64;
					belowY = y;
				}
			}

			// draw line from center of the screen to mouse pointer location
			// e.Graphics.DrawLine(borderPen, new Point(this.Width/ 2, this.Height / 2), PointToClient(Cursor.Position));
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			int belowY = 64, aboveY = -64, x = 0, y = 0;
			foreach (PieMenuItemGroup group in PieMenuManager.Groups)
			{
				if (group.Position == PieMenuItemGroupPosition.Above)
				{
					y = aboveY;
				}
				else if (group.Position == PieMenuItemGroupPosition.Below)
				{
					y = belowY;
				}

				int totalWidth = 0;
				foreach (PieMenuItem item in group.Items)
				{
					totalWidth += 32 + (TextRenderer.MeasureText(item.Title, Font).Width);
				}
				x = (totalWidth / 2);

				foreach (PieMenuItem item in group.Items)
				{
					Size size = TextRenderer.MeasureText(item.Title, Font);
					Rectangle rect = new Rectangle(x, y, size.Width, size.Height + 8);
					item.Hover = rect.Contains(e.Location);
					x += (size.Width + 8);
				}

				if (group.Position == PieMenuItemGroupPosition.Above)
				{
					y -= 64;
					aboveY = y;
				}
				else if (group.Position == PieMenuItemGroupPosition.Below)
				{
					y += 64;
					belowY = y;
				}
			}
			Refresh();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.Location = new Point((Cursor.Position.X - ((Width - Cursor.Size.Width) / 2)) - (Cursor.Size.Width / 2), (Cursor.Position.Y - ((Height - Cursor.Size.Height) / 2)) - (Cursor.Size.Height / 2));
			this.Size = Screen.FromControl(this).Bounds.Size;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				Hide();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				Hide();
			}
		}

		private void DrawMenuItem(Graphics g, string text)
		{
			Size size = TextRenderer.MeasureText(text, Font);
			Point point = new Point(0, 0);
			DrawMenuItem(g, text, point, false);
		}
		private void DrawMenuItem(Graphics g, string text, Point point, bool selected, Image image = null)
		{
			Size size = TextRenderer.MeasureText(text, Font);
			point.Offset((this.Width - size.Width) / 2, (this.Height - size.Height) / 2);
			DrawMenuItem(g, text, new Rectangle(point, size), selected, image);
		}

		private static Brush backgroundBrush = new SolidBrush(Color.White);
		private static Pen borderPen = new Pen(Color.Black, 2);

		private void DrawMenuItem(Graphics g, string text, Rectangle rect, bool selected, Image image = null)
		{
			Rectangle rectOuter = rect;
			rectOuter.Inflate(8, 8);

			if (image != null)
			{
				rectOuter.Inflate(image.Width, image.Height);
				g.DrawImage(image, new Point(rectOuter.X, rectOuter.Y));
			}

			Color color = Color.Black;
			if (selected)
			{
				backgroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
				borderPen = new Pen(Color.FromKnownColor(KnownColor.HighlightText), 2);
				color = Color.FromKnownColor(KnownColor.HighlightText);
			}
			else
			{
				backgroundBrush = new SolidBrush(Color.White);
				borderPen = new Pen(Color.Black, 2);
				color = Color.Black;
			}

			g.FillRectangle(backgroundBrush, rectOuter);
			g.DrawRectangle(borderPen, rectOuter);
			TextRenderer.DrawText(g, text, Font, rect, color, TextFormatFlags.Left);
		}
	}
}
