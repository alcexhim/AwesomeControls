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

			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Hatsune Miku", "Character");
				grp.Properties.Add("ClassName", "Character", true);
				grp.Properties.Add("Name", "Hatsune Miku", false);
				grp.Properties.Add("Costumes", "", false);
				grp.Properties.Add("IsAvailable", true, false);
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyGrid.PropertyDataType.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Kagamine Rin", "Character");
				grp.Properties.Add("ClassName", "Character", true);
				grp.Properties.Add("Name", "Kagamine Rin", false);
				grp.Properties.Add("Costumes", "", false);
				grp.Properties.Add("IsAvailable", true, false);
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyGrid.PropertyDataType.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Kagamine Len", "Character");
				grp.Properties.Add("ClassName", "Character", true);
				grp.Properties.Add("Name", "Kagamine Len", false);
				grp.Properties.Add("Costumes", "", false);
				grp.Properties.Add("IsAvailable", true, false);
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyGrid.PropertyDataType.Boolean;
				pg.Groups.Add(grp);
			}
			{
				PropertyGrid.PropertyGroup grp = new PropertyGrid.PropertyGroup("Megurine Luka", "Character");
				grp.Properties.Add("ClassName", "Character", true);
				grp.Properties.Add("Name", "Megurine Luka", false);
				grp.Properties.Add("Costumes", "", false);
				grp.Properties.Add("IsAvailable", true, false);
				grp.Properties[grp.Properties.Count - 1].DataType = PropertyGrid.PropertyDataType.Boolean;
				pg.Groups.Add(grp);
			}
		}
	}
}
