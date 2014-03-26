/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 6/8/2013
 * Time: 12:16 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace AwesomeControls.ScrollBar
{
	/// <summary>
	/// Description of ScrollBarControl.
	/// </summary>
	public class ScrollBarControl : System.Windows.Forms.Control
	{
		private double mvarMinimum = 0;
		public double Minimum { get { return mvarMinimum; } set { mvarMinimum = value; } }
		
		private double mvarMaximum = 100;
		public double Maximum { get { return mvarMaximum; } set { mvarMaximum = value; } }
		
		private double mvarValue = 0;
		public double Value { get { return mvarValue; } set { mvarValue = value; } }
		
		private double mvarSmallIncrement = 1;
		public double SmallIncrement { get { return mvarSmallIncrement; } set { mvarSmallIncrement = value; } }
		
		private double mvarLargeIncrement = 10;
		public double LargeIncrement { get { return mvarLargeIncrement; } set { mvarLargeIncrement = value; } }
		
		private string mvarThumbText = "#";
		public string ThumbText { get { return mvarThumbText; } set { mvarThumbText = value; } }
		
		private System.Windows.Forms.Orientation mvarOrientation = System.Windows.Forms.Orientation.Horizontal;
		public System.Windows.Forms.Orientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }
		
		private bool mvarDragging = false;
		private int prevPos = 0;
		
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			Rectangle thumbRect = GetThumbRect();
			if (mvarOrientation == System.Windows.Forms.Orientation.Horizontal)
			{
				if (e.Location.X >= thumbRect.X && e.Location.X <= thumbRect.Right)
				{
					prevPos = thumbRect.X;
					mvarDragging = true;
				}
			}
		}
		private Rectangle mvarLastRect = new Rectangle();
		
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);
			
			if (mvarDragging)
			{
				mvarLastRect = GetThumbRect();
				if (e.Location.X <= buttonSize)
				{
					mvarValue = mvarMinimum;
				}
				else if (e.Location.X >= this.Width - buttonSize)
				{
					mvarValue = mvarMaximum;
				}
				else
				{
					double totalWidth = base.Width - buttonSize - buttonSize;
					double pos = (((e.Location.X - prevPos) - buttonSize) / totalWidth) * (mvarMaximum - mvarMinimum);
					mvarValue = pos;
					if (mvarValue < mvarMinimum)
					{
						mvarValue = mvarMinimum;
					}
					else if (mvarValue > mvarMaximum)
					{
						mvarValue = mvarMaximum;
					}
				}
				
				Rectangle rect1 = mvarLastRect;
				rect1.Width++;
				rect1.Height ++;
				
				Rectangle rect2 = GetThumbRect();
				rect2.Width ++;
				rect2.Height ++;
				Invalidate(rect1);
				Invalidate(rect2);
			}
		}
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);
			mvarDragging = false;
		}
		
		private Rectangle GetThumbRect()
		{
			double pct = (mvarValue / (mvarMaximum - mvarMinimum));
			double pos = (pct * (base.Width - thumbSize)) + buttonSize + 1;
			if (pos >= base.Width - buttonSize - thumbSize)
			{
				pos = base.Width - buttonSize - thumbSize;
			}
			return new Rectangle((int)pos, 0, thumbSize, base.Height - 1);
		}
			
		int buttonSize = 10;
		int thumbSize = 20;
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			
			e.Graphics.Clear(Color.Gray);
			
			if (mvarOrientation == System.Windows.Forms.Orientation.Horizontal)
			{
				Rectangle rectBtnDecrease = new Rectangle(0, 0, buttonSize, base.Height - 1);
				e.Graphics.FillRectangle(Brushes.Silver, rectBtnDecrease);
				e.Graphics.DrawRectangle(Pens.Black, rectBtnDecrease);
				// Theming.Theme.CurrentTheme.DrawScrollBarArrow(Direction.Left, rect);
				
				Rectangle thumbRect = GetThumbRect();
				e.Graphics.FillRectangle(Brushes.Silver, thumbRect);
				e.Graphics.DrawRectangle(Pens.Black, thumbRect);
				
				Rectangle rectBtnIncrease = new Rectangle(base.Width - buttonSize, 0, buttonSize, base.Height - 1);
				e.Graphics.FillRectangle(Brushes.Silver, rectBtnIncrease);
				e.Graphics.DrawRectangle(Pens.Black, rectBtnIncrease);
				// Theming.Theme.CurrentTheme.DrawScrollBarArrow(Direction.Left, rect);
			}
		}
	}
}
