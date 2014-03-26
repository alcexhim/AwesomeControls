using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
	public enum TextBoxAutoCompleteMode
	{
		/// <summary>
		/// AutoComplete is disabled for this text box.
		/// </summary>
		None = 0,
		/// <summary>
		/// Displays the first autocomplete term in the text box alongside the typed text, highlighted. Pressing
		/// TAB will cycle between autocomplete terms (or complete the term if there is only one), and pressing
		/// ENTER will complete the term.
		/// </summary>
		Inline = 1,
		/// <summary>
		/// Displays a list of terms that the user can choose from, similar to Microsoft Visual Studio.
		/// </summary>
		Popup = 2
	}
}
