using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TextBox
{
	public partial class TextBoxAutoCompleteWindow : Form
	{
		public TextBoxAutoCompleteWindow()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		protected override bool ShowWithoutActivation { get { return true; } }

		private void lst_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			if (e.Index > -1)
			{
				TextBoxAutoCompleteTerm term = (lst.Items[e.Index] as TextBoxAutoCompleteTerm);
				if (term.Image != null)
				{
					e.Graphics.DrawImage(term.Image, e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
				}
				TextRenderer.DrawText(e.Graphics, term.Value, Font, new Rectangle(e.Bounds.X + e.Bounds.Height + 4, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height - 4, e.Bounds.Height), e.ForeColor, TextFormatFlags.Left);
			}
			e.DrawFocusRectangle();
		}
	}
}
