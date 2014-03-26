using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.NotebookTabs
{
	public class NotebookTabControlDesigner : System.Windows.Forms.Design.ParentControlDesigner
	{
		public override bool CanParent(System.Windows.Forms.Control control)
		{
			return (control is NotebookTab && !Control.Contains(control));
		}
	}
}
