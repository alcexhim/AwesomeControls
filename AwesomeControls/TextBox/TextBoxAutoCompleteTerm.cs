using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public class TextBoxAutoCompleteTerm
	{
		public class TextBoxAutoCompleteTermCollection
			: System.Collections.ObjectModel.Collection<TextBoxAutoCompleteTerm>
		{
			public TextBoxAutoCompleteTerm Add(string value)
			{
				return Add(value, String.Empty, null);
			}
			public TextBoxAutoCompleteTerm Add(string value, string description, System.Drawing.Image image)
			{
				TextBoxAutoCompleteTerm act = new TextBoxAutoCompleteTerm();
				act.Value = value;
				act.Description = description;
				act.Image = image;
				base.Add(act);
				return act;
			}
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = Description; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }
	}
}
