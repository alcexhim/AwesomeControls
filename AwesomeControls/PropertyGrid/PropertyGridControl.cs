using System;
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
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor);
		}

		private PropertyGridView mvarView = PropertyGridView.Unsorted;
		public PropertyGridView View { get { return mvarView; } }

		public Color PropertyListBackColor { get { return propertyGridPanel1.BackColor; } set { propertyGridPanel1.BackColor = value; } }

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

		public int ItemHeight { get { return propertyGridPanel1.ItemHeight; } set { propertyGridPanel1.ItemHeight = value; } }

		private void scPropertiesDescription_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBorderColor), new Rectangle(0, 0, scPropertiesDescription.Width - 1, scPropertiesDescription.Height - 1));
		}

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			propertyGridPanel1.SelectedGroup = mvarGroups[cboObject.SelectedIndex];
		}

		private void sc_Panel_Paint(object sender, PaintEventArgs e)
		{
			Panel panel = (sender as Panel);
			e.Graphics.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.ControlDark)), new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
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
			Property p = propertyGridPanel1.SelectedProperty;
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
			if (propertyGridPanel1.SelectedProperty != null)
			{
				propertyGridPanel1.SelectedProperty.Reset();
			}
		}

		public void UpdatePropertyBounds()
		{
			propertyGridPanel1.UpdatePropertyBounds();
		}

		private void propertyGridPanel1_PropertyChanging(object sender, PropertyChangingEventArgs e)
		{
			OnPropertyChanging(e);
		}

		private void propertyGridPanel1_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(e);
		}
	}
}
