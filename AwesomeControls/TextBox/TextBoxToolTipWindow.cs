using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TextBox
{
    public partial class TextBoxToolTipWindow : Form
    {
        public TextBoxToolTipWindow()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
        }

        protected override bool ShowWithoutActivation { get { return true; } }
    }
}
