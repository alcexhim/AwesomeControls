using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.Timeline
{
	public class TimelineEntry
	{
		public class TimelineEntryCollection
			: System.Collections.ObjectModel.Collection<TimelineEntry>
		{
			private TimelineTrack mvarParent = null;
			private bool mvarUpdateParent = true;
			public TimelineEntryCollection()
			{
				mvarParent = null;
			}
			internal TimelineEntryCollection(bool updateParent)
			{
				mvarUpdateParent = false;
			}
			internal TimelineEntryCollection(TimelineTrack parent)
			{
				mvarParent = parent;
			}

			public TimelineEntry Add(string name, int start, int length)
			{
				return Add(name, name, start, length);
			}
			public TimelineEntry Add(string name, int start, int length, Color backColor)
			{
				return Add(name, name, start, length, backColor);
			}
			public TimelineEntry Add(string name, string text, int length, Color backColor)
			{
				return Add(name, text, 0, length, backColor);
			}
			public TimelineEntry Add(string name, string text, int start, int length)
			{
				return Add(name, text, start, length, Color.White);
			}
			public TimelineEntry Add(string name, string text, int start, int length, Color backColor)
			{
				TimelineEntry entry = new TimelineEntry();
				entry.Name = name;
				entry.Text = text;
				entry.Start = start;
				entry.Length = length;
				entry.BackColor = backColor;
				base.Add(entry);
				return entry;
			}

			protected override void InsertItem(int index, TimelineEntry item)
			{
				if (mvarUpdateParent) item.Parent = mvarParent;
				base.InsertItem(index, item);
			}
			protected override void RemoveItem(int index)
			{
				if (mvarUpdateParent) this[index].Parent = null;
				base.RemoveItem(index);
			}
		}

		private TimelineTrack mvarParent = null;
		public TimelineTrack Parent { get { return mvarParent; } private set { mvarParent = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private Color mvarBackColor = Color.White;
		public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

		private int mvarStart = 0;
		public int Start { get { return mvarStart; } set { mvarStart = value; } }
		private int mvarLength = 0;
		public int Length { get { return mvarLength; } set { mvarLength = value; } }

		private bool mvarAllowMove = true;
		public bool AllowMove { get { return mvarAllowMove; } set { mvarAllowMove = value; } }
		private bool mvarAllowSize = true;
		public bool AllowSize { get { return mvarAllowSize; } set { mvarAllowSize = value; } }
	}
}
