using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Timeline
{
    public class TimelineGroup
    {
        public class TimelineGroupCollection
            : System.Collections.ObjectModel.Collection<TimelineGroup>
        {
            public TimelineGroup Add(string name, string title = null, bool expanded = false, params TimelineTrack[] tracks)
            {
                TimelineGroup grp = new TimelineGroup(name, title, expanded, tracks);
                Add(grp);
                return grp;
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private bool mvarExpanded = false;
        public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; } }

        private TimelineTrack.TimelineTrackCollection mvarTracks = new TimelineTrack.TimelineTrackCollection();
        public TimelineTrack.TimelineTrackCollection Tracks { get { return mvarTracks; } }

        public TimelineGroup(string name, string title = null, bool expanded = false, params TimelineTrack[] tracks)
        {
            if (title == null) title = name;
            mvarName = name;
            mvarTitle = title;
            mvarExpanded = expanded;
            foreach (TimelineTrack track in tracks)
            {
                mvarTracks.Add(track);
            }
        }
    }
}
