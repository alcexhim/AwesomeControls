using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.TextBox
{
    public enum TextBoxFormattingLineType
    {
        None = 0,
        Solid,
        Dot,
        Dash,
        DotDash,
        DotDotDash,
        Wave
    }
    public class TextBoxFormattingLineStyle
    {
        private TextBoxFormattingLineType mvarType = TextBoxFormattingLineType.None;
        public TextBoxFormattingLineType Type { get { return mvarType; } set { mvarType = value; } }

        private int mvarSize = 1;
        public int Size { get { return mvarSize; } set { mvarSize = value; } }

        private System.Drawing.Color mvarColor = System.Drawing.Color.Empty;
        public System.Drawing.Color Color { get { return mvarColor; } set { mvarColor = value; } }

        private int mvarCount = 1;
        public int Count { get { return mvarCount; } set { mvarCount = value; } }
    }
}
