using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxSyntaxGroup
	{
		public class TextBoxSyntaxHighlightGroupCollection
			: System.Collections.ObjectModel.Collection<TextBoxSyntaxGroup>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private Color mvarBackColor = Color.Transparent;
		public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

		private Color mvarForeColor = Color.Transparent;
		public Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

		public TextBoxSyntaxGroup(string title, Color foreColor, Color backColor = default(Color), Image image = null)
		{
			mvarTitle = title;
			mvarForeColor = foreColor;
			mvarBackColor = backColor;
			mvarImage = image;
		}
	}
}
