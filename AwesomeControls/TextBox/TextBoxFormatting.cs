using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxFormatting
	{
		public class TextBoxFormattingCollection
            : System.Collections.ObjectModel.Collection<TextBoxFormatting>
		{

		}

        public TextBoxFormatting(int start, int end)
        {
            mvarStart = start;
            mvarEnd = end;
        }

        private int mvarStart = 0;
        public int Start { get { return mvarStart; } set { mvarStart = value; } }

        private int mvarEnd = 0;
        public int End { get { return mvarEnd; } set { mvarEnd = value; } }

        private TextBoxFormattingAttributes mvarAttributes = TextBoxFormattingAttributes.None;
        public TextBoxFormattingAttributes Attributes { get { return mvarAttributes; } set { mvarAttributes = value; } }

        private TextBoxFormattingLineStyle mvarUnderlineStyle = new TextBoxFormattingLineStyle();
        public TextBoxFormattingLineStyle UnderlineStyle { get { return mvarUnderlineStyle; } }
        
        private TextBoxFormattingLineStyle mvarOverlineStyle = new TextBoxFormattingLineStyle();
        public TextBoxFormattingLineStyle OverlineStyle { get { return mvarOverlineStyle; } }

		private Color mvarForeColor = Color.FromKnownColor(KnownColor.WindowText);
		public Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

		private Color mvarBackColor = Color.Empty;
		public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

		private Font mvarFont = null;
		public Font Font { get { return mvarFont; } set { mvarFont = value; } }
	}
}
