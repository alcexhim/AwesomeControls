using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	public partial class PropertyEditor : UserControl
	{
		public PropertyEditor()
		{
			InitializeComponent();
		}

		public virtual int ButtonWidth
		{
			get { return 16; }
		}
		public virtual void DrawButton(Graphics graphics, Rectangle rect, bool buttonDown)
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
	}
}
