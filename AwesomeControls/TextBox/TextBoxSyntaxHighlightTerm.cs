using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxSyntaxHighlightTerm
	{
		public class TextBoxSyntaxHighlightTermCollection
			: System.Collections.ObjectModel.Collection<TextBoxSyntaxHighlightTerm>
		{
			public TextBoxSyntaxHighlightTerm Add(string Value, System.Drawing.Color ForeColor)
			{
				TextBoxSyntaxHighlightTerm term = new TextBoxSyntaxHighlightTerm();
				term.ForeColor = ForeColor;
				term.Value = Value;
				base.Add(term);
				return term;
			}
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private System.Drawing.Color mvarForeColor = System.Drawing.Color.Black;
		public System.Drawing.Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }
	}
}
