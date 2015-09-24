using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls
{
	public partial class DropShadowWindow : Form
	{
		public DropShadowWindow()
		{
			InitializeComponent();
			
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.BackColor = Color.Transparent;
			base.TransparencyKey = Color.Transparent;
		}

		private DropShadowPosition mvarPosition = DropShadowPosition.Top;
		public DropShadowPosition Position
		{
			get { return mvarPosition; }
			set
			{
				mvarPosition = value; switch (mvarPosition)
				{
					case DropShadowPosition.Left:
					case DropShadowPosition.Right:
					{
						Cursor = System.Windows.Forms.Cursors.SizeWE;
						break;
					}
					case DropShadowPosition.Top:
					case DropShadowPosition.Bottom:
					{
						Cursor = System.Windows.Forms.Cursors.SizeNS;
						break;
					}
				}
			}
		}

		private Point initialPointerLocation = new Point();
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			initialPointerLocation = e.Location;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				switch (mvarPosition)
				{
					case DropShadowPosition.Top:
					{
						break;
					}
					case DropShadowPosition.Left:
					{
						break;
					}
					case DropShadowPosition.Right:
					{
						break;
					}
					case DropShadowPosition.Bottom:
					{
						break;
					}
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// empty implementation
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			DrawFade(e.Graphics);
		}

		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			RefreshBackgroundImage();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			RefreshBackgroundImage();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			RefreshBackgroundImage();
		}

		private void RefreshBackgroundImage()
		{
			Bitmap bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			DrawFade(g);
		}

		private float percentAlphaMultiplier = 0.1f;
		private void DrawFade(Graphics g)
		{
			System.Drawing.Color color = System.Drawing.Color.Blue;
			switch (mvarPosition)
			{
				case DropShadowPosition.Top:
				{
					for (int i = this.Height - 1; i >= 0; i--)
					{
						double percentAlpha = ((double)i / (double)this.Height);
						percentAlpha *= percentAlphaMultiplier;

						byte alpha = (byte)(percentAlpha * 255);
						color = System.Drawing.Color.FromArgb(alpha, color.R, color.G, color.B);

						g.DrawLine(new Pen(color), 0, i, Width, i);
					}
					break;
				}
				case DropShadowPosition.Bottom:
				{
					for (int i = 0; i < this.Height; i++)
					{
						double percentAlpha = ((double)(this.Height - i) / (double)this.Height);
						percentAlpha *= percentAlphaMultiplier;

						byte alpha = (byte)(percentAlpha * 255);
						color = System.Drawing.Color.FromArgb(alpha, color.R, color.G, color.B);

						g.DrawLine(new Pen(color), 0, i, Width, i);
					}
					break;
				}
				case DropShadowPosition.Left:
				{
					for (int i = this.Width - 1; i >= 0; i--)
					{
						double percentAlpha = ((double)i / (double)this.Width);
						percentAlpha *= percentAlphaMultiplier;

						byte alpha = (byte)(percentAlpha * 255);
						color = System.Drawing.Color.FromArgb(alpha, color.R, color.G, color.B);

						g.DrawLine(new Pen(color), i, 0, i, Height);
					}
					break;
				}
				case DropShadowPosition.Right:
				{
					for (int i = 0; i < this.Width; i++)
					{
						double percentAlpha = ((double)(this.Width - i) / (double)this.Width);
						percentAlpha *= percentAlphaMultiplier;

						byte alpha = (byte)(percentAlpha * 255);
						color = System.Drawing.Color.FromArgb(alpha, color.R, color.G, color.B);

						g.DrawLine(new Pen(color), i, 0, i, Height);
					}
					break;
				}
			}
		}
	}
}
