using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TestProject
{
	public partial class TimelineTest : Form
	{
		public TimelineTest()
		{
			InitializeComponent();


			tc.Groups.Add("tgChauvIntimSpotL250R", "CHAUVET Intimidator Spot LED 250 Right", true,
				new Timeline.TimelineTrack("trkPanCoarse", "Pan Coarse", null, 24),
				new Timeline.TimelineTrack("trkTiltCoarse", "Tilt Coarse", null, 24),
				new Timeline.TimelineTrack("trkColorWheel", "Color Wheel", null, 24),
				new Timeline.TimelineTrack("trkGoboWheel", "Gobo Wheel", null, 24),
				new Timeline.TimelineTrack("trkShutter", "Shutter", null, 24)
			);
			tc.Groups[0].Tracks[2].ShowGridLines = true;
			tc.Groups[0].Tracks[3].ShowGridLines = true;
			tc.Groups[0].Tracks[4].ShowGridLines = true;

			tc.Tracks.Add(new Timeline.TimelineTrack("trkAudio", "Audio Track", null, 24));
			tc.Tracks[0].Entries.Add("Please Don't Go.wav", 0, 300, Color.CadetBlue);
			tc.Tracks[0].Entries[0].AllowSize = false;

			tc.Tracks.Add(new Timeline.TimelineTrack("trkVideo", "Video Track", null, 64));

			tc.Tracks[1].Entries.Add("keyframe 1", 0, 96);
			tc.Tracks[1].Entries[0].AllowMove = false;

			tc.Tracks[1].Entries.Add("kf2", 100, 32, Color.Red);
		}
	}
}
