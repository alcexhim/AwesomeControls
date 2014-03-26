using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TestProject
{
	public partial class PropertyGridTest : Form
	{
		public PropertyGridTest()
		{
			InitializeComponent();

			PropertyGrid.PropertyGroup grpTestGroup = new PropertyGrid.PropertyGroup("Test Group", "IGroupTestObj");
			grpTestGroup.Properties.Add("AJAXName", "Media Manager", true);
			grpTestGroup.Properties.Add("PropertyType", "Testing", false, "Testing", "Implementation", "Debugging");
			pg.Groups.Add(grpTestGroup);

			PropertyGrid.PropertyGroup grpTestGroup1 = new PropertyGrid.PropertyGroup("mms01PDHFmNu7324YzO", "Media Manager Surrogate");
			grpTestGroup1.Properties.Add("Object", "Test Group", true);
			grpTestGroup1.Properties.Add("PropertyType", "Testing", false, "Testing", "Implementation", "Debugging");
			pg.Groups.Add(grpTestGroup1);
		}
	}
}
