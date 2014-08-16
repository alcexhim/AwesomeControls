using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public abstract class TextBoxAutoSuggestTerm : IComparable
	{
		public class TextBoxAutoSuggestTermCollection
			: System.Collections.ObjectModel.Collection<TextBoxAutoSuggestTerm>
		{
			public TextBoxAutoSuggestTerm Add(string value)
			{
				return Add(value, null, null);
			}
			public TextBoxAutoSuggestTerm Add(string value, string description)
			{
				return Add(value, description, null);
			}
			public TextBoxAutoSuggestTerm Add(string value, System.Drawing.Image image)
			{
				return Add(value, null, image);
			}
			public TextBoxAutoSuggestTerm Add(string value, string description, System.Drawing.Image image)
			{
				TextBoxAutoSuggestTermItem act = new TextBoxAutoSuggestTermItem(value, description, image);
				Add(act);
				return act;
			}
		}

		public abstract int CompareTo(object obj);
	}
	public class TextBoxAutoSuggestTermItem : TextBoxAutoSuggestTerm
	{
		private TextBoxAutoSuggestTermCollection mvarAutoSuggestTerms = new TextBoxAutoSuggestTermCollection();
		public TextBoxAutoSuggestTermCollection AutoSuggestTerms { get { return mvarAutoSuggestTerms; } }

		private bool mvarPreventDefaultSuggestions = false;
		/// <summary>
		/// If true, only suggestions in <see cref="AutoSuggestTerms" /> from this instance will be suggested.
		/// Otherwise, suggestions from the default list will also be suggested.
		/// </summary>
		public bool PreventDefaultSuggestions { get { return mvarPreventDefaultSuggestions; } set { mvarPreventDefaultSuggestions = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { if (value == null) return; mvarDescription = value; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

		public TextBoxAutoSuggestTermItem(string value, string description)
		{
			mvarValue = value;
			mvarDescription = description;
			mvarImage = null;
		}
		public TextBoxAutoSuggestTermItem(string value, System.Drawing.Image image)
		{
			mvarValue = value;
			mvarDescription = String.Empty;
			mvarImage = image;
		}
		public TextBoxAutoSuggestTermItem(string value, string description, System.Drawing.Image image)
		{
			mvarValue = value;
			mvarDescription = description;
			mvarImage = image;
		}

		public override int CompareTo(object obj)
		{
			if (obj is TextBoxAutoSuggestTermItem)
			{
				TextBoxAutoSuggestTermItem item = (obj as TextBoxAutoSuggestTermItem);
				return mvarValue.CompareTo(item.Value);
			}
			return 0;
		}
	}
	public class TextBoxAutoSuggestTermGroup : TextBoxAutoSuggestTerm
	{
		private TextBoxAutoSuggestTermCollection mvarAutoSuggestTerms = new TextBoxAutoSuggestTermCollection();
		public TextBoxAutoSuggestTermCollection AutoSuggestTerms { get { return mvarAutoSuggestTerms; } }

		private System.Collections.Specialized.StringCollection mvarPrefixes = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// The string(s) that must precede terms in this group in order for them to display in the list. The terms
		/// will not appear in the list unless one of the prefixes is satisfied or there are no prefixes defined.
		/// </summary>
		public System.Collections.Specialized.StringCollection Prefixes { get { return mvarPrefixes; } }

		public override int CompareTo(object obj)
		{
			return 0;
		}
	}
}
