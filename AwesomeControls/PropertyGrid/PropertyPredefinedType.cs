using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
    public static class PropertyPredefinedType
    {
        private static string[] m_BooleanValues = new string[] { "True", "False" };
        public static string[] BooleanValues { get { return m_BooleanValues; } }
    }
}
