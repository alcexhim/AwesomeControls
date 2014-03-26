using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Ribbon
{
	public class Ribbon : Control
	{
		private ControlState mvarApplicationButtonControlState = ControlState.Normal;

		private Size mvarDefaultSize = new Size(300, 24);
		protected override Size DefaultSize
		{
			get
			{
				return mvarDefaultSize;
			}
		}

		public Ribbon()
		{
			base.Dock = DockStyle.Top;
			mvarTabs = new RibbonTab.RibbonTabCollection(this);
            base.Text = "Ribbon UI";
		}

		private int mvarMinimumTabWidth = 54;
		public int MinimumTabWidth
		{
			get { return mvarMinimumTabWidth; }
			set { mvarMinimumTabWidth = value; }
		}
		private int mvarTabSpacing = 4;
		public int TabSpacing
		{
			get { return mvarTabSpacing; }
			set { mvarTabSpacing = value; }
		}

		private RibbonTab.RibbonTabCollection mvarTabs = null;
		public RibbonTab.RibbonTabCollection Tabs
		{
			get { return mvarTabs; }
			set { mvarTabs = value; }
		}

		private bool mvarIsMinimized = false;
		/// <summary>
		/// Determines whether the Ribbon is minimized.
		/// </summary>
        [Description("Determines whether the Ribbon is minimized.")]
		public bool IsMinimized
		{
			get { return mvarIsMinimized; }
			set
			{
				mvarIsMinimized = value;
				if (mvarIsMinimized)
				{
					this.MinimumSize = new Size(this.MinimumSize.Width, 24);
                    this.MaximumSize = new Size(this.MaximumSize.Width, 24);
				}
				else
                {
                    this.MinimumSize = new Size(this.MinimumSize.Width, 117);
                    this.MaximumSize = new Size(this.MaximumSize.Width, 117);
                    
                    ClosePopupControl();
				}
			}
		}

		private RibbonTab mvarHoverTab = null;
        private RibbonControl mvarHoverControl = null;

		private RibbonTab mvarSelectedTab = null;
		public RibbonTab SelectedTab
		{
			get { return mvarSelectedTab; }
			set
			{
				bool changed = false;
				if (mvarSelectedTab != value) changed = true;

                RibbonTab rt = mvarSelectedTab;
				mvarSelectedTab = value;

                if (rt != mvarSelectedTab)
                {
                    Rectangle rect;
                    if (rt != null)
                    {
                        rect = GetTabButtonBounds(rt);
                        rect.Height += 1;
                        Invalidate(rect);
                    }
                    if (mvarSelectedTab != null)
                    {
                        rect = GetTabButtonBounds(mvarSelectedTab);
                        rect.Height += 1;
                        Invalidate(rect);
                    }
                }

                if (changed)
                {
                    if (!mvarIsMinimized)
                    {
                        Rectangle rect1 = new Rectangle(0, 24, base.Width - 1, base.Height - 1 - 24);
                        Invalidate(rect1);
                    }

                    OnSelectedTabChanged(EventArgs.Empty);

                    if (popupControl == null && mvarIsMinimized && value != null)
                    {
                        OpenPopupControl();
                    }
                }
			}
		}

		private int mvarMinimumParentHeight = 251;
		/// <summary>
		/// The minimum height the parent form needs to be in order to display the Ribbon.
		/// </summary>
		public int MinimumParentHeight
		{
			get { return mvarMinimumParentHeight; }
			set { mvarMinimumParentHeight = value; }
		}

		private Control mvarOldParent = null;
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);

			if (Parent != mvarOldParent && mvarOldParent != null)
			{
				mvarOldParent.SizeChanged -= Parent_SizeChanged;
			}
			if (Parent != null)
			{
				Parent.SizeChanged += Parent_SizeChanged;
			}
		}

		private void Parent_SizeChanged(object sender, EventArgs e)
		{
			if (Parent != null)
			{
				if (Parent.ClientSize.Height < mvarMinimumParentHeight)
				{
					if (Visible) Visible = false;
				}
				else
				{
					if (!Visible) Visible = true;
				}
			}
		}

		#region Events
		public event EventHandler SelectedTabChanged;
		protected virtual void OnSelectedTabChanged(EventArgs eventArgs)
		{
			if (SelectedTabChanged != null)
			{
				SelectedTabChanged(this, eventArgs);
			}
		}
		#endregion

		private Image mvarApplicationButtonImage = null;
		public Image ApplicationButtonImage
		{
			get { return mvarApplicationButtonImage; }
			set { mvarApplicationButtonImage = value; }
		}

		private System.Collections.Generic.Dictionary<RibbonControlGroup, ControlState> mvarActionButtonControlStates = new Dictionary<RibbonControlGroup, ControlState>();

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

            try
            {
                // Tab bar height is 24
                Theming.Theme.CurrentTheme.DrawRibbonTabBar(e.Graphics, new Rectangle(0, 0, base.Width, 24), mvarIsMinimized);
                Theming.Theme.CurrentTheme.DrawRibbonApplicationButton(e.Graphics, new Rectangle(0, 0, 56, 23), mvarApplicationButtonControlState);
                Theming.Theme.CurrentTheme.DrawRibbonApplicationButtonImage(e.Graphics, new Rectangle(14, 5, 15, 12), mvarApplicationButtonImage);

                foreach (RibbonTab tab in mvarTabs)
                {
                    Rectangle bounds = GetTabButtonBounds(tab);
                    if (tab == mvarHoverTab)
                    {
                        ControlState state = ControlState.Hover;
                        if (tab == mvarSelectedTab)
                        {
                            state |= ControlState.Pressed;
                        }
                        Theming.Theme.CurrentTheme.DrawRibbonTab(e.Graphics, bounds, state, tab);
                    }
                    else if (tab == mvarSelectedTab)
                    {
                        Theming.Theme.CurrentTheme.DrawRibbonTab(e.Graphics, bounds, ControlState.Pressed, tab);
                    }
                    else if (!tab.Enabled)
                    {
                        Theming.Theme.CurrentTheme.DrawRibbonTab(e.Graphics, bounds, ControlState.Disabled, tab);
                    }
                    else
                    {
                        Theming.Theme.CurrentTheme.DrawRibbonTab(e.Graphics, bounds, ControlState.Normal, tab);
                    }
                }

                if (!mvarIsMinimized)
                {
                    Theming.Theme.CurrentTheme.DrawRibbonTabPageBackground(e.Graphics, new Rectangle(0, 24, base.Width, base.Height - 1));
                    // e.Graphics.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.RibbonTabBarBorderBottom), 0, base.Height - 1, base.Width - 1, base.Height - 1);

                    if (mvarSelectedTab != null)
                    {
                        Rectangle rect = new Rectangle(6, 24, 140, base.Height - 30);
                        foreach (RibbonControlGroup group in mvarSelectedTab.Groups)
                        {
                            rect = GetControlGroupBounds(group);
                            Theming.Theme.CurrentTheme.DrawRibbonControlGroup(e.Graphics, rect, group);
                            foreach (RibbonControl ctl in group.Controls)
                            {
                                Theming.Theme.CurrentTheme.DrawRibbonControl(e.Graphics, GetControlBounds(ctl), ctl);
                            }

							if (group.ActionButtonVisible)
							{
								Rectangle rectActionButton = new Rectangle(rect.Right - 15, rect.Bottom - 14, 15, 14);

								ControlState state = ControlState.Normal;
								if (mvarActionButtonControlStates.ContainsKey(group))
								{
									state = mvarActionButtonControlStates[group];
								}
								Theming.Theme.CurrentTheme.DrawRibbonControlGroupActionButton(e.Graphics, rectActionButton, state);
							}
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
            }
		}

        private RibbonTab HitTestTabButton(Point pt)
        {
            foreach (RibbonTab rt in mvarTabs)
            {
                Rectangle rect = GetTabButtonBounds(rt);
                if (pt.X >= rect.X && pt.X <= rect.Right && pt.Y >= rect.Y && pt.Y <= rect.Bottom)
                {
                    return rt;
                }
            }
            return null;
        }
        private RibbonControl HitTestControl(Point pt)
        {
            if (mvarSelectedTab == null) return null;
            foreach (RibbonControlGroup grp in mvarSelectedTab.Groups)
            {
                if (grp == null) continue;
                foreach (RibbonControl ctl in grp.Controls)
                {
                    if (ctl == null) continue;

                    Rectangle rect = GetControlBounds(ctl);
                    if (pt.X >= rect.X && pt.X <= rect.Right && pt.Y >= rect.Y && pt.Y <= rect.Bottom)
                    {
                        return ctl;
                    }
                }
            }
            return null;
        }

        private Dictionary<RibbonControl, Rectangle> controlBounds = new Dictionary<RibbonControl, Rectangle>();
        private Rectangle GetControlBounds(RibbonControl ctl)
        {
            if (!controlBounds.ContainsKey(ctl))
            {
                Rectangle bounds = new Rectangle(7, 5 + 24, mvarMinimumTabWidth, 22);
                int textWidth = CreateGraphics().MeasureString(ctl.Text, base.Font).ToSize().Width;
                if (textWidth > bounds.Width) bounds.Width = textWidth;

                if (mvarSelectedTab == null) return Rectangle.Empty;

                foreach (RibbonControlGroup rg in mvarSelectedTab.Groups)
                {
                    foreach (RibbonControl rc in rg.Controls)
                    {
                        if (rc.DisplayStyle == RibbonControlDisplayStyle.ImageAboveText)
                        {
                            bounds.Size = new System.Drawing.Size(42, 66);
                        }
                        else if (rc.DisplayStyle == RibbonControlDisplayStyle.ImageBesideText)
                        {
                            bounds.Size = new Size(52, 22);
                        }
                        controlBounds.Add(rc, bounds);

                        if (rc.DisplayStyle == RibbonControlDisplayStyle.ImageAboveText)
                        {
                            bounds.X += bounds.Width;
                            bounds.Y = 5 + 24;
                        }
                        else if (rc.DisplayStyle == RibbonControlDisplayStyle.ImageBesideText)
                        {
                            bounds.Y += bounds.Height;
                        }
                    }
                }
            }
            return controlBounds[ctl];
        }

        private Dictionary<RibbonTab, Rectangle> tabButtonBounds = new Dictionary<RibbonTab, Rectangle>();
        private Rectangle GetTabButtonBounds(RibbonTab tab)
        {
            if (!tabButtonBounds.ContainsKey(tab))
            {
                int x = 59;
                foreach (RibbonTab rt in mvarTabs)
                {
                    Rectangle bounds = new Rectangle(x, 1, mvarMinimumTabWidth, 22);
                    int textWidth = CreateGraphics().MeasureString(rt.Text, base.Font).ToSize().Width;
                    bounds.Width = (textWidth + 22);

                    if ((textWidth + 22) < mvarMinimumTabWidth) bounds.Width = mvarMinimumTabWidth;

                    tabButtonBounds.Add(rt, bounds);
                    x += (bounds.Width + mvarTabSpacing);
                }
            }
            return tabButtonBounds[tab];
        }

        private Dictionary<RibbonControlGroup, Rectangle> controlGroupBounds = new Dictionary<RibbonControlGroup, Rectangle>();
        private Rectangle GetControlGroupBounds(RibbonControlGroup group)
        {
            if (!controlGroupBounds.ContainsKey(group))
            {
                if (mvarSelectedTab == null) return Rectangle.Empty;

                int x = 6;
                foreach (RibbonControlGroup rg in mvarSelectedTab.Groups)
                {
                    Rectangle bounds = new Rectangle(x, 24 + 4, 0, base.Height - (24 + 4) - 6);
                    foreach (RibbonControl rc in rg.Controls)
                    {
                        Rectangle rcb = GetControlBounds(rc);

                        if (rcb.Right > bounds.Width)
                        {
                            bounds.Width += (rcb.Right - bounds.Width);
                        }
                    }
                    controlGroupBounds.Add(rg, bounds);
                    x += bounds.Width + 6;
                }
            }
            return controlGroupBounds[group];
        }

        private RibbonPopupControl popupControl = null;
        internal void ClosePopupControl()
        {
            if (popupControl == null) return;
            popupControl.Visible = false;
            if (popupControl == null) return;

            Form parentForm = popupControl.FindForm();
            if (parentForm != null)
            {
                parentForm.Controls.Remove(popupControl);
            }
            popupControl = null;

            SelectedTab = null;

            if (parentForm != null)
            {
                foreach (Control ctl in parentForm.Controls)
                {
                    if (ctl != this)
                    {
                        ctl.Focus();
                        break;
                    }
                }
            }
        }
        internal void OpenPopupControl()
        {
            if (popupControl != null) ClosePopupControl();

            popupControl = new RibbonPopupControl(this);
            Form parentForm = FindForm();
            popupControl.Width = parentForm.Width;
            popupControl.Height = 93;
            popupControl.Top = 24;
            popupControl.Visible = true;
            parentForm.Controls.Add(popupControl);
            popupControl.BringToFront();

            inhibitLostFocus = true;
            popupControl.Focus();
            inhibitLostFocus = false;
        }

        private bool inhibitLostFocus = false;
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (!inhibitLostFocus)
            {
                ClosePopupControl();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            RibbonTab httb = HitTestTabButton(e.Location);
            if (httb != null)
            {
                SelectedTab = httb;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!FindForm().ContainsFocus) return;

            Rectangle rect = new Rectangle(0, 0, 56, 23);
            if (rect.Contains(e.Location))
            {
                RibbonTab ts = mvarHoverTab;
                mvarHoverTab = null;
                if (ts != null) Invalidate(GetTabButtonBounds(ts));

                if (mvarApplicationButtonControlState != ControlState.Hover)
                {
                    mvarApplicationButtonControlState = ControlState.Hover;
                    Invalidate(rect);
                }
                return;
            }
            else if (mvarApplicationButtonControlState != ControlState.Normal)
            {
                mvarApplicationButtonControlState = ControlState.Normal;
                Invalidate(rect);
            }

            RibbonTab httb = HitTestTabButton(e.Location);
            if (httb != mvarHoverTab)
            {
                RibbonTab ts = mvarHoverTab;
                mvarHoverTab = httb;
                if (ts != null) Invalidate(GetTabButtonBounds(ts));
                if (httb != null)
                {
                    Invalidate(GetTabButtonBounds(httb));
                }
            }

            RibbonControl rc = HitTestControl(e.Location);
            RibbonControl rch = mvarHoverControl;
            bool changed = (rc != mvarHoverControl);
            mvarHoverControl = rc;

            if (changed)
            {
                if (rch != null)
                {
                    rch.ControlState = ControlState.Normal;
                    Invalidate(GetControlBounds(rch));
                }
                if (rc != null && rc.Enabled)
                {
                    rc.ControlState = ControlState.Hover;
                    Invalidate(GetControlBounds(rc));
                }
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (mvarHoverTab != null)
            {
                RibbonTab ts = mvarHoverTab;
                mvarHoverTab = null;
                if (ts != null) Invalidate(GetTabButtonBounds(ts));
            }

            Rectangle rect = new Rectangle(0, 0, 56, 23);
            if (mvarApplicationButtonControlState != ControlState.Normal)
            {
                mvarApplicationButtonControlState = ControlState.Normal;
                Invalidate(rect);
            }
            if (mvarHoverControl != null)
            {
                Refresh();
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            IsMinimized = !IsMinimized;

            if (IsMinimized)
            {
                RibbonTab ts = mvarSelectedTab;
                mvarSelectedTab = null;

                Rectangle rect;
                if (ts != null)
                {
                    rect = GetTabButtonBounds(ts);
                    rect.Height += 1;
                    Invalidate(rect);
                }
            }
            else
            {
                mvarSelectedTab = HitTestTabButton(e.Location);
            }
        }
	}
}
