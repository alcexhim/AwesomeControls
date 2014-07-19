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
            _parent = parent;
            if (_parent != null)
            {
                base.Font = _parent.Font;

				foreach (PropertyDataTypeChoice s in _parent.SelectedProperty.DataType.Choices)
                {
                	_validValues.Add(s);
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
                if (_parent.SelectedProperty == null) return;
                if (_parent.SelectedProperty.DataType.Choices.Count > 0)
                {
                    int i = 0;
					foreach (PropertyDataTypeChoice s in _validValues)
                    {
                        Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
                        if (e.Y >= rect.Top && e.Y <= rect.Bottom)
                        {
                            _parent.SelectedProperty.Value = s.Value;
                            base.Refresh();
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
                if (_parent.SelectedProperty == null) return;
                if (_validValues.Count > 0)
                {
                    int i = 0;
					foreach (PropertyDataTypeChoice s in _validValues)
                    {
                        Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
                        if (e.Y >= rect.Top && e.Y <= rect.Bottom)
                        {
                            _parent.SelectedProperty.Value = s.Value;
                            base.Refresh();
                            return;
                        }
                        i += _parent.ItemHeight;
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _parent.Refresh();
            base.Close();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawingTools.PrepareGraphics(e.Graphics);
            if (_parent.SelectedProperty == null) return;

            int i = 0;
            foreach (PropertyDataTypeChoice s in _validValues)
            {
                Rectangle rect = new Rectangle(0, i, base.Width, _parent.ItemHeight);
                Color fc = base.ForeColor;
                if (_parent.SelectedProperty.Value == s.Value)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), rect);
                    fc = Color.FromKnownColor(KnownColor.HighlightText);
                    rect.Height--;
                    DrawingTools.DrawFocusRectangle(e.Graphics, rect);
                }
                Rectangle rect1 = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);
                e.Graphics.DrawString(s.Title, base.Font, new SolidBrush(fc), rect1);
                i += _parent.ItemHeight;
            }
        }
    }
}
