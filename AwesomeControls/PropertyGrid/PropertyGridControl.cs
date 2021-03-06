﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	[DefaultEvent("PropertyChanged")]
	public partial class PropertyGridControl : UserControl
	{
		public PropertyGridControl()
		{
			InitializeComponent();
			mvarGroups = new PropertyGroup.PropertyGroupCollection(this);
			this.BackColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor;

			cboObject.BackColor = Theming.Theme.CurrentTheme.ColorTable.DropDownBackgroundColorNormal;
			cboObject.ForeColor = Theming.Theme.CurrentTheme.ColorTable.DropDownForegroundColorNormal;

			lblPropertyName.ForeColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
			lblDescription.ForeColor = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;

			lblPropertyName.Text = String.Empty;
			lblDescription.Text = String.Empty;

			tb.LoadThemeIcons("PropertyGrid");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor);
		}

		private PropertyGridView mvarView = PropertyGridView.Unsorted;
		public PropertyGridView View { get { return mvarView; } }

		public Color PropertyListBackColor { get { return pgp.BackColor; } set { pgp.BackColor = value; } }

		/// <summary>
		/// The <see cref="PropertyCategory" /> in which to place uncategorized properties.
		/// </summary>
		public PropertyCategory DefaultCategory { get { return pgp.DefaultCategory; } set { pgp.DefaultCategory = value; } }

		private PropertyGroup.PropertyGroupCollection mvarGroups = null;
		public PropertyGroup.PropertyGroupCollection Groups { get { return mvarGroups; } }

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

		private void cboObject_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index > -1)
			{
				e.DrawBackground();
				PropertyGroup g = (cboObject.Items[e.Index] as PropertyGroup);
				StringFormat sf = new StringFormat();
				DrawingTools.PrepareGraphics(e.Graphics);

				Font boldFont = new Font(base.Font, FontStyle.Bold);
				Font regularFont = base.Font;
				float w = e.Graphics.MeasureString(g.Name, boldFont).Width;
				w += 2;

				TextRenderer.DrawText(e.Graphics, g.Name, new Font(base.Font, FontStyle.Bold), new Rectangle(e.Bounds.Left, e.Bounds.Top + 1, (int)w, cboObject.ItemHeight), Theming.Theme.CurrentTheme.ColorTable.DropDownForegroundColorNormal, TextFormatFlags.Left);
				TextRenderer.DrawText(e.Graphics, g.DataType.Title, base.Font, new Rectangle(e.Bounds.Left + (int)w, e.Bounds.Top + 1, cboObject.Width - 1 - ((int)w), cboObject.ItemHeight), Theming.Theme.CurrentTheme.ColorTable.DropDownForegroundColorNormal, TextFormatFlags.Left);
			}
		}

		public int SelectedGroupIndex { get { return cboObject.SelectedIndex; } set { cboObject.SelectedIndex = value; } }

		public PropertyGridSortingMode SortingMode
		{
			get { return pgp.SortingMode; }
			set
			{
				pgp.SortingMode = value;
				switch (value)
				{
					case PropertyGridSortingMode.Alphabetical:
					{
						tsbAlphabetical.Checked = true;
						tsbCategorized.Checked = false;
						break;
					}
					case PropertyGridSortingMode.Categorized:
					{
						tsbAlphabetical.Checked = false;
						tsbCategorized.Checked = true;
						break;
					}
				}
			}
		}

		public int ItemHeight { get { return pgp.ItemHeight; } set { pgp.ItemHeight = value; } }

		private void scPropertiesDescription_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(0, 0, scPropertiesDescription.Width - 1, scPropertiesDescription.Height - 1));
		}

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			pgp.SelectedGroup = mvarGroups[cboObject.SelectedIndex];
		}

		private void sc_Panel_Paint(object sender, PaintEventArgs e)
		{
			Panel panel = (sender as Panel);
			e.Graphics.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.ControlDark)), new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
		}

		public bool ShowObjects
		{
			get { return cboObject.Visible; }
			set { cboObject.Visible = value; }
		}
		public bool ShowToolbar
		{
			get { return tb.Visible; }
			set { tb.Visible = value; }
		}
		public bool ShowCommands
		{
			get { return !scPropertiesCommands.Panel2Collapsed; }
			set
			{
				scPropertiesCommands.Panel2Collapsed = !value;
				mnuContextCommands.Checked = !scPropertiesCommands.Panel2Collapsed;
			}
		}
		public bool ShowDescription
		{
			get { return !scPropertiesDescription.Panel2Collapsed; }
			set
			{
				scPropertiesDescription.Panel2Collapsed = !value;
				mnuContextDescription.Checked = !scPropertiesDescription.Panel2Collapsed;
			}
		}

		private void mnuContextCommands_Click(object sender, EventArgs e)
		{
			ShowCommands = !ShowCommands;
		}
		private void mnuContextDescription_Click(object sender, EventArgs e)
		{
			ShowDescription = !ShowDescription;
		}

		private void mnuContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Property p = (pgp.SelectedItem as Property);
			if (p != null)
			{
				if (p.DefaultValueSet)
				{
					mnuContextReset.Enabled = p.IsChanged;
				}
				else
				{
					mnuContextReset.Enabled = false;
				}
			}
		}

		private void mnuContextReset_Click(object sender, EventArgs e)
		{
			Property SelectedProperty = (pgp.SelectedItem as Property);
			if (SelectedProperty != null)
			{
				SelectedProperty.Reset();
			}
		}

		public void UpdatePropertyBounds()
		{
			pgp.UpdatePropertyBounds();
		}

		private void propertyGridPanel1_PropertyChanging(object sender, PropertyChangingEventArgs e)
		{
			OnPropertyChanging(e);
		}

		private void propertyGridPanel1_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(e);
		}

		private void tsbCategorized_Click(object sender, EventArgs e)
		{
			tsbCategorized.Checked = true;
			tsbAlphabetical.Checked = false;
			pgp.SortingMode = PropertyGridSortingMode.Categorized;
		}
		private void tsbAlphabetical_Click(object sender, EventArgs e)
		{
			tsbCategorized.Checked = false;
			tsbAlphabetical.Checked = true;
			pgp.SortingMode = PropertyGridSortingMode.Alphabetical;
		}

		public event PropertyGridSelectionChangingEventHandler SelectionChanging;
		protected virtual void OnSelectionChanging(PropertyGridSelectionChangingEventArgs e)
		{
			if (SelectionChanging != null) SelectionChanging(this, e);
		}
		public event PropertyGridSelectionChangedEventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(PropertyGridSelectionChangedEventArgs e)
		{
			if (SelectionChanged != null) SelectionChanged(this, e);
		}

		private void pgp_SelectionChanging(object sender, PropertyGridSelectionChangingEventArgs e)
		{
			OnSelectionChanging(e);
		}

		private void pgp_SelectionChanged(object sender, PropertyGridSelectionChangedEventArgs e)
		{
			OnSelectionChanged(e);
			if (e.NewProperty != null)
			{
				lblPropertyName.Text = e.NewProperty.Title;
				lblDescription.Text = e.NewProperty.Description;
			}
			else
			{
				lblPropertyName.Text = String.Empty;
				lblDescription.Text = String.Empty;
			}
		}
	}
}
