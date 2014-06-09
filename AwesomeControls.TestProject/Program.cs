using System;

namespace AwesomeControls.TestProject
{
	public static class Program
	{
		public static void Main (string[] args)
		{
			/*
			AwesomeControls.Theming.BuiltinThemes.Office2007Theme o2k7blue = new AwesomeControls.Theming.BuiltinThemes.Office2007Theme();
			o2k7blue.ColorScheme = AwesomeControls.Theming.BuiltinThemes.Office2007Theme.Office2007ColorScheme.Blue;

			AwesomeControls.Theming.Theme.CurrentTheme = o2k7blue;
			*/

			//System.Windows.Forms.Application.EnableVisualStyles();

			AwesomeControls.Theming.Theme.CurrentTheme = AwesomeControls.Theming.Themes.System;
			AwesomeControls.Theming.Theme.CurrentTheme = new AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme(Theming.BuiltinThemes.VisualStudio2012Theme.ColorMode.Dark);


			BinaryDropDownTest mwt = new BinaryDropDownTest();
			mwt.ShowDialog();

			/*
			MainWindowTest mwt = new MainWindowTest();
			mwt.ShowDialog();
			
			TimelineTest test = new TimelineTest();
			test.ShowDialog();
			
			PropertyGridTest test = new PropertyGridTest();
			test.ShowDialog();
			
			DesignerTest test = new DesignerTest();
			test.ShowDialog();

			TextBoxTest test = new TextBoxTest();
			test.ShowDialog();

			*/
		}

	}
}

