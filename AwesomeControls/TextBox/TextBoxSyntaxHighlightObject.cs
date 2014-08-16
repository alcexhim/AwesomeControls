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

		private TextBoxSyntaxGroup mvarGroup = null;
		public TextBoxSyntaxGroup Group { get { return mvarGroup; } set { mvarGroup = value; } }
	}

	public class TextBoxSyntaxHighlightTerm : TextBoxSyntaxHighlightObject
	{
		public TextBoxSyntaxHighlightTerm(string value, TextBoxSyntaxGroup group)
		{
			mvarValue = value;
			Group = group;
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
		public TextBoxSyntaxHighlightBlock(string begin, string end, TextBoxSyntaxGroup group)
		{
			mvarBegin = begin;
			mvarEnd = end;
			Group = group;
		}

		private string mvarBegin = String.Empty;
		public string Begin { get { return mvarBegin; } set { mvarBegin = value; } }
		private string mvarEnd = String.Empty;
		public string End { get { return mvarEnd; } set { mvarEnd = value; } }
	}
}
