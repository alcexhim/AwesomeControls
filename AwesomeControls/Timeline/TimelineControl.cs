using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Timeline
{
	public partial class TimelineControl : UserControl
	{
		public TimelineControl()
		{
			InitializeComponent();
			base.DoubleBuffered = true;
		}

		private ContextMenuStrip mvarGroupContextMenuStrip = null;
		public ContextMenuStrip GroupContextMenuStrip { get { return mvarGroupContextMenuStrip; } set { mvarGroupContextMenuStrip = value; } }

		private ContextMenuStrip mvarEntryContextMenuStrip = null;
		public ContextMenuStrip EntryContextMenuStrip { get { return mvarEntryContextMenuStrip; } set { mvarEntryContextMenuStrip = value; } }

		private TimelineTrack.TimelineTrackCollection mvarTracks = new TimelineTrack.TimelineTrackCollection();
		public TimelineTrack.TimelineTrackCollection Tracks { get { return mvarTracks; } }

		private TimelineGroup.TimelineGroupCollection mvarGroups = new TimelineGroup.TimelineGroupCollection();
		public TimelineGroup.TimelineGroupCollection Groups { get { return mvarGroups; } }

		private Dictionary<TimelineEntry, Rectangle> entryBounds = null;
		private void RecalculateBounds()
		{
			if (entryBounds == null) entryBounds = new Dictionary<TimelineEntry, Rectangle>();
			entryBounds.Clear();

			Rectangle rect = new Rectangle(0, 0, base.Width - 2, base.Height - 1);

			Rectangle rectGroup = new Rectangle(2, 20, base.Width - 17 - 4, 64);
			foreach (TimelineTrack trk in mvarTracks)
			{
				rectGroup.Height = trk.Height;

				Rectangle rectText = rectGroup;
				rectText.Offset(4, 4);
				rectText.Width = 96;

				Rectangle rectInnerBox = new Rectangle(rectText.Right + 4, rectGroup.Top + 2, 0, rectGroup.Height - 4);
				foreach (TimelineEntry entry in trk.Entries)
				{
					rectInnerBox.X = entry.Start + (rectText.Right + 4);
					rectInnerBox.Width = entry.Length;
					entryBounds.Add(entry, rectInnerBox);
				}
				rectGroup.Y += rectGroup.Height;
			}
		}

		private Rectangle GetEntryBounds(TimelineEntry entry)
		{
			if (entryBounds == null) RecalculateBounds();
			return entryBounds[entry];
		}

		private TimelineTrack HitTestTrack(Point point)
		{
			return null;
		}
		private TimelineEntry HitTestEntry(Point point)
		{
			foreach (TimelineTrack grp in mvarTracks)
			{
				foreach (TimelineEntry entry in grp.Entries)
				{
					Rectangle rectInnerBox = GetEntryBounds(entry);
					if (point.X >= rectInnerBox.Left && point.X <= rectInnerBox.Right && point.Y >= rectInnerBox.Y && point.Y <= rectInnerBox.Bottom) return entry;
				}
			}
			return null;
		}

		private int mvarEntryQuantization = 8;
		public int EntryQuantization { get { return mvarEntryQuantization; } set { mvarEntryQuantization = value; } }

		private int mvarFrameWidth = 8;
		public int FrameWidth { get { return mvarFrameWidth; } set { mvarFrameWidth = value; } }

		private Rectangle rectMoving = new Rectangle();
		private TimelineEntry entryMoving = null;
		private int cx = 0;
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
		}

		private int mvarCurrentFrameIndex = 0;
		private bool mvarDraggingThumb = false;
		private bool mvarDraggingEntry = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Y < 18 && e.X > 104)
			{
				int ci = (int)(((double)(e.X - 104) + 1) / mvarFrameWidth) + 1;
				mvarCurrentFrameIndex = ci;
				mvarDraggingThumb = true;
				OnCurrentFrameChanged(EventArgs.Empty);
			}

			TimelineEntry entry = HitTestEntry(e.Location);
			if (entry != null)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left || !mvarSelectedEntries.Contains(entry))
				{
					// Only change the selection if we are clicking with the left mouse
					// button, or if we click on an item that is not in the selection with
					// the right mouse button
					if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
					{
						mvarSelectedEntries.Clear();
						mvarSelectedEntries.Add(entry);

						if (entry.AllowMove)
						{
							mvarDraggingEntry = true;
							cx = e.X - (entry.Start + 104);
						}
					}
					else
					{
						if (mvarSelectedEntries.Contains(entry))
						{
							mvarSelectedEntries.Remove(entry);
						}
						else
						{
							mvarSelectedEntries.Add(entry);
						}
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					// popup timeline entry context menu
					if (mvarEntryContextMenuStrip != null)
					{
						mvarEntryContextMenuStrip.Show(this, e.Location);
					}
					else
					{
						mnuContextEntry.Show(this, e.Location);
					}
				}
				Refresh();
			}
			else
			{
				mvarSelectedEntries.Clear();
				Refresh();

				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					// popup timeline context menu
				}
			}

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TimelineTrack grp = HitTestTrack(e.Location);
				if (grp != null)
				{
					// popup timeline group context menu
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Button == System.Windows.Forms.MouseButtons.Left && mvarDraggingThumb && e.X > 104)
			{
				int ci = (int)(((double)(e.X - 104) + 1) / mvarFrameWidth) + 1;
				mvarCurrentFrameIndex = ci;
				Refresh();

				OnCurrentFrameChanged(EventArgs.Empty);
				return;
			}

			TimelineEntry entry = HitTestEntry(e.Location);
			if ((entry != null && entry.AllowSize) && 
				((e.X - 104 > entry.Start && e.X - 104 < entry.Start + 4) ||
				(e.X - 104 > (entry.Start + entry.Length - 4) && e.X - 104 < (entry.Start + entry.Length)))
			)
			{
				Cursor = Cursors.SizeWE;
			}
			else if ((entry != null && entry.AllowMove) || entryMoving != null)
			{
				Cursor = Cursors.SizeAll;
			}
			else
			{
				Cursor = Cursors.Default;
			}

			// TODO: inhibit movement when bumping up against another
			// TimelineEntry, make it jump past that TimelineEntry

			if (e.Button == System.Windows.Forms.MouseButtons.Left && mvarDraggingEntry)
			{
				if (entryMoving == null && entry != null)
				{
					if (entry.AllowMove)
					{
						rectMoving = GetEntryBounds(entry);
						rectMoving.Y += 2;
						entryMoving = entry;
					}
				}
				else if (entryMoving != null)
				{
					rectMoving.X = e.X - cx;

					if (mvarEntryQuantization > 0)
					{
						if (rectMoving.X % mvarEntryQuantization != 0) rectMoving.X -= (rectMoving.X % mvarEntryQuantization);
					}
					if (rectMoving.X < 104) rectMoving.X = 104;
					Refresh();
				}
			}
			else
			{
				entryMoving = null;
			}
		}

		public event EventHandler CurrentFrameChanged;
		protected virtual void OnCurrentFrameChanged(EventArgs e)
		{
			if (CurrentFrameChanged != null) CurrentFrameChanged(this, e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (entryMoving != null)
			{
				entryMoving.Start = rectMoving.X - 104;
				RecalculateBounds();
			}

			entryMoving = null;
			mvarDraggingThumb = false;
			mvarDraggingEntry = false;
			Refresh();
		}

		private TimelineEntry.TimelineEntryCollection mvarSelectedEntries = new TimelineEntry.TimelineEntryCollection(false);
		public TimelineEntry.TimelineEntryCollection SelectedEntries { get { return mvarSelectedEntries; } }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = new Rectangle(0, 0, base.Width - 2, base.Height - 1);

			Rectangle rectRuler = new Rectangle(102, 18, base.Width - 96, 2);
			for (int i = rectRuler.X; i < rectRuler.Right; i += mvarFrameWidth)
			{
				int j = (int)(((double)(i - rectRuler.X) / mvarFrameWidth) + 1);
				if (j == 1 || (j % 5 == 0))
				{
					Rectangle rect1 = new Rectangle(i - 4, 0, 16, 16);
					TextRenderer.DrawText(e.Graphics, j.ToString(), new Font(base.Font.FontFamily, 6, FontStyle.Regular), rect1, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				}
				e.Graphics.DrawLine(DrawingTools.Pens.DarkShadowPen, i, rectRuler.Top, i, rectRuler.Bottom);
			}

			Rectangle rectGroup = new Rectangle(2, 2 + 20, base.Width - 17 - 4, mvarTrackHeight);
			foreach (TimelineTrack trk in mvarTracks)
			{
				rectGroup.Height = trk.Height;
				DrawingTools.DrawRaisedBorderMini(e.Graphics, rectGroup);

				Rectangle rectText = rectGroup;
				rectText.Offset(4, 4);
				rectText.Width = 96;

				TextRenderer.DrawText(e.Graphics, trk.Text, new Font(base.Font, FontStyle.Bold), rectText, Color.Black, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);

				DrawingTools.DrawRaisedLineMini(e.Graphics, rectText.Right, rectText.Y - 4, rectText.Height - 2, Orientation.Vertical);

				if (trk.ShowGridLines)
				{
					rectRuler.Y = rectGroup.Y;
					rectRuler.Height = rectGroup.Height;
					for (int i = rectRuler.X; i < rectRuler.Right; i += mvarFrameWidth)
					{
						int j = (int)(((double)(i - rectRuler.X) / mvarFrameWidth) + 1);
						if (j == 1 || (j % 5 == 0))
						{
							Rectangle rect1 = new Rectangle(i - 4, 0, 16, 16);
							TextRenderer.DrawText(e.Graphics, j.ToString(), new Font(base.Font.FontFamily, 6, FontStyle.Regular), rect1, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
						}
						e.Graphics.DrawLine(DrawingTools.Pens.DarkShadowPen, i, rectRuler.Top, i, rectRuler.Bottom);
					}
				}

				Rectangle rectInnerBox = new Rectangle(rectText.Right + 4, rectGroup.Top + 2, 0, rectGroup.Height - 4);
				foreach (TimelineEntry entry in trk.Entries)
				{
					rectInnerBox.X = entry.Start + (rectText.Right + 4);
					rectInnerBox.Width = entry.Length;
					rectInnerBox.Width -= 2;
					rectInnerBox.Height -= 2;

					Color foreColor = Color.Black;
					if (mvarSelectedEntries.Contains(entry))
					{
						// DrawingTools.HSLColor hsl = new DrawingTools.HSLColor(entry.BackColor.R, entry.BackColor.G, entry.BackColor.B);
						// hsl.Luminosity /= 2;
						// e.Graphics.FillRectangle(new SolidBrush(hsl), rectInnerBox);
						e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), rectInnerBox);
						foreColor = Color.FromKnownColor(KnownColor.HighlightText);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(entry.BackColor), rectInnerBox);
					}
					rectInnerBox.Width += 2;
					rectInnerBox.Height += 2;
					DrawingTools.DrawRaisedBorder(e.Graphics, rectInnerBox);

					Rectangle rectText2 = rectInnerBox;
					rectText2.Offset(4, 4);
					rectText2.Width -= 10;
					TextRenderer.DrawText(e.Graphics, entry.Text, base.Font, rectText2, foreColor, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);
				}

				rectGroup.Y += rectGroup.Height;
			}

			foreach (TimelineGroup grp in mvarGroups)
			{
				Rectangle rectGrpText = rectGroup;
				rectGrpText.Offset(4, 4);
				TextRenderer.DrawText(e.Graphics, grp.Title, new Font(base.Font, FontStyle.Bold), rectGrpText, Color.Black, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);

				rectGroup.Y += 24;
				if (grp.Expanded)
				{
					foreach (TimelineTrack trk in grp.Tracks)
					{
						rectGroup.Height = trk.Height;
						DrawingTools.DrawRaisedBorderMini(e.Graphics, rectGroup);

						Rectangle rectText = rectGroup;
						rectText.Offset(4, 4);
						rectText.Width = 96;

						TextRenderer.DrawText(e.Graphics, trk.Text, new Font(base.Font, FontStyle.Bold), rectText, Color.Black, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);

						DrawingTools.DrawRaisedLineMini(e.Graphics, rectText.Right, rectText.Y - 4, rectText.Height - 2, Orientation.Vertical);

						if (trk.ShowGridLines)
						{
							rectRuler.Y = rectGroup.Y;
							rectRuler.Height = rectGroup.Height - 2;
							for (int i = rectRuler.X + mvarFrameWidth; i < rectRuler.Right; i += mvarFrameWidth)
							{
								int j = (int)(((double)(i - rectRuler.X) / mvarFrameWidth) + 1);
								if (j == 1 || (j % 5 == 0))
								{
									Rectangle rect1 = new Rectangle(i - 4, 0, 16, 16);
								}
								e.Graphics.DrawLine(DrawingTools.Pens.LightShadowPen, i, rectRuler.Top, i, rectRuler.Bottom);
							}
						}

						Rectangle rectInnerBox = new Rectangle(rectText.Right + 4, rectGroup.Top + 2, 0, rectGroup.Height - 4);
						foreach (TimelineEntry entry in trk.Entries)
						{
							rectInnerBox.X = entry.Start + (rectText.Right + 4);
							rectInnerBox.Width = entry.Length;
							rectInnerBox.Width -= 2;
							rectInnerBox.Height -= 2;

							Color foreColor = Color.Black;
							if (mvarSelectedEntries.Contains(entry))
							{
								// DrawingTools.HSLColor hsl = new DrawingTools.HSLColor(entry.BackColor.R, entry.BackColor.G, entry.BackColor.B);
								// hsl.Luminosity /= 2;
								// e.Graphics.FillRectangle(new SolidBrush(hsl), rectInnerBox);
								e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), rectInnerBox);
								foreColor = Color.FromKnownColor(KnownColor.HighlightText);
							}
							else
							{
								e.Graphics.FillRectangle(new SolidBrush(entry.BackColor), rectInnerBox);
							}
							rectInnerBox.Width += 2;
							rectInnerBox.Height += 2;
							DrawingTools.DrawRaisedBorder(e.Graphics, rectInnerBox);

							Rectangle rectText2 = rectInnerBox;
							rectText2.Offset(4, 4);
							rectText2.Width -= 10;
							TextRenderer.DrawText(e.Graphics, entry.Text, base.Font, rectText2, foreColor, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);
						}

						rectGroup.Y += rectGroup.Height;
					}
				}
			}

			#region Ruler thumb and current line
			Rectangle rectRulerThumb = new Rectangle(102 + (mvarCurrentFrameIndex - 1) * mvarFrameWidth, 2, mvarFrameWidth, 18);
			e.Graphics.DrawRectangle(new Pen(Color.Red), rectRulerThumb);
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), rectRulerThumb);
			e.Graphics.DrawLine(new Pen(Color.Red), rectRulerThumb.X + (rectRulerThumb.Width / 2), rectRulerThumb.Bottom, rectRulerThumb.X + (rectRulerThumb.Width / 2), this.Height);
			#endregion

			DrawingTools.DrawSunkenBorder(e.Graphics, rect);

			if (entryMoving != null)
			{
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 0, 0, 0), 2), rectMoving);
			}
		}

		private void mnuContextEntry_Opening(object sender, CancelEventArgs e)
		{
			mnuContextEntryProperties.Enabled = (mvarSelectedEntries.Count == 1);
		}

		private void mnuContextEntryDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you wish to delete the selected timeline entry?", "Delete Timeline Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

			foreach (TimelineEntry entry in mvarSelectedEntries)
			{
				if (entry.Parent != null) entry.Parent.Entries.Remove(entry);
			}
			mvarSelectedEntries.Clear();
			Refresh();
		}

		private int mvarTrackHeight = 64;
		public int TrackHeight
		{
			get { return mvarTrackHeight; }
			set
			{
				mvarTrackHeight = value;
				foreach (TimelineTrack track in mvarTracks)
				{
					track.Height = value;
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Refresh();
		}
	}
}
