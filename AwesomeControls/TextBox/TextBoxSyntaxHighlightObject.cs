using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxSyntaxHighlightObject
	{
		public class TextBoxSyntaxHighlightObjectCollection
			: System.Collections.ObjectModel.Collection<TextBoxSyntaxHighlightObject>
		{
        }

        private System.Drawing.Color mvarForeColor = System.Drawing.Color.Black;
        public System.Drawing.Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

        private System.Drawing.Color mvarBackColor = System.Drawing.Color.Transparent;
        public System.Drawing.Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }
	}

    public class TextBoxSyntaxHighlightTerm : TextBoxSyntaxHighlightObject
    {
        public TextBoxSyntaxHighlightTerm(string value, System.Drawing.Color foreColor, System.Drawing.Color? backColor = null)
        {
            mvarValue = value;
            ForeColor = foreColor;
            if (backColor != null)
            {
                BackColor = backColor.Value;
            }
        }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

        public override string ToString()
        {
            return mvarValue;
        }
    }

    public class TextBoxSyntaxHighlightBlock : TextBoxSyntaxHighlightObject
    {
        public TextBoxSyntaxHighlightBlock(string begin, string end, System.Drawing.Color foreColor, System.Drawing.Color? backColor = null)
        {
            mvarBegin = begin;
            mvarEnd = end;
            ForeColor = foreColor;
            if (backColor != null)
            {
                BackColor = backColor.Value;
            }
        }

        private string mvarBegin = String.Empty;
        public string Begin { get { return mvarBegin; } set { mvarBegin = value; } }
        private string mvarEnd = String.Empty;
        public string End { get { return mvarEnd; } set { mvarEnd = value; } }
    }
}
