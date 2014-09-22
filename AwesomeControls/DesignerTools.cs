using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AwesomeControls
{
	internal static class DesignerTools
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TEXTMETRIC
		{
			public int tmHeight;
			public int tmAscent;
			public int tmDescent;
			public int tmInternalLeading;
			public int tmExternalLeading;
			public int tmAveCharWidth;
			public int tmMaxCharWidth;
			public int tmWeight;
			public int tmOverhang;
			public int tmDigitizedAspectX;
			public int tmDigitizedAspectY;
			public char tmFirstChar;
			public char tmLastChar;
			public char tmDefaultChar;
			public char tmBreakChar;
			public byte tmItalic;
			public byte tmUnderlined;
			public byte tmStruckOut;
			public byte tmPitchAndFamily;
			public byte tmCharSet;
		}

		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteObject(IntPtr hObject);
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetTextMetrics(IntPtr hdc, TEXTMETRIC tm);

		public const ContentAlignment AnyTopAlignment = (ContentAlignment)7;
		public const ContentAlignment AnyMiddleAlignment = (ContentAlignment)112;

		public static int GetTextBaseline(Graphics graphics, Font font, Rectangle rectangle, ContentAlignment alignment)
		{
			int num = 0;
			int num2 = 0;

			IntPtr hdc = graphics.GetHdc();
			IntPtr handle = font.ToHfont();

			try
			{
				IntPtr handle2 = SelectObject(hdc, handle);
				TEXTMETRIC tEXTMETRIC = new TEXTMETRIC();
				GetTextMetrics(hdc, tEXTMETRIC);
				num = tEXTMETRIC.tmAscent + 1;
				num2 = tEXTMETRIC.tmHeight;
				SelectObject(hdc, handle2);
			}
			finally
			{
				DeleteObject(handle);
				graphics.ReleaseHdc(hdc);
			}

			if ((alignment & AnyTopAlignment) != (ContentAlignment)0)
			{
				return rectangle.Top + num;
			}
			if ((alignment & AnyMiddleAlignment) != (ContentAlignment)0)
			{
				return rectangle.Top + rectangle.Height / 2 - num2 / 2 + num;
			}
			return rectangle.Bottom - num2 + num;
		}
	}
}
