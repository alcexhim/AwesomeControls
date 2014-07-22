using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	[DefaultEvent("PropertyChanged")]
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

		public event PropertyChangingEventHandler PropertyChanging;
		protected internal virtual void OnPropertyChanging(PropertyChangingEventArgs e)
		{
			if (PropertyChanging != null) PropertyChanging(this, e);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected internal virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) PropertyChanged(this, e);
		}


		private PropertyGridView mvarView = PropertyGridView.Unsorted;
		public PropertyGridView View { get { return mvarView; } set { mvarView = value; } }

		private PropertyGroup mvarGroup = null;
		public PropertyGroup Group
		{
			get { return mvarGroup; }
			set
			{
				mvarGroup = value;

				if (mvarGroup != null)
				{
					mvarGroup.ParentControl = this;
					if (mvarSelectedProperty != null)
					{
						Property p = mvarGroup.Properties[mvarSelectedProperty.Name];
						if (p != null)
						{
							SelectedProperty = p;
						}
						else
						{
							SelectedProperty = mvarGroup.Properties[0];
						}
					}
					else
					{
						SelectedProperty = mvarGroup.Properties[0];
					}
				}

				UpdatePropertyBounds();
				Refresh();
			}
		}

		private int mvarMarginWidth = 17;

		private Property mvarSelectedProperty = null;
		public Property SelectedProperty
		{
			get { return mvarSelectedProperty; }
			set { mvarSelectedProperty = value; }
		}

		private int mvarItemHeight = 18;
		public int ItemHeight { get { return mvarItemHeight; } set { mvarItemHeight = value; } }

		public override void Refresh()
		{
			base.Refresh();
			if (mvarGroup == null) return;

			if (SelectedProperty != null)
			{
				if (SelectedProperty.Value != null)
				{
					txt.Text = SelectedProperty.Value.ToString();
				}
				else
				{
					txt.Text = String.Empty;
				}
			}
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
			if (vsc.Value > vsc.Maximum)
			{
				vsc.Value = 0;
				pnlProperties.Refresh();
			}
		}

		private int CalculatePropertyHeight(Property property)
		{
			int h = 0;
			h++;
			h += mvarItemHeight;
			if (property.Properties.Count > 0)
			{
				if (property.Expanded)
				{
					for (int i = 0; i < property.Properties.Count; i++)
					{
						h += CalculatePropertyHeight(property.Properties[i]);
					}
				}
			}
			h++;
			return h;
		}

		private double mvarSplitterPosition = 0.40;
		public double SplitterPosition { get { return mvarSplitterPosition; } set { mvarSplitterPosition = value; } }

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
					int y = 0;
					for (int i = s; i < mvarGroup.Properties.Count; i++)
					{
						RenderProperty(e.Graphics, mvarGroup.Properties[i], ref y, 0);
						if (i < mvarGroup.Properties.Count - 1)
						{
							e.Graphics.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), mvarMarginWidth, y + mvarItemHeight, pnlProperties.Width - 2, y + mvarItemHeight);
						}
					}
				}
			}
			e.Graphics.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), leftWidth, 1, leftWidth, pnlProperties.Height - 2);
		}

		private int mvarPropertyIndentSize = 14;

		private void RenderProperty(Graphics g, Property property, ref int y, int indentLevel)
		{
			int indentSize = mvarPropertyIndentSize * indentLevel;
			int leftWidth = (int)(mvarSplitterPosition * pnlProperties.Width) - mvarMarginWidth;
			int rightWidth = pnlProperties.Width - leftWidth;

			Color fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
			if (property.ReadOnly)
			{
				fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridDisabledForegroundColor;
			}
			if (SelectedProperty == property)
			{
				txt.ForeColor = fc;
				if (_hasFocus)
				{
					g.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridItemHighlightBackgroundColor), new Rectangle(mvarMarginWidth, y, leftWidth - mvarMarginWidth, mvarItemHeight));
					fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridItemHighlightForegroundColor;
				}
				else
				{
					g.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(mvarMarginWidth, y, leftWidth - mvarMarginWidth, mvarItemHeight));
				}

				PropertyEditor editor = property.DataType.Editor;
				if (editor != null)
				{
					Rectangle rect = new Rectangle(pnlProperties.Width - editor.ButtonWidth - 1, y, editor.ButtonWidth, mvarItemHeight);
					g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
					editor.DrawButton(g, rect, buttonDown);
				}
				else if (property.DataType.Choices.Count > 0)
				{
					Rectangle rect = new Rectangle(pnlProperties.Width - 16 - 1, y, 16, mvarItemHeight);
					g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
					DrawDropDownArrow(g, rect, buttonDown);
				}
			}

			if (property.Properties.Count > 0)
			{
				Rectangle rectTreeButton = new Rectangle(5, y + 4, 8, 8);

				// border rectangle
				g.DrawRectangle(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor), rectTreeButton);

				// horizontal line
				g.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor), rectTreeButton.X + 2, rectTreeButton.Y + 4, rectTreeButton.Right - 2, rectTreeButton.Y + 4);

				if (!property.Expanded)
				{
					// vertical line
					g.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor), rectTreeButton.X + 4, rectTreeButton.Y + 2, rectTreeButton.X + 4, rectTreeButton.Bottom - 2);
				}
			}

			Rectangle rectTitle = new Rectangle(mvarMarginWidth, y + 1, leftWidth - mvarMarginWidth, mvarItemHeight);
			Rectangle rectValue = new Rectangle(leftWidth + 2, y + 1, rightWidth, mvarItemHeight);
			rectTitle.X += indentSize;
			rectTitle.Width -= indentSize;
			// rectValue.X += indentSize;
			// rectValue.Width -= indentSize;

			TextRenderer.DrawText(g, property.Name, base.Font, rectTitle, fc, TextFormatFlags.Left);

			if (property.ReadOnly)
			{
				fc = Color.FromKnownColor(KnownColor.GrayText);
			}
			else
			{
				fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
			}
			if (property.Value == null)
			{
				if (property.DefaultValue != null)
				{
					TextRenderer.DrawText(g, property.DefaultValue.ToString(), base.Font, rectValue, fc, TextFormatFlags.Left);
				}
			}
			else
			{
				Font font = base.Font;
				if (property.IsChanged)
				{
					font = new Font(font, FontStyle.Bold);
				}
				if (SelectedProperty == property)
				{
					txt.Font = font;
				}
				TextRenderer.DrawText(g, property.Value.ToString(), font, rectValue, fc, TextFormatFlags.Left);
			}


			y += mvarItemHeight;
			if (property.Properties.Count > 0 && property.Expanded)
			{
				foreach (Property prop in property.Properties)
				{
					RenderProperty(g, prop, ref y, indentLevel + 1);
					g.DrawLine(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), mvarMarginWidth, y, pnlProperties.Width - 2, y);
				}
			}
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

		private Dictionary<Property, Rectangle> propBounds = new Dictionary<Property, Rectangle>();
		public void UpdatePropertyBounds()
		{
			propBounds.Clear();

			int h = 0;
			if (mvarGroup != null)
			{
				for (int i = 0; i < mvarGroup.Properties.Count; i++)
				{
					UpdatePropertyBounds(mvarGroup.Properties[i], ref h);
				}
			}
		}
		private void UpdatePropertyBounds(Property p, ref int h)
		{
			Rectangle rect = new Rectangle(mvarMarginWidth, h, pnlProperties.Width - mvarMarginWidth, mvarItemHeight);
			propBounds[p] = rect;
			h += mvarItemHeight;
			if (p.Properties.Count > 0 && p.Expanded)
			{
				foreach (Property p1 in p.Properties)
				{
					UpdatePropertyBounds(p1, ref h);
				}
			}
		}

		public Rectangle GetPropertyBounds(Property p)
		{
			if (!propBounds.ContainsKey(p))
			{
				UpdatePropertyBounds();
			}
			return propBounds[p];
		}
		public Property HitTest(int x, int y, bool includeMargin = false)
		{
			if (propBounds.Count == 0)
			{
				UpdatePropertyBounds();
			}

			foreach (KeyValuePair<Property, Rectangle> kvp in propBounds)
			{
				Rectangle rect = kvp.Value;
				if (includeMargin)
				{
					rect.X -= mvarMarginWidth;
					rect.Width += mvarMarginWidth;
				}
				if (rect.Contains(x, y)) return kvp.Key;
			}
			return null;
		}

		private bool buttonDown = false;
		private int m_clicked = 0;
		private void pnlProperties_MouseDown(object sender, MouseEventArgs e)
		{
			Property p = HitTest(e.X, e.Y);

			Property pMargin = HitTest(e.X, e.Y, true);


			if (pMargin != null && pMargin.Properties.Count > 0 && e.X <= mvarMarginWidth)
			{
				pMargin.Expanded = !pMargin.Expanded;
				SelectedProperty = pMargin;
				Refresh();
				return;
			}

			if (p == null)
			{
				SelectedProperty = null;
				return;
			}

			Rectangle bounds = GetPropertyBounds(p);

			if (p != SelectedProperty)
			{
				m_clicked = 0;
			}
			SelectedProperty = p;

			int leftWidth = (int)(mvarSplitterPosition * pnlProperties.Width) - mvarMarginWidth;
			int rightWidth = pnlProperties.Width - leftWidth;

			txt.Visible = true;
			txt.Left = leftWidth + 2;
			txt.Multiline = true;
			txt.Height = mvarItemHeight - 1;
			txt.Width = rightWidth - 6;

			if (p.Value != null)
			{
				txt.Text = p.Value.ToString();
			}
			else
			{
				txt.Text = String.Empty;
			}
			txt.Top = bounds.Y + 1;

			if (p.ReadOnly)
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

			PropertyEditor editor = p.DataType.Editor;
			if (editor != null)
			{
				txt.Width -= editor.ButtonWidth;
				if (e.Button == MouseButtons.Left && e.X >= pnlProperties.Width - editor.ButtonWidth)
				{
					buttonDown = true;
				}
			}
			else if (p.DataType.Choices.Count > 0)
			{
				txt.Width -= 16;
				if (e.Button == MouseButtons.Left && e.X >= pnlProperties.Width - 16)
				{
					buttonDown = true;
				}
			}

			dummy.Focus();
			pnlProperties.Refresh();
			m_clicked++;
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
					if (e.X >= (pnlProperties.Width - 16))
					{
						Property p = SelectedProperty;
						if (p != null && !p.ReadOnly)
						{
							// Dropdown open
							PropertyGridDropDownWindow wnd = new PropertyGridDropDownWindow(this);
							wnd.StartPosition = FormStartPosition.Manual;
							wnd.Width = txt.Width + 6;
							int hhh = 0;
							if (p.DataType.Choices.Count > 0)
							{
								hhh = (mvarItemHeight * p.DataType.Choices.Count);
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
					}
				}
			}
		}

		private void pnlProperties_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				RotateSelectedProperty();
				Refresh();
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
				if (SelectedProperty != null)
				{
					SelectedProperty.Value = txt.Text;
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
			if (SelectedProperty != null)
			{
				PropertyDataType dt = SelectedProperty.DataType;
				if (SelectedProperty.ReadOnly)
				{
					if (dt.Properties.Count > 0)
					{
						SelectedProperty.Expanded = !SelectedProperty.Expanded;
						UpdatePropertyBounds();
					}
					else
					{
						txt.Focus();
						txt.SelectAll();
					}
				}
				else
				{
					if (dt.Choices.Count > 0)
					{
						int idx = dt.Choices.IndexOf(SelectedProperty.Value);
						if (idx > -1)
						{
							if (idx + 1 < dt.Choices.Count)
							{
								SelectedProperty.Value = dt.Choices[idx + 1].Value;
							}
							else
							{
								SelectedProperty.Value = dt.Choices[0].Value;
							}
						}
						else
						{
							SelectedProperty.Value = dt.Choices[0].Value;
						}
						txt.Text = SelectedProperty.Value.ToString();
					}
					else if (dt.Properties.Count > 0)
					{
						SelectedProperty.Expanded = !SelectedProperty.Expanded;
						UpdatePropertyBounds();
					}
					else
					{
						txt.Focus();
						txt.SelectAll();
					}
				}
			}
		}
	}
}
