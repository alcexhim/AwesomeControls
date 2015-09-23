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
	public partial class Window : Form
	{
		public Window()
		{
			InitializeComponent();
			base.DoubleBuffered = true;
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (Theming.Theme.CurrentTheme.HasCustomToplevelWindowFrame) Theming.Theme.CurrentTheme.DrawToplevelWindowBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), this.Text);
		}


		private bool animationInProgress = false;

		private bool mvarAnimate = false;
		public bool Animate { get { return mvarAnimate; } set { mvarAnimate = value; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (!animationInProgress && mvarAnimate)
			{
				animationInProgress = true;
				for (int i = 0; i < 100; i++)
				{
					base.Opacity = (double)i / 100;
					Application.DoEvents();

					System.Threading.Thread.Sleep(2);
				}
				animationInProgress = false;
			}
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			if (!animationInProgress && mvarAnimate)
			{
				animationInProgress = true;
				for (int i = 100; i >= 0; i--)
				{
					base.Opacity = (double)i / 100;
					Application.DoEvents();

					System.Threading.Thread.Sleep(2);
				}
				animationInProgress = false;
			}
		}

		private bool mvarUseThemeWindowBorder = true;
		public bool UseThemeWindowBorder { get { return mvarUseThemeWindowBorder; } set { mvarUseThemeWindowBorder = value; } }

		public const bool ThemeWindowBorderEnabled = true;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (IsUsingThemeWindowBorder())
			{
				Point pt = PointToScreen(e.Location);

				if (pt.X >= this.Left && pt.Y >= this.Top && pt.X <= this.Right && pt.Y <= this.Top + 32)
				{
					// thanks http://webspace.webring.com/people/lp/practicalvb/vb/input/dragform.html
					Internal.Windows.Methods.ReleaseCapture();
					Internal.Windows.Methods.SendMessage(this.Handle, Internal.Windows.Constants.WindowMessage.WM_NCLBUTTONDOWN, Internal.Windows.Constants.HTCAPTION, 0);
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (IsUsingThemeWindowBorder())
			{
				// Internal.Windows.Methods.ReleaseCapture();
			}
		}

		private bool IsUsingThemeWindowBorder()
		{
			return ThemeWindowBorderEnabled && mvarUseThemeWindowBorder && Theming.Theme.CurrentTheme.HasCustomToplevelWindowFrame;
		}

		protected override void WndProc(ref Message m)
		{
			if (IsUsingThemeWindowBorder())
			{
				switch (Environment.OSVersion.Platform)
				{
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
					{
						Internal.Windows.Constants.WindowMessage msg = (Internal.Windows.Constants.WindowMessage)m.Msg;
						switch (msg)
						{
							case Internal.Windows.Constants.WindowMessage.WM_NCCALCSIZE:
							{
								System.Drawing.Rectangle rect = (System.Drawing.Rectangle)System.Runtime.InteropServices.Marshal.PtrToStructure(m.LParam, typeof(System.Drawing.Rectangle));

								int captionHeight = SystemInformation.CaptionHeight + SystemInformation.HorizontalResizeBorderThickness;

								int customBorderWidth = 1;

								rect.X -= SystemInformation.VerticalResizeBorderThickness;
								rect.Width += SystemInformation.VerticalResizeBorderThickness;
								rect.Y -= captionHeight;
								rect.Height += SystemInformation.HorizontalResizeBorderThickness;

								this.Padding = new System.Windows.Forms.Padding(customBorderWidth, 32, customBorderWidth, customBorderWidth);

								System.Runtime.InteropServices.Marshal.StructureToPtr(rect, m.LParam, true);
								break;
							}
							case Internal.Windows.Constants.WindowMessage.Activate:
							case Internal.Windows.Constants.WindowMessage.NonClientPaint:
							case Internal.Windows.Constants.WindowMessage.NonClientActivate:
							case Internal.Windows.Constants.WindowMessage.SetText:
							{
								/*
								IntPtr hdc = Internal.Windows.Methods.GetWindowDC(m.HWnd);
								if ((int)hdc != 0)
								{
									Graphics g = Graphics.FromHdc(hdc);

									Theming.Theme.CurrentTheme.DrawToplevelWindowBorder(g, new Rectangle(0, 0, Width, 32), Text);

									g.Flush();
									Internal.Windows.Methods.ReleaseDC(m.HWnd, hdc);
								}

								if (msg == Internal.Windows.Constants.WindowMessage.NonClientPaint || msg == Internal.Windows.Constants.WindowMessage.SetText) return;
								*/
								break;
							}
						}
						break;
					}
				}
			}
			base.WndProc(ref m);
		}

		private bool mvarDropShadow = false;
		public bool DropShadow
		{
			get { return mvarDropShadow; }
			set
			{
				if (IsHandleCreated) throw new InvalidOperationException("Must be set before handle is created");
				mvarDropShadow = value;
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				if (mvarDropShadow)
				{
					cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
				}
				return cp;
			}
		}
	}
}
