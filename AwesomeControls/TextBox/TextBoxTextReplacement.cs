using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxTextReplacement
	{
		public class TextBoxTextReplacementCollection
			: System.Collections.ObjectModel.Collection<TextBoxTextReplacement>
		{
			public TextBoxTextReplacement Add(string SearchQuery, string ReplacementValue)
			{
				return Add(SearchQuery, ReplacementValue, 0);
			}
			public TextBoxTextReplacement Add(string SearchQuery, string ReplacementValue, int CursorOffset)
			{
				TextBoxTextReplacement repl = new TextBoxTextReplacement();
				repl.SearchQuery = SearchQuery;
				repl.ReplacementValue = ReplacementValue;
				repl.CursorOffset = CursorOffset;
				Add(repl);
				return repl;
			}
		}

		private string mvarSearchQuery = String.Empty;
		public string SearchQuery { get { return mvarSearchQuery; } set { mvarSearchQuery = value; } }

		private string mvarReplacementValue = String.Empty;
		public string ReplacementValue { get { return mvarReplacementValue; } set { mvarReplacementValue = value; } }

		private int mvarCursorOffset = 0;
		public int CursorOffset { get { return mvarCursorOffset; } set { mvarCursorOffset = value; } }
	}
}
