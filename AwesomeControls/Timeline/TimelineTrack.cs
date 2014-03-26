using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.Timeline
{
	public class TimelineTrack
	{
		public class TimelineTrackCollection
			: System.Collections.ObjectModel.Collection<TimelineTrack>
		{
			public TimelineTrack Add(string name, string text = null, Image image = null, int height = 64)
			{
				if (text == null) text = name;

				TimelineTrack grp = new TimelineTrack(name, text, image, height);
				base.Add(grp);
				return grp;
			}
		}

		public TimelineTrack(string name, string text = null, Image image = null, int height = 64)
		{
			if (text == null) text = name;
			mvarName = name;
			mvarText = text;
			mvarImage = image;
			mvarHeight = height;

			mvarEntries = new TimelineEntry.TimelineEntryCollection(this);
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }
		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }
		private int mvarHeight = 64;
		public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

		private TimelineEntry.TimelineEntryCollection mvarEntries = null;
		public TimelineEntry.TimelineEntryCollection Entries { get { return mvarEntries; } }

		private bool mvarShowGridLines = false;
		public bool ShowGridLines { get { return mvarShowGridLines; } set { mvarShowGridLines = value; } }
	}
}
