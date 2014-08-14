using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	public partial class PropertyGridDropDownWindow : Form
	{
		private PropertyGridPanel _parent = null;
		private List<PropertyDataTypeChoice> _validValues = new List<PropertyDataTypeChoice>();

		public PropertyGridDropDownWindow(PropertyGridPanel parent)
		{
			InitializeComponent();
			base.DoubleBuffered = true;

			_parent = parent;
			if (_parent != null)
			{
				base.Font = _parent.Font;

				Property SelectedProperty = (_parent.SelectedItem as Property);
				foreach (PropertyDataTypeChoice s in SelectedProperty.DataType.Choices)
				{
					_validValues.Add(s);
				}
			}
		}

		private int mvarSelectedIndex = 0;

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Property SelectedProperty = (_parent.SelectedItem as Property);
			foreach (PropertyDataTypeChoice choice in _validValues)
			{
				if (choice.Value == null)
				{
					if (SelectedProperty.Value == null)
					{
						mvarSelectedIndex = _validValues.IndexOf(choice);
						return;
					}
				}
				else
				{
					if (choice.Value.Equals(SelectedProperty.Value))
					{
						mvarSelectedIndex = _validValues.IndexOf(choice);
						return;
					}
				}
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			_parent.Refresh();
			base.Close();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				Property SelectedProperty = (_parent.SelectedItem as Property);
				if (SelectedProperty == null) return;
				if (SelectedProperty.DataType.Choices.Count > 0)
				{
					int i = 0;
					for (int j = 0; j < _validValues.Count; j++)
					{
						Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
						if (e.Y >= rect.Top && e.Y <= rect.Bottom)
						{
							mvarSelectedIndex = j;
							Refresh();
							return;
						}
						i += _parent.ItemHeight;
					}
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.Left)
			{
				Property SelectedProperty = (_parent.SelectedItem as Property);
				if (SelectedProperty == null) return;
				if (_validValues.Count > 0)
				{
					int i = 0;
					for (int j = 0; j < _validValues.Count; j++)
					{
						Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
						if (e.Y >= rect.Top && e.Y <= rect.Bottom)
						{
							mvarSelectedIndex = j;
							base.Refresh();
						}
						i += _parent.ItemHeight;
					}
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			base.Close();

			if (mvarSelectedIndex > -1 && mvarSelectedIndex < _validValues.Count)
			{
				Property SelectedProperty = (_parent.SelectedItem as Property);
				SelectedProperty.Value = _validValues[mvarSelectedIndex].Value;
				_parent.Refresh();
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.PropertyGridBackgroundColor);

			DrawingTools.PrepareGraphics(e.Graphics);
			Property SelectedProperty = (_parent.SelectedItem as Property);
			if (SelectedProperty == null) return;

			int i = 0;
			foreach (PropertyDataTypeChoice s in _validValues)
			{
				Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
				Color fc = Theming.Theme.CurrentTheme.ColorTable.PropertyGridForegroundColor;
				if (_validValues.IndexOf(s) == mvarSelectedIndex)
				{
					e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.PropertyGridItemHighlightBackgroundColor), rect);
					fc = Color.FromKnownColor(KnownColor.HighlightText);
					rect.Height--;
					DrawingTools.DrawFocusRectangle(e.Graphics, rect);
				}
				Rectangle rect1 = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);
				TextRenderer.DrawText(e.Graphics, s.Title, base.Font, rect1, fc, TextFormatFlags.Left);
				i += _parent.ItemHeight;
			}
		}
	}
}
