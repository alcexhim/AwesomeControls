using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
    [Flags()]
    public enum TextBoxFormattingAttributes
    {
        None = 0,
        ForeColor = 1,
        BackColor = 2,
        Font = 4,
        Underline = 8,
        Overline = 16
    }
}
