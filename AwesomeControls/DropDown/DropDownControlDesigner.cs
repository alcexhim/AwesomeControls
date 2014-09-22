using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace AwesomeControls.DropDown
{
	public class DropDownControlDesigner : ControlDesigner
	{
		public override System.Collections.IList SnapLines
		{
			get
			{
				System.Collections.IList list = base.SnapLines;
				// int num = DesignerTools.GetTextBaseline(this.Control.CreateGraphics(), this.Control.Font, this.Control.ClientRectangle, ContentAlignment.TopLeft);
				// num += 3;
				list.Add(new SnapLine(SnapLineType.Baseline, this.Control.Font.Height + 4, SnapLinePriority.Medium));
				return list;
			}
		}
	}
}
