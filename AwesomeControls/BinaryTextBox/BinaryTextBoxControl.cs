using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.BinaryTextBox
{
	public partial class BinaryTextBoxControl : UserControl
	{
		public BinaryTextBoxControl()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}

		private BinaryTextBoxDropDownWindow ddw = null;

		private ControlState m_Button1_State = ControlState.Normal;
		private ControlState m_Button2_State = ControlState.Normal;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Location.X >= this.Width - (24 * 2) && e.Location.X <= this.Width - 24)
			{
				m_Button1_State = ControlState.Pressed;
			}
			else if (e.Location.X >= this.Width - 24 && e.Location.X <= this.Width)
			{
				m_Button2_State = ControlState.Pressed;
			}
			else
			{
				m_Button2_State = ControlState.Normal;
				m_Button2_State = ControlState.Normal;
			}
			Refresh();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			m_Button1_State = ControlState.Normal;
			m_Button2_State = ControlState.Normal;
			Refresh();

			if (e.Location.X >= this.Width - (24 * 2) && e.Location.X <= this.Width - 24)
			{
				OnButton1Pressed();
			}
			else if (e.Location.X >= this.Width - 24 && e.Location.X <= this.Width)
			{
				OnButton2Pressed();
			}
		}

		private void OnButton1Pressed()
		{
			MessageBox.Show("Button1 Pressed");
		}
		private void OnButton2Pressed()
		{
			MessageBox.Show("Button2 Pressed");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rectTextBox = new Rectangle();
			rectTextBox.X = this.ClientRectangle.Left;
			rectTextBox.Y = this.ClientRectangle.Top;
			rectTextBox.Width = this.Width - (24 * 2);
			rectTextBox.Height = this.ClientRectangle.Height;
			DrawingTools.DrawSunkenBorder(e.Graphics, rectTextBox);

			Rectangle rectButton = new Rectangle();
			rectButton.Width = 24;
			rectButton.Height = this.ClientRectangle.Height;

			rectButton.X = this.Width - rectButton.Width;
			rectButton.Y = this.ClientRectangle.Top;
			e.Graphics.FillRectangle(DrawingTools.Brushes.ControlBrush, rectButton);
			if (m_Button2_State == ControlState.Pressed)
			{
				DrawingTools.DrawSunkenBorder(e.Graphics, rectButton);
			}
			else
			{
				DrawingTools.DrawRaisedBorder(e.Graphics, rectButton);
			}

			rectButton.X = this.Width - (rectButton.Width * 2);
			rectButton.Y = this.ClientRectangle.Top;
			e.Graphics.FillRectangle(DrawingTools.Brushes.ControlBrush, rectButton);
			if (m_Button1_State == ControlState.Pressed)
			{
				DrawingTools.DrawSunkenBorder(e.Graphics, rectButton);
			}
			else
			{
				DrawingTools.DrawRaisedBorder(e.Graphics, rectButton);
			}
			DrawingTools.DrawArrow(e.Graphics, ForeColor, DrawingTools.Direction.Down, rectButton.X + 9, rectButton.Y + 8, 4);

			Font font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
			Rectangle rectChar = new Rectangle(this.ClientRectangle.Left + 4, this.ClientRectangle.Top + 4, 16, 16);
			for (int i = 0; i < mvarValue.Length; i++)
			{
				string val = mvarValue[i].ToString("X").PadLeft(2, '0');
				double w = e.Graphics.MeasureString(val, font).Width;
				rectChar.Width = (int)(w + mvarWordSpacing + 4);
				e.Graphics.DrawString(val, font, new SolidBrush(ForeColor), rectChar);
				rectChar.X += (int)(w + mvarWordSpacing + wordSpacingOffset);

				if (rectChar.Right >= this.Width - (24 * 2)) break;
			}
		}

		private int wordSpacingOffset = -3;

		private int mvarWordSpacing = 2;
		public int WordSpacing { get { return mvarWordSpacing; } set { mvarWordSpacing = value; } }

		private bool mvarIsDropDownOpened = false;
		public bool IsDropDownOpened { get { return mvarIsDropDownOpened; } }

		public void OpenDropDownWindow()
		{
			if (ddw == null) ddw = new BinaryTextBoxDropDownWindow(this);
			if (ddw.IsDisposed) ddw = new BinaryTextBoxDropDownWindow(this);
			ddw.Show(this);
			mvarIsDropDownOpened = true;
		}
		public void CloseDropDownWindow()
		{
			if (ddw == null) ddw = new BinaryTextBoxDropDownWindow(this);
			if (ddw.IsDisposed) ddw = new BinaryTextBoxDropDownWindow(this);
			ddw.Close();
			mvarIsDropDownOpened = false;
		}

		private byte[] mvarValue = new byte[0];
		public byte[] Value { get { return mvarValue; } set { mvarValue = value; Refresh(); } }
	}
}
