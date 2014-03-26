using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Ribbon
{
    public partial class RibbonForm : Form
    {
        public RibbonForm()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);

        private RibbonQuickAccessToolbar mvarQuickAccessToolbar = new RibbonQuickAccessToolbar();
        public RibbonQuickAccessToolbar QuickAccessToolbar
        {
            get { return mvarQuickAccessToolbar; }
        }
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0085 /* WM_NCPAINT */ && mvarQuickAccessToolbar.ShowInTitlebar)
            {
                if ((System.Environment.OSVersion.Platform == PlatformID.Win32NT)
                    || (System.Environment.OSVersion.Platform == PlatformID.Win32S)
                    || (System.Environment.OSVersion.Platform == PlatformID.Win32Windows)
                    || (System.Environment.OSVersion.Platform == PlatformID.WinCE))
                {
                    IntPtr hdc = GetWindowDC(base.Handle);
                    Graphics g = Graphics.FromHdc(hdc);

                    int height = SystemInformation.CaptionHeight + 3;
                    
                    int x = 30;
                    foreach (RibbonControl ctl in mvarQuickAccessToolbar.Controls)
                    {
                        int width = 80;
                        g.FillRectangle(Brushes.AliceBlue, new Rectangle(x, 1, width, height));
                        x += width + 2;
                    }


                    g.Flush();
                    ReleaseDC(base.Handle, hdc);
                }
            }
        }

        public Ribbon Ribbon
        {
            get { return ribbon1; }
        }
    }
}
