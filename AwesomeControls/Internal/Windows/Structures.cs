﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AwesomeControls.Internal.Windows
{
	public static class Structures
	{
		/// <summary>
		/// RECT structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		/// <summary>
		/// Base notify message header
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}
		/// <summary>
		/// Standard notify message header
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NMHEADER
		{
			public NMHDR hdr;
			public int iItem;
			public int iButton;
			public IntPtr pitem;
		}
		/// <summary>
		/// Custom draw notify message
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NMCUSTOMDRAW
		{
			public NMHDR hdr;
			public int dwDrawStage;
			public IntPtr hdc;
			public RECT rc;
			public int dwItemSpec;
			public int uItemState;
			public IntPtr lItemlParam;
		}
		/// <summary>
		/// ListView specialized custom draw message 
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NMLVCUSTOMDRAW
		{
			public NMCUSTOMDRAW nmcd;
			public int clrText;
			public int clrTextBk;
			public int iSubItem;
			public uint dwItemType;
			public int clrFace;
			public int iIconEffect;
			public int iIconPhase;
			public int iPartId;
			public int iStateId;
			public RECT rcText;
			public uint uAlign;
		}
		/// <summary>
		/// ListView item data
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct LVITEM
		{
			public Constants.ListViewItemMask mask;
			public int iItem;
			public int iSubItem;
			public uint state;
			public uint stateMask;
			public String pszText;
			public int cchTextMax;
			public int iImage;
			public int lParam;
			public int iIndent;
		}


		/// <summary>
		/// Header item data
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct HDITEM
		{
			public Constants.ListViewHeaderMask mask;
			public int cxy;
			public String pszText;
			public IntPtr hbm;
			public int cchTextMax;
			public Constants.ListViewHeaderFormat fmt;
			public int lParam;
			public int iImage;
			public int iOrder;
			public uint type;
			public IntPtr pvFilter;
		}


		/// <summary>
		/// Header hittest information
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct HDHITTESTINFO
		{
			public long x;
			public long y;
			public uint flags;
			public int iItem;
		}


		/// <summary>
		/// Structure for header layout
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct HDLAYOUT
		{
			public IntPtr prc;   // RECT*
			public IntPtr pwpos; // WINDOWPOS*
		}


		/// <summary>
		/// Header filter text data
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct HDTEXTFILTER
		{
			public String pszText;
			public int cchTextMax;
		}


		/// <summary>
		/// Window position structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public uint flags;
		}

		public struct BLENDFUNCTION
		{
			public Constants.BlendFunctionOperation BlendOp;
			public Constants.BlendFunctionFlags BlendFlags;
			public byte SourceConstantAlpha;
			public Constants.BlendFunctionAlphaFormat AlphaFormat;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

			public static implicit operator System.Drawing.Point(POINT p)
			{
				return new System.Drawing.Point(p.X, p.Y);
			}

			public static implicit operator POINT(System.Drawing.Point p)
			{
				return new POINT(p.X, p.Y);
			}
		}
		/// <summary>
		/// Specifies the width and height of a rectangle.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct SIZE
		{
			public int cx;
			public int cy;

			public SIZE(int cx, int cy)
			{
				this.cx = cx;
				this.cy = cy;
			}
		}
	}
}
