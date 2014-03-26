using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Theming
{
    public static class Themes
    {
        private static BuiltinThemes.SystemTheme mvarSystem = new BuiltinThemes.SystemTheme();
        public static BuiltinThemes.SystemTheme System { get { return mvarSystem; } }
    }
}
