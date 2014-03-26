using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Wizard
{
	public class WizardDialog
	{
		private WizardPanel.WizardPanelCollection mvarPanels = new WizardPanel.WizardPanelCollection();
		public WizardPanel.WizardPanelCollection Panels { get { return mvarPanels; } }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            WizardForm frm = new WizardForm();
            frm.ParentDialog = this;
            
            System.Windows.Forms.DialogResult result = frm.ShowDialog();
            return result;
        }
	}
}
