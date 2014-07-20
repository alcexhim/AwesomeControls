using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
    public partial class PropertyGridPanel : UserControl
    {
        private bool _hasFocus = false;
        private bool _isPopupOpen = false;

        public PropertyGridPanel()
        {
            InitializeComponent();
			txt.BackColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (base.ParentForm != null)
            {
	            base.ParentForm.Activated += delegate(object o, EventArgs e)
	            {
	                if (!_isPopupOpen)
	                {
	                    _hasFocus = true;
	                    pnlProperties.Refresh();
	                }
	            };
	            base.ParentForm.Deactivate += delegate(object o, EventArgs e)
	            {
	                if (!_isPopupOpen)
	                {
	                    _hasFocus = false;
	                    pnlProperties.Refresh();
	                }
	            };
            }
        }

        private PropertyGridView mvarView = PropertyGridView.Unsorted;
        public PropertyGridView View { get { return mvarView; } set { mvarView = value; } }

        private PropertyGroup mvarGroup = null;
        public PropertyGroup Group { get { return mvarGroup; } set { mvarGroup = value; Refresh(); } }

        private int m_previdx = -1;
        private int m_clicked = 0;

        private int mvarMarginWidth = 14;

        public Property SelectedProperty
        {
            get
            {
                if (mvarGroup != null)
                {
                    if (mvarSelectedPropertyIndex > -1 && mvarSelectedPropertyIndex < mvarGroup.Properties.Count)
                    {
                        return mvarGroup.Properties[mvarSelectedPropertyIndex];
                    }
                }
                return null;
            }
        }

        private int mvarItemHeight = 16;
        public int ItemHeight { get { return mvarItemHeight; } set { mvarItemHeight = value; } }

        public override void Refresh()
        {
            base.Refresh();
            if (mvarGroup == null) return;

            if (SelectedProperty != null) txt.Text = SelectedProperty.Value.ToString();
            pnlProperties.Refresh();
            vsc.Minimum = 0;

            RecalculateVisibleProperties();
        }

        private void RecalculateVisibleProperties()
        {
            if (mvarGroup == null) return;
            int visibleProperties = 0, h = 0;
            for (int i = 0; i < mvarGroup.Properties.Count; i++)
            {
                h += CalculatePropertyHeight(mvarGroup.Properties[i]);
                if (h <= pnlProperties.Height)
                {
                    visibleProperties++;
                }
            }
			visibleProperties--;
            vsc.Maximum = (mvarGroup.Properties.Count - visibleProperties) + 7;
        }

        private int CalculatePropertyHeight(Property property)
        {
            int h = 0;
            h++;
            h += mvarItemHeight;
			if (property is GroupProperty)
			{
				if ((property as GroupProperty).Expanded)
				{
					for (int i = 0; i < (property as GroupProperty).Properties.Count; i++)
					{
						h += CalculatePropertyHeight((property as GroupProperty).Properties[i]);
					}
				}
			}
            h++;
            return h;
        }

        private double mvarSplitterPosition = 0.40;
        public double SplitterPosition { get { return mvarSplitterPosition; } set { mvarSplitterPosition = value; } }

        private int mvarSelectedPropertyIndex = -1;
        public int SelectedPropertyIndex { get { return mvarSelectedPropertyIndex; } set { mvarSelectedPropertyIndex = value; m_previdx = value; m_clicked = 2; } }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalculateVisibleProperties();
            Refresh();
        }

        private void pnlProperties_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor);

            DrawingTools.PrepareGraphics(e.Graphics);

			e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(0, 0, mvarMarginWidth, pnlProperties.Height - 1));
            e.Graphics.DrawRectangle(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(0, 0, pnlProperties.Width - 1, pnlProperties.Height - 1));
            
            int leftWidth = (int)(mvarSplitterPosition * pnlProperties.Width) - mvarMarginWidth;
            int rightWidth = pnlProperties.Width - leftWidth;

            if (mvarGroup != null)
            {
                if (mvarView == PropertyGridView.Alphabetical)
                {
                }
                else if (mvarView == PropertyGridView.Categorized)
                {
                }
                else if (mvarView == PropertyGridView.Unsorted)
                {
                    int s = 0;
                    if (vsc.Maximum > 0) s = vsc.Value;
                    for (int i = s; i < mvarGroup.Properties.Count; i++)
                    {
                        Color fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
                        if (mvarGroup.Properties[i].ReadOnly)
                        {
							fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridDisabledForegroundColor;
                        }
                        if (mvarSelectedPropertyIndex == i)
                        {
                            if (_hasFocus)
                            {
								e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridItemHighlightBackgroundColor), new Rectangle(mvarMarginWidth, mvarItemHeight * (i - s), leftWidth - mvarMarginWidth, mvarItemHeight));
                                fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridItemHighlightForegroundColor;
                            }
                            else
                            {
								e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(mvarMarginWidth, mvarItemHeight * (i - s), leftWidth - mvarMarginWidth, mvarItemHeight));
                            }

							PropertyEditor editor = mvarGroup.Properties[i].DataType.Editor;
                            if (editor != null)
                            {
								Rectangle rect = new Rectangle(pnlProperties.Width - editor.ButtonWidth - 1, mvarItemHeight * (i - s), editor.ButtonWidth, mvarItemHeight);
                                e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
								editor.DrawButton(e.Graphics, rect, buttonDown);
                            }
							else if (mvarGroup.Properties[i].DataType.Choices.Count > 0)
							{
								Rectangle rect = new Rectangle(pnlProperties.Width - 16 - 1, mvarItemHeight * (i - s), 16, mvarItemHeight);
								e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
								DrawDropDownArrow(e.Graphics, rect, buttonDown);
							}
                        }
                        e.Graphics.DrawString(mvarGroup.Properties[i].Name, base.Font, new SolidBrush(fc), new Rectangle(mvarMarginWidth, mvarItemHeight * (i - s) + 1, leftWidth - mvarMarginWidth, mvarItemHeight));


                        if (mvarGroup.Properties[i].ReadOnly)
                        {
                            fc = Color.FromKnownColor(KnownColor.GrayText);
                        }
                        else
                        {
                            fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
                        }
                        if (mvarGroup.Properties[i].Value == null)
                        {
                            if (mvarGroup.Properties[i].GetDefaultValue() != null)
                            {
                                TextRenderer.DrawText(e.Graphics, mvarGroup.Properties[i].GetDefaultValueDisplayString(), base.Font, new Rectangle(leftWidth + 2, mvarItemHeight * (i - s) + 1, rightWidth, mvarItemHeight), fc, TextFormatFlags.Left);
                            }
                        }
                        else
                        {
							TextRenderer.DrawText(e.Graphics, mvarGroup.Properties[i].Value.ToString(), base.Font, new Rectangle(leftWidth + 2, mvarItemHeight * (i - s) + 1, rightWidth, mvarItemHeight), fc, TextFormatFlags.Left);
                        }
                        e.Graphics.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), mvarMarginWidth, mvarItemHeight * ((i - s) + 1), pnlProperties.Width - 2, mvarItemHeight * ((i - s) + 1));
                    }
                }
            }
			e.Graphics.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), leftWidth, 1, leftWidth, pnlProperties.Height - 2);
        }

		private void DrawDropDownArrow(Graphics graphics, Rectangle rect, bool buttonDown)
		{
			DrawingTools.DrawArrow(graphics, ArrowDirection.Down, rect.Left + 5, rect.Top + 6, 5);

			if (buttonDown)
			{
				DrawingTools.DrawSunkenBorder(graphics, rect);
			}
			else
			{
				DrawingTools.DrawRaisedBorder(graphics, rect);
			}
		}

        private void vsc_Scroll(object sender, ScrollEventArgs e)
        {
            vsc.Value = e.NewValue;
            pnlProperties.Refresh();
        }

        private bool buttonDown = false;
        private void pnlProperties_MouseDown(object sender, MouseEventArgs e)
        {
            if (mvarGroup == null) return;
            int h = 0, s = 0;
            if (vsc.Maximum > 0) s = vsc.Value;
            for (int i = s; i < mvarGroup.Properties.Count; i++)
            {
                if (e.Y >= h && e.Y <= (h + mvarItemHeight))
                {
                    mvarSelectedPropertyIndex = i;

                    if (mvarSelectedPropertyIndex != m_previdx)
                    {
                        m_clicked = 0;
                        m_previdx = mvarSelectedPropertyIndex;
                    }
                    m_clicked++;

                    int leftWidth = (int)(mvarSplitterPosition * pnlProperties.Width) - mvarMarginWidth;
                    int rightWidth = pnlProperties.Width - leftWidth;

                    txt.Visible = true;
                    txt.Left = leftWidth + 4;
                    txt.Width = rightWidth - 6;

                    txt.Text = mvarGroup.Properties[i].Value.ToString();
                    txt.Top = (mvarItemHeight * (i - s)) + 1;

                    if (mvarGroup.Properties[i].ReadOnly)
                    {
                        txt.ReadOnly = true;
						txt.BackColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor;
						txt.ForeColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridDisabledForegroundColor;
                    }
                    else
                    {
						txt.ReadOnly = false;
						txt.BackColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor;
						txt.ForeColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
                    }

					PropertyEditor editor = mvarGroup.Properties[i].DataType.Editor;
					if (editor != null)
                    {
						txt.Width -= editor.ButtonWidth;
                        if (e.Button == MouseButtons.Left && e.X >= pnlProperties.Width - editor.ButtonWidth)
                        {
                            buttonDown = true;
                        }
                    }
					else if (mvarGroup.Properties[i].DataType.Choices.Count > 0)
					{
						txt.Width -= 16;
						if (e.Button == MouseButtons.Left && e.X >= pnlProperties.Width - 16)
						{
							buttonDown = true;
						}
					}
                    txt.SelectAll();

                    pnlProperties.Refresh();
                    return;
                }
                h += mvarItemHeight;
            }
        }
        private void pnlProperties_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (mvarGroup == null) return;
                int h = 0, s = 0;
                if (vsc.Maximum > 0) s = vsc.Value;
                if (m_clicked > 1)
                {
                    for (int i = s; i < mvarGroup.Properties.Count; i++)
                    {
                        if (e.Y >= h && e.Y <= (h + mvarItemHeight))
                        {
                            if (e.X >= (pnlProperties.Width - 16))
                            {
                                if (!mvarGroup.Properties[i].ReadOnly)
                                {
                                    // Dropdown open
                                    PropertyGridDropDownWindow wnd = new PropertyGridDropDownWindow(this);
                                    wnd.StartPosition = FormStartPosition.Manual;
                                    wnd.Width = txt.Width + 6;
                                    int hhh = 0;
                                    if (mvarGroup.Properties[i].DataType.Choices.Count > 0)
                                    {
										hhh = (mvarItemHeight * mvarGroup.Properties[i].DataType.Choices.Count);
                                    }

                                    Point pt = new Point(txt.Left + base.Left - 3, txt.Top + txt.Height + base.Top + 3);
                                    pt = Parent.PointToScreen(pt);

                                    wnd.Left = pt.X;
                                    wnd.Top = pt.Y;
                                    wnd.Height = hhh + 2;
                                    wnd.FormClosed += delegate(object dsender, FormClosedEventArgs de)
                                    {
                                        buttonDown = false;
                                        _isPopupOpen = false;
                                        if (!ParentForm.Focused)
                                        {
                                            _hasFocus = false;
                                        }
                                        pnlProperties.Refresh();
                                    };
                                    _isPopupOpen = true;
                                    wnd.Show(ParentForm);
                                }
                                break;
                            }
                        }
                        h += mvarItemHeight;
                    }
                }
            }
        }

        private void pnlProperties_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				RotateSelectedProperty();
			}

        }
        private void pnlProperties_MouseWheel(object sender, MouseEventArgs e)
        {

        }


        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (mvarGroup == null) return;
            if (e.KeyCode == Keys.Enter)
            {
                if (mvarSelectedPropertyIndex > -1)
                {
                    mvarGroup.Properties[mvarSelectedPropertyIndex].Value = txt.Text;
                    txt.Visible = false;
                }
            }
        }

		private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				RotateSelectedProperty();
			}
		}

		private void RotateSelectedProperty()
		{
			if (mvarSelectedPropertyIndex > -1)
			{
				Property prop = mvarGroup.Properties[mvarSelectedPropertyIndex];
				if (prop.DataType.Choices.Count > 0)
				{
					int idx = prop.DataType.Choices.IndexOf(prop.Value);
					if (idx > -1)
					{
						if (idx + 1 < prop.DataType.Choices.Count)
						{
							prop.Value = prop.DataType.Choices[idx + 1].Value;
						}
						else
						{
							prop.Value = prop.DataType.Choices[0].Value;
						}
					}
					else
					{
						prop.Value = prop.DataType.Choices[0].Value;
					}
					txt.Text = prop.Value.ToString();
				}
			}
		}
    }
}
