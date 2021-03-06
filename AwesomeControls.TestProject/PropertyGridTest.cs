﻿using System;
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

		private bool mvarF5 = false;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.F5)
			{
				mvarF5 = !mvarF5;
				foreach (PropertyGroup grp in pg.Groups)
				{
					foreach (Property p in grp.Properties)
					{
						p.ReadOnly = mvarF5;
					}
				}
				pg.Refresh();
			}
		}

		public PropertyGridTest()
		{
			InitializeComponent();

			base.KeyPreview = true;
			pg.Font = SystemFonts.MenuFont;

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

			PropertyDataType PrecisionDataType = new PropertyDataType("Precision",
			new object[]
			{
				"Single", "Double"
			});


			PropertyDataType Vector3DataType = new PropertyDataType("Vector3");
			Vector3DataType.Properties.Add(new Property("Precision"));
			Vector3DataType.Properties[0].DefaultValue = PrecisionDataType.Choices[0].Value;
			Vector3DataType.Properties[0].Value = PrecisionDataType.Choices[0].Value;
			Vector3DataType.Properties[0].DataType = PrecisionDataType;
			Vector3DataType.Properties.Add(new Property("X"));
			Vector3DataType.Properties.Add(new Property("Y"));
			Vector3DataType.Properties.Add(new Property("Z"));
			Vector3DataType.PropertyValueRendering += delegate(object sender, PropertyValueRenderingEventArgs e)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("{ ");
				sb.Append(e.Property.Properties["X"].Value.ToString());
				sb.Append(", ");
				sb.Append(e.Property.Properties["Y"].Value.ToString());
				sb.Append(", ");
				sb.Append(e.Property.Properties["Z"].Value.ToString());
				sb.Append(" }");
				e.DisplayString = sb.ToString();
			};
			Vector3DataType.PropertyValueParsing += delegate(object sender, PropertyValueParsingEventArgs e)
			{
				if (!(e.DisplayString.StartsWith("{") && e.DisplayString.EndsWith("}")))
				{
					e.Cancel = true;
					return;
				}

				string s = e.DisplayString.Substring(1, e.DisplayString.Length - 2).Trim();
				string[] v = s.Split(new char[] { ',' });

				e.Property.Properties["X"].Value = Double.Parse(v[0].Trim());
				e.Property.Properties["Y"].Value = Double.Parse(v[0].Trim());
				e.Property.Properties["Z"].Value = Double.Parse(v[0].Trim());
			};

			PropertyDataType CharacterDataType = new PropertyDataType("Character");
			CharacterDataType.Properties.Add(new Property("ClassName", "Character", null, true));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Description = "The name of the character used in programming.";
			CharacterDataType.Properties.Add(new Property("Name", "Kagamine Len", null, false));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Description = "The display name of the character.";
			CharacterDataType.Properties.Add(new Property("Costumes", "", null, false));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Description = "The default costume of the character.";

			PropertyCategory catAppearance = new PropertyCategory("Appearance");
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Category = catAppearance;
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].DataType = CostumesDataType;

			CharacterDataType.Properties.Add(new Property("Location", null, null, false));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].DataType = Vector3DataType;
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Category = catAppearance;
			CharacterDataType.Properties.Add(new Property("Build Action", "Compile", null, false));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].DataType = BuildActionDataType;
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].Category = new PropertyCategory("Build");

			CharacterDataType.Properties.Add(new Property("IsAvailable", true, null, false));
			CharacterDataType.Properties[CharacterDataType.Properties.Count - 1].DataType = PropertyDataTypes.Boolean;


			pg.Groups.Add(new PropertyGrid.PropertyGroup("Hatsune Miku", CharacterDataType));

			pg.Groups[pg.Groups.Count - 1].Properties["Name"].DefaultValue = "Hatsune Miku";
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].Value = "Hatsune Miku";
			pg.Groups.Add(new PropertyGrid.PropertyGroup("Kagamine Rin", CharacterDataType));
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].DefaultValue = "Kagamine Rin";
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].Value = "Kagamine Rin";
			pg.Groups.Add(new PropertyGrid.PropertyGroup("Kagamine Len", CharacterDataType));
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].DefaultValue = "Kagamine Len";
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].Value = "Kagamine Len";
			pg.Groups.Add(new PropertyGrid.PropertyGroup("Megurine Luka", CharacterDataType));
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].DefaultValue = "Megurine Luka";
			pg.Groups[pg.Groups.Count - 1].Properties["Name"].Value = "Megurine Luka";
		}

		private void pg_PropertyChanged(object sender, PropertyGrid.PropertyChangedEventArgs e)
		{
			MessageBox.Show("Property '" + e.Property.Name + "' changed to '" + e.Property.Value + "'!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
