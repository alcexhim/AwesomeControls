using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.BinaryTextBox
{
	public partial class BinaryTextBoxDropDownWindow : Form
	{
		private BinaryTextBoxControl mvarParentControl = null;
		public BinaryTextBoxDropDownWindow(BinaryTextBoxControl parentControl)
		{
			InitializeComponent();
			mvarParentControl = parentControl;
		}

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			if (mvarParentControl != null)
			{
				mvarParentControl.CloseDropDownWindow();
			}
		}
	}
}
