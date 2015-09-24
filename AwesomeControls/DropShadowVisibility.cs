using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls
{
	public enum DropShadowVisibility
	{
		/// <summary>
		/// The <see cref="Window" /> will not have a drop shadow.
		/// </summary>
		None,
		/// <summary>
		/// The <see cref="Window" /> will have a drop shadow if the current theme requests it.
		/// </summary>
		Default,
		/// <summary>
		/// The <see cref="Window" /> will always have a drop shadow even if the current theme does not request it (if the window border is custom-drawn).
		/// </summary>
		Visible
	}
}
