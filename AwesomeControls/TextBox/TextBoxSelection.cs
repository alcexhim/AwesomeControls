using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.TextBox
{
	public abstract class TextBoxSelection
	{
		public class TextBoxSelectionCollection
			: System.Collections.ObjectModel.Collection<TextBoxSelection>
		{
			private TextBoxControl _parent = null;
			internal TextBoxSelectionCollection(TextBoxControl parent)
			{
				_parent = parent;
			}

			public TextBoxLinearSelection Add(int start, int length)
			{
                TextBoxLinearSelection sel = new TextBoxLinearSelection(start, length);
                Add(sel);
                return sel;
			}
            public TextBoxRectangularSelection Add(Point start, Point end)
			{
                TextBoxRectangularSelection sel = new TextBoxRectangularSelection(start, end);
				Add(sel);
				return sel;
			}
            public TextBoxRectangularSelection Add(Point start, Size length)
			{
                TextBoxRectangularSelection sel = new TextBoxRectangularSelection(start, length);
				Add(sel);
				return sel;
			}
        }
    }

    public class TextBoxLinearSelection : TextBoxSelection
    {
        private int mvarStart = 0;
        public int Start { get { if (mvarStart < 0) mvarStart = 0; return mvarStart; } set { mvarStart = value; } }

        private int mvarEnd = 0;
        public int End { get { if (mvarEnd < 0) mvarEnd = 0; return mvarEnd; } set { mvarEnd = value; } }

        public int Length
        {
            get { return mvarEnd - mvarStart; }
            set { mvarEnd = mvarStart + value; }
        }

        public TextBoxLinearSelection(int start, int length)
        {
            mvarStart = start;
            Length = length;
        }

        /// <summary>
        /// Determines if the given character index is contained within the selection range.
        /// </summary>
        /// <param name="i">The character index to search for.</param>
        /// <returns>True if the given character index is within the selection range, false otherwise.</returns>
        public bool Contains(int i)
        {
            return (i >= mvarStart && i < mvarEnd);
        }
    }
    public class TextBoxRectangularSelection : TextBoxSelection
    {
        private Point mvarStart = Point.Empty;
        public Point Start { get { return mvarStart; } set { mvarStart = value; } }

        private Point mvarEnd = Point.Empty;
        public Point End { get { return mvarEnd; } set { mvarEnd = value; } }

        public Size Length
        {
            get { return new Size(mvarEnd.X - mvarStart.X, mvarEnd.Y - mvarStart.Y); }
            set { mvarEnd = new Point(mvarStart.X + value.Width, mvarStart.Y + value.Height); }
        }

        public TextBoxRectangularSelection(Point start, Point end)
        {
            mvarStart = start;
            mvarEnd = end;
        }

        public TextBoxRectangularSelection(Point start, Size length)
        {
            mvarStart = start;
            Length = length;
        }
        
        /// <summary>
        /// Determines if the given point is contained within the selection range.
        /// </summary>
        /// <param name="pt">The point to search for.</param>
        /// <returns>True if the given point is within the selection range, false otherwise.</returns>
        public bool Contains(Point pt)
        {
            return Contains(pt.X, pt.Y);
        }
        /// <summary>
        /// Determines if the given point is contained within the selection range.
        /// </summary>
        /// <param name="x">The X-coordinate of the point to search for.</param>
        /// <param name="y">The Y-coordinate of the point to search for.</param>
        /// <returns>True if the given point is within the selection range, false otherwise.</returns>
        public bool Contains(int x, int y)
        {
            return
            (
                ((mvarStart.X < mvarEnd.X) && (x > mvarStart.X && y >= mvarStart.Y && x <= mvarEnd.X && y <= mvarEnd.Y)) ||
                ((mvarStart.X > mvarEnd.X) && (x > mvarEnd.X && y >= mvarEnd.Y && x <= mvarStart.X && y <= mvarStart.Y))
            );
        }
    }
}
