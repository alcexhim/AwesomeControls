using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.ActionBar
{
	public abstract class ActionBarItem : IComparable<ActionBarItem>
	{
		public class ActionBarItemCollection
			: List<ActionBarItem>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		private int mvarDisplayIndex = -1;
		public int DisplayIndex { get { return mvarDisplayIndex; } set { mvarDisplayIndex = value; } }

		private System.Drawing.Font mvarFont = null;
		public System.Drawing.Font Font { get { return mvarFont; } set { mvarFont = value; } }

		private ControlState mvarState = ControlState.Normal;
		public ControlState State { get { return mvarState; } set { mvarState = value; } }

		private System.Drawing.ContentAlignment mvarAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		public System.Drawing.ContentAlignment Alignment { get { return mvarAlignment; } set { mvarAlignment = value; } }

		int IComparable<ActionBarItem>.CompareTo(ActionBarItem other)
		{
			if (mvarAlignment == other.Alignment)
			{
				if (mvarDisplayIndex == other.DisplayIndex)
				{
					return 0;
				}
				else
				{
					return mvarDisplayIndex.CompareTo(other.DisplayIndex);
				}
			}
			else
			{
				if (mvarAlignment == System.Drawing.ContentAlignment.BottomLeft || mvarAlignment == System.Drawing.ContentAlignment.MiddleLeft || mvarAlignment == System.Drawing.ContentAlignment.TopLeft)
				{
					if (other.Alignment == System.Drawing.ContentAlignment.BottomCenter || other.Alignment == System.Drawing.ContentAlignment.MiddleCenter || other.Alignment == System.Drawing.ContentAlignment.TopCenter
						|| other.Alignment == System.Drawing.ContentAlignment.BottomRight || other.Alignment == System.Drawing.ContentAlignment.MiddleRight || other.Alignment == System.Drawing.ContentAlignment.TopRight)
					{
						return -1;
					}
					else
					{
						return 1;
					}
				}
				else if (mvarAlignment == System.Drawing.ContentAlignment.BottomCenter || mvarAlignment == System.Drawing.ContentAlignment.MiddleCenter || mvarAlignment == System.Drawing.ContentAlignment.TopCenter)
				{
					if (other.Alignment == System.Drawing.ContentAlignment.BottomRight || other.Alignment == System.Drawing.ContentAlignment.MiddleRight || other.Alignment == System.Drawing.ContentAlignment.TopRight)
					{
						return -1;
					}
					else
					{
						return 1;
					}
				}
				else if (mvarAlignment == System.Drawing.ContentAlignment.BottomRight || mvarAlignment == System.Drawing.ContentAlignment.MiddleRight || mvarAlignment == System.Drawing.ContentAlignment.TopRight)
				{
					return 1;
				}
			}
			return 0;
		}
	}

	public class ActionBarLabel : ActionBarItem
	{
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private string mvarToolTipText = String.Empty;
		public string ToolTipText { get { return mvarToolTipText; } set { mvarToolTipText = value; } }
	}
	public class ActionBarButton : ActionBarLabel
	{
		public event EventHandler Click;
		protected internal virtual void OnClick(EventArgs e)
		{
			if (Click != null) Click(this, e);
		}
	}
}
