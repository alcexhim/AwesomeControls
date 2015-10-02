using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public class Theme : ICloneable
	{
		public class ThemeCollection
			: System.Collections.ObjectModel.Collection<Theme>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarAuthor = String.Empty;
		public string Author { get { return mvarAuthor; } set { mvarAuthor = value; } }

		private ThemeColor.ThemeColorCollection mvarColors = new ThemeColor.ThemeColorCollection();
		public ThemeColor.ThemeColorCollection Colors { get { return mvarColors; } }

		private ThemeFont.ThemeFontCollection mvarFonts = new ThemeFont.ThemeFontCollection();
		public ThemeFont.ThemeFontCollection Fonts { get { return mvarFonts; } }

		private ThemeMetric.ThemeMetricCollection mvarMetrics = new ThemeMetric.ThemeMetricCollection();
		public ThemeMetric.ThemeMetricCollection Metrics { get { return mvarMetrics; } }

		private ThemeComponent.ThemeComponentCollection mvarComponents = new ThemeComponent.ThemeComponentCollection();
		public ThemeComponent.ThemeComponentCollection Components { get { return mvarComponents; } }

		private ThemeStockImage.ThemeStockImageCollection mvarStockImages = new ThemeStockImage.ThemeStockImageCollection();
		public ThemeStockImage.ThemeStockImageCollection StockImages { get { return mvarStockImages; } }

		private ThemeProperty.ThemePropertyCollection mvarProperties = new ThemeProperty.ThemePropertyCollection();
		public ThemeProperty.ThemePropertyCollection Properties { get { return mvarProperties; } }

		public object Clone()
		{
			Theme clone = new Theme();
			clone.Author = (mvarAuthor.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			clone.ID = mvarID;
			foreach (ThemeColor item in mvarColors)
			{
				clone.Colors.Add(item.Clone() as ThemeColor);
			}
			foreach (ThemeFont item in mvarFonts)
			{
				clone.Fonts.Add(item.Clone() as ThemeFont);
			}
			foreach (ThemeMetric item in mvarMetrics)
			{
				clone.Metrics.Add(item.Clone() as ThemeMetric);
			}
			foreach (ThemeComponent item in mvarComponents)
			{
				clone.Components.Add(item.Clone() as ThemeComponent);
			}
			foreach (ThemeStockImage item in mvarStockImages)
			{
				clone.StockImages.Add(item.Clone() as ThemeStockImage);
			}
			foreach (ThemeProperty item in mvarProperties)
			{
				clone.Properties.Add(item.Clone() as ThemeProperty);
			}
			return clone;
		}

		private Guid mvarInheritsThemeID = Guid.Empty;
		public Guid InheritsThemeID { get { return mvarInheritsThemeID; } set { mvarInheritsThemeID = value; } }

		private Theme mvarInheritsTheme = null;
		public Theme InheritsTheme { get { return mvarInheritsTheme; } set { mvarInheritsTheme = value; } }

		private string mvarBasePath = null;
		public string BasePath { get { return mvarBasePath; } set { mvarBasePath = value; } }
	}
}
