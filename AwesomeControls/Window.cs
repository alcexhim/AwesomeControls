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

			RefreshDropShadowWindows();
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

		private bool mvarIsActive = false;
		public bool IsActive { get { return mvarIsActive; } }


		private DropShadowVisibility mvarDropShadowVisibility = DropShadowVisibility.Default;
		[Category("Appearance")]
		public DropShadowVisibility DropShadowVisibility
		{
			get { return mvarDropShadowVisibility; }
			set
			{
				mvarDropShadowVisibility = value;
				if (!DesignMode) RefreshDropShadowWindows();
			}
		}

		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			RefreshDropShadowWindows(RefreshDropShadowWindowsFlags.Position);
		}

		private bool IsDropShadowVisible()
		{
			// not implemented properly (yet...)
			return false;

			if (mvarDropShadowVisibility == AwesomeControls.DropShadowVisibility.None) return false;
			// TODO: logic goes here!
			return true;
		}

		private void RefreshDropShadowWindows(RefreshDropShadowWindowsFlags flags = RefreshDropShadowWindowsFlags.All)
		{
			int DropShadowSize = 8;

			if (mvarDropShadowWindows == null) mvarDropShadowWindows = new DropShadowWindow[4];
			for (int i = 0; i < mvarDropShadowWindows.Length; i++)
			{
				if (mvarDropShadowWindows[i] == null) mvarDropShadowWindows[i] = new DropShadowWindow();
				if (mvarDropShadowWindows[i].IsDisposed) mvarDropShadowWindows[i] = new DropShadowWindow();
				if (!IsDropShadowVisible())
				{
					mvarDropShadowWindows[i].Visible = false;
					continue;
				}

				mvarDropShadowWindows[i].Position = (DropShadowPosition)i;

				switch (mvarDropShadowWindows[i].Position)
				{
					case DropShadowPosition.Top:
					{
						if ((flags & RefreshDropShadowWindowsFlags.Size) == RefreshDropShadowWindowsFlags.Size)
						{
							mvarDropShadowWindows[i].Width = this.Width;
							mvarDropShadowWindows[i].Height = DropShadowSize;
						}
						mvarDropShadowWindows[i].Left = this.Left;
						mvarDropShadowWindows[i].Top = this.Top - mvarDropShadowWindows[i].Height;
						break;
					}
					case DropShadowPosition.Bottom:
					{
						if ((flags & RefreshDropShadowWindowsFlags.Size) == RefreshDropShadowWindowsFlags.Size)
						{
							mvarDropShadowWindows[i].Width = this.Width;
							mvarDropShadowWindows[i].Height = DropShadowSize;
						}
						mvarDropShadowWindows[i].Left = this.Left;
						mvarDropShadowWindows[i].Top = this.Bottom;
						break;
					}
					case DropShadowPosition.Left:
					{
						if ((flags & RefreshDropShadowWindowsFlags.Size) == RefreshDropShadowWindowsFlags.Size)
						{
							mvarDropShadowWindows[i].Width = DropShadowSize;
							mvarDropShadowWindows[i].Height = this.Height;
						}
						mvarDropShadowWindows[i].Left = this.Left - mvarDropShadowWindows[i].Width;
						mvarDropShadowWindows[i].Top = this.Top;
						break;
					}
					case DropShadowPosition.Right:
					{
						if ((flags & RefreshDropShadowWindowsFlags.Size) == RefreshDropShadowWindowsFlags.Size)
						{
							mvarDropShadowWindows[i].Width = DropShadowSize;
							mvarDropShadowWindows[i].Height = this.Height;
						}
						mvarDropShadowWindows[i].Left = this.Right;
						mvarDropShadowWindows[i].Top = this.Top;
						break;
					}
				}

				if (((flags & RefreshDropShadowWindowsFlags.Size) == RefreshDropShadowWindowsFlags.Size) || !mvarDropShadowWindows[i].Visible)
				{
					if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Minimized)
					{
						mvarDropShadowWindows[i].Visible = false;
					}
					else
					{
						mvarDropShadowWindows[i].Visible = true;
					}
				}
			}

			if ((flags & RefreshDropShadowWindowsFlags.Activation) == RefreshDropShadowWindowsFlags.Activation)
			{
				inhibitActivate = true;
				for (int i = 0; i < mvarDropShadowWindows.Length; i++)
				{
					mvarDropShadowWindows[i].BringToFront();
				}
				BringToFront();
				Focus();
				inhibitActivate = false;
			}
		}

		private DropShadowWindow[] mvarDropShadowWindows = null;
		private bool inhibitActivate = false;

		protected override void OnActivated(EventArgs e)
		{
			mvarIsActive = true;
			Invalidate();

			if (!inhibitActivate)
			{
				base.OnActivated(e);

				RefreshDropShadowWindows(RefreshDropShadowWindowsFlags.Activation);
			}
		}
		protected override void OnDeactivate(EventArgs e)
		{
			mvarIsActive = false;
			Invalidate();

			if (!inhibitActivate)
			{
				base.OnDeactivate(e);

				RefreshDropShadowWindows(RefreshDropShadowWindowsFlags.Activation);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (Theming.Theme.CurrentTheme.HasCustomToplevelWindowFrame)
			{
				Theming.Theme.CurrentTheme.DrawToplevelWindowBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), this.Text, this.IsActive);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (Theming.Theme.CurrentTheme.HasCustomToplevelWindowFrame)
			{
				Invalidate(new Rectangle(0, 0, this.Width, this.Height));
			}
			RefreshDropShadowWindows(RefreshDropShadowWindowsFlags.Size);
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
					if (e.Button == System.Windows.Forms.MouseButtons.Right)
					{
						Internal.Windows.Methods.ReleaseCapture();
						Internal.Windows.Methods.SendMessage(this.Handle, Internal.Windows.Constants.WindowMessage.WM_NCRBUTTONDOWN, Internal.Windows.Constants.HTCAPTION, 0);
					}
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (IsUsingThemeWindowBorder())
			{
				Point pt = PointToScreen(e.Location);
				if (pt.X >= this.Left && pt.Y >= this.Top && pt.X <= this.Right && pt.Y <= this.Top + 32)
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						// thanks http://webspace.webring.com/people/lp/practicalvb/vb/input/dragform.html
						Internal.Windows.Methods.ReleaseCapture();
						Internal.Windows.Methods.SendMessage(this.Handle, Internal.Windows.Constants.WindowMessage.WM_NCLBUTTONDOWN, Internal.Windows.Constants.HTCAPTION, 0);
					}
				}
			}
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (IsUsingThemeWindowBorder())
			{
				Point pt = PointToScreen(e.Location);
				if (pt.X >= this.Left && pt.Y >= this.Top && pt.X <= this.Right && pt.Y <= this.Top + 32)
				{
					Internal.Windows.Methods.ReleaseCapture();
					Internal.Windows.Methods.SendMessage(this.Handle, Internal.Windows.Constants.WindowMessage.WM_NCLBUTTONDBLCLK, Internal.Windows.Constants.HTCAPTION, 0);
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (IsUsingThemeWindowBorder())
			{
				Point pt = PointToScreen(e.Location);
				if (pt.X >= this.Left && pt.Y >= this.Top && pt.X <= this.Right && pt.Y <= this.Top + 32)
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Right)
					{
						Internal.Windows.Methods.ReleaseCapture();
						Internal.Windows.Methods.SendMessage(this.Handle, Internal.Windows.Constants.WindowMessage.WM_NCRBUTTONUP, Internal.Windows.Constants.HTCAPTION, 0);
					}
				}
			}
		}

		private bool IsUsingThemeWindowBorder()
		{
			return ThemeWindowBorderEnabled && mvarUseThemeWindowBorder && Theming.Theme.CurrentTheme.HasCustomToplevelWindowFrame;
		}

		private bool IsRectangleMaximized(System.Drawing.Rectangle rect)
		{
			foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
			{
				if (rect.X < screen.WorkingArea.X && rect.Y < screen.WorkingArea.Y && rect.Width > screen.WorkingArea.Width && rect.Height > screen.WorkingArea.Height) return true;
			}
			return false;
		}
		private bool IsRectangleMinimized(System.Drawing.Rectangle rect)
		{
			// TODO: don't hardcode this!!!
			return (rect.X == -32000 && rect.Y == -32000);
		}

		private int wasRectangleMinimized = 0;

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
								int customBorderWidth = 1;
								if (wasRectangleMinimized == 1)
								{
									wasRectangleMinimized = 2;
									return;
								}
								else if (wasRectangleMinimized == 2)
								{
									wasRectangleMinimized = 0;
									return;
								}

								System.Drawing.Rectangle rect = (System.Drawing.Rectangle)System.Runtime.InteropServices.Marshal.PtrToStructure(m.LParam, typeof(System.Drawing.Rectangle));

								if (IsRectangleMinimized(rect))
								{
									wasRectangleMinimized = 1;
									return;
								}

								if (!IsRectangleMaximized(rect))
								{
									switch (FormBorderStyle)
									{
										case System.Windows.Forms.FormBorderStyle.Sizable:
										case System.Windows.Forms.FormBorderStyle.SizableToolWindow:
										{
											rect.X -= SystemInformation.VerticalResizeBorderThickness;
											rect.Width += SystemInformation.VerticalResizeBorderThickness;
											rect.Height += SystemInformation.HorizontalResizeBorderThickness;
											rect.Y -= SystemInformation.HorizontalResizeBorderThickness;
											break;
										}
										case System.Windows.Forms.FormBorderStyle.Fixed3D:
										{
											// TODO: is +1 a magic constant we shouldn't use?
											rect.X -= (SystemInformation.Border3DSize.Width + 3);
											rect.Width += (SystemInformation.Border3DSize.Width + 3);
											rect.Height += (SystemInformation.Border3DSize.Height + 3);
											rect.Y -= (SystemInformation.Border3DSize.Height + 3);
											break;
										}
										case System.Windows.Forms.FormBorderStyle.FixedToolWindow:
										case System.Windows.Forms.FormBorderStyle.FixedDialog:
										case System.Windows.Forms.FormBorderStyle.FixedSingle:
										{
											// TODO: is +1 a magic constant we shouldn't use?
											rect.X -= (SystemInformation.Border3DSize.Width + 1);
											rect.Width += (SystemInformation.Border3DSize.Width + 1);
											rect.Height += (SystemInformation.Border3DSize.Height + 1);
											rect.Y -= (SystemInformation.Border3DSize.Height + 1);
											break;
										}
									}
								}

								switch (FormBorderStyle)
								{
									case System.Windows.Forms.FormBorderStyle.Fixed3D:
									case System.Windows.Forms.FormBorderStyle.FixedDialog:
									case System.Windows.Forms.FormBorderStyle.FixedSingle:
									case System.Windows.Forms.FormBorderStyle.Sizable:
									{
										rect.Y -= SystemInformation.CaptionHeight;
										break;
									}
									case System.Windows.Forms.FormBorderStyle.FixedToolWindow:
									case System.Windows.Forms.FormBorderStyle.SizableToolWindow:
									{
										rect.Y -= SystemInformation.ToolWindowCaptionHeight;
										break;
									}
								}

								int customTitleBarHeight = 32;
								if (FormBorderStyle == System.Windows.Forms.FormBorderStyle.SizableToolWindow || FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
								{
									int twch = SystemInformation.ToolWindowCaptionHeight;
									int ch = SystemInformation.CaptionHeight;

									customTitleBarHeight = (int)(((double)twch / (double)ch) * customTitleBarHeight);
								}
								this.Padding = new System.Windows.Forms.Padding(customBorderWidth, customTitleBarHeight, customBorderWidth, customBorderWidth);

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

	public enum RefreshDropShadowWindowsFlags
	{
		None = 0,
		Activation = 1,
		Position = 2,
		Size = 4,
		All = Activation | Position | Size
	}
}
