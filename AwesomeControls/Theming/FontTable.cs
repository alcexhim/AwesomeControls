using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AwesomeControls.Theming
{
    public class FontTable
	{
		private Font mvarDefault = SystemFonts.MenuFont;
		public Font Default { get { return mvarDefault; } set { mvarDefault = value; } }

        private Font mvarCommandBar = SystemFonts.MenuFont;
        public Font CommandBar { get { return mvarCommandBar; } set { mvarCommandBar = value; } }

        private Font mvarDialogFont = SystemFonts.MenuFont;
		public Font DialogFont { get { return mvarDialogFont; } set { mvarDialogFont = value; } }

		private Font mvarDocumentTabTextSelected = null;
		public Font DocumentTabTextSelected { get { return mvarDocumentTabTextSelected; } set { mvarDocumentTabTextSelected = value; } }
	}
}
