using AwesomeControls.DockingWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TestProject
{
	public partial class MainWindowTest : AwesomeControls.Window
	{
		public MainWindowTest()
		{
			InitializeComponent();

			System.Windows.Forms.TextBox textbox = new System.Windows.Forms.TextBox();
			textbox.Multiline = true;
			DockingWindow wndTextbox = dcc.Windows.Add("wndTextbox", "Text Box Test", textbox);
			dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(wndTextbox);

			DockingWindow wndButton = dcc.Windows.Add("wndButton", "Big FUKING button", new Button());
			dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(wndButton);

			DockingWindow wndSolutionExplorer = dcc.Windows.Add("wndSolutionExplorer", "Solution Explorer", new AwesomeControls.ListView.ListViewControl());
			DockingWindow wndProperties = dcc.Windows.Add("wndProperties", "Properties", new AwesomeControls.PropertyGrid.PropertyGridControl());
			DockingWindow wndErrorList = dcc.Windows.Add("wndErrorList", "Error List", new ListView.ListViewControl());
			DockingWindow wndOutput = dcc.Windows.Add("wndOutput", "Output", new RichTextBox());
			
			// dcc.Areas[DockPosition.Right].Areas[DockPosition.Top].Windows.Add(wndSolutionExplorer);
			// dcc.Areas[DockPosition.Right].Areas[DockPosition.Bottom].Windows.Add(wndProperties);

			dcc.Areas[DockPosition.Right].Windows.Add(wndSolutionExplorer);
			dcc.Areas[DockPosition.Right].Windows.Add(wndProperties);

			dcc.Areas[DockPosition.Center].Areas[DockPosition.Bottom].Windows.Add(wndErrorList);
			dcc.Areas[DockPosition.Center].Areas[DockPosition.Bottom].Windows.Add(wndOutput);
		}
	}
}
