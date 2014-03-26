using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Accordion
{
	public partial class AccordionControl : UserControl
	{
		public AccordionControl()
		{
			InitializeComponent();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			Theming.Theme.CurrentTheme.DrawAccordionBackground(e.Graphics, new Rectangle(0, 0, Width, Height));
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
	}
}
