using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Windows.Forms;

using AwesomeControls.PropertyGrid;

namespace AwesomeControls.TestProject
{
	public partial class PropertyGridTest : Form
	{
		public PropertyGridTest()
		{
			InitializeComponent();

			PropertyDataType BuildActionDataType = new PropertyDataType("BuildAction",
			new object[] 
			{
				"Compile", "Content", "Embedded Resource", "Code Analysis Dictionary", "Application Definition", "Page", "Resource", "Splash Screen", "Design Data", "Design Data With Design Time Creatable Types", "Entity Deploy", "Fakes"
			}, true);

			PropertyDataType CostumesDataType = new PropertyDataType("Costumes",
			new object[] 
			{
				"Default", "Magical Mirai 2013", "Wonder*Girl", "Future Rider"
			}, true);

			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Hatsune Miku", "Character");
				grp.Properties.Add(new Property("ClassName", "Character", null, true));
				grp.Properties.Add(new Property("Name", "Hatsune Miku", null, false));
				grp.Properties.Add(new Property("Build Action", "Compile", null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = BuildActionDataType;
				grp.Properties.Add(new Property("Costumes", "", null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = CostumesDataType;
				grp.Properties.Add(new Property("IsAvailable", true, null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyDataTypes.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Kagamine Rin", "Character");
				grp.Properties.Add(new Property("ClassName", "Character", null, true));
				grp.Properties.Add(new Property("Name", "Kagamine Rin", null, false));
				grp.Properties.Add(new Property("Costumes", "", null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = CostumesDataType;
				grp.Properties.Add(new Property("IsAvailable", true, null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyDataTypes.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Kagamine Len", "Character");
				grp.Properties.Add(new Property("ClassName", "Character", null, true));
				grp.Properties.Add(new Property("Name", "Kagamine Len", null, false));
				grp.Properties.Add(new Property("Costumes", "", null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = CostumesDataType;
				grp.Properties.Add(new Property("IsAvailable", true, null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyDataTypes.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Megurine Luka", "Character");
				grp.Properties.Add(new Property("ClassName", "Character", null, true));
				grp.Properties.Add(new Property("Name", "Megurine Luka", null, false));
				grp.Properties.Add(new Property("Costumes", "", null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = CostumesDataType;
				grp.Properties.Add(new Property("IsAvailable", true, null, false));
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyDataTypes.Boolean;
				pg.Groups.Add(grp);
			}
		}
	}
}
