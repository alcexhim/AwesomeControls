using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls
{
	public static class ExtensionMethods
	{
		private struct TIPTIMERDATA
		{
			public System.Windows.Forms.ToolTip tip;
			public string text;
			public System.Windows.Forms.Control parent;
			public int x;
			public int y;
			public int duration;
			public int delay;
		}

		public static void Show(this System.Windows.Forms.ToolTip tip, string text, System.Windows.Forms.Control parent, int x, int y, int duration, int delay)
		{
			System.Threading.Thread tipTimerThread = new System.Threading.Thread(tipTimerThread_ParameterizedThreadStart);

			TIPTIMERDATA ttd = new TIPTIMERDATA();
			ttd.delay = delay;
			ttd.duration = duration;
			ttd.parent = parent;
			ttd.text = text;
			ttd.tip = tip;
			ttd.x = x;
			ttd.y = y;
			tipTimerThread.Start(ttd);
		}

		private static void tipTimerThread_ParameterizedThreadStart(object param)
		{
			try
			{
				TIPTIMERDATA ttd = ((TIPTIMERDATA)param);
				System.Threading.Thread.Sleep(ttd.delay);
				if (ttd.parent.InvokeRequired)
				{
					ttd.parent.Invoke(new Action<TIPTIMERDATA>(ttd_parent_invoke), ttd);
				}
				else
				{
					if (ttd.duration > 0)
					{
						ttd.tip.Show(ttd.text, ttd.parent, ttd.x, ttd.y, ttd.duration);
					}
					else
					{
						ttd.tip.Show(ttd.text, ttd.parent, ttd.x, ttd.y);
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
		}

		private static void ttd_parent_invoke(TIPTIMERDATA ttd)
		{
			if (ttd.duration > 0)
			{
				ttd.tip.Show(ttd.text, ttd.parent, ttd.x, ttd.y, ttd.duration);
			}
			else
			{
				ttd.tip.Show(ttd.text, ttd.parent, ttd.x, ttd.y);
			}
		}

        public static System.Drawing.Bitmap Resize(this System.Drawing.Bitmap original, int width, int height)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            graphics.DrawImage(original, new System.Drawing.Rectangle(0, 0, width, height));
            graphics.Flush();

            return bitmap;
        }
	}
}