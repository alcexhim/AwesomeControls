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
	public partial class Dialog : Form
	{
		public Dialog()
		{
			InitializeComponent();
			mvarOldFontSize = ClientSize;
			mvarNewFontSize = ClientSize;
		}


		private Size mvarOldFontSize = Size.Empty;
		private Size mvarNewFontSize = Size.Empty;

		private double mvarScaleW = 1.0, mvarScaleH = 1.0;

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			mvarNewFontSize = ClientSize;

			mvarScaleW = (double)mvarNewFontSize.Width / mvarOldFontSize.Width;
			mvarScaleH = (double)mvarNewFontSize.Height / mvarOldFontSize.Height;
		}

		private bool mvarUseThemeBackground = true;
		public bool UseThemeBackground { get { return mvarUseThemeBackground; } set { mvarUseThemeBackground = value; } }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (mvarUseThemeBackground)
			{
				int h = (int)(mvarScaleH * 42);

				Rectangle rect = new Rectangle(0, 0, this.Width, this.Height - h);
				Rectangle rect2 = new Rectangle(0, this.ClientSize.Height - h, this.Width, h);
				e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.DialogBackground);

				Theming.Theme.CurrentTheme.DrawContentAreaBackground(e.Graphics, rect2);
			}
		}
	}
}
