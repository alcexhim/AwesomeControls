using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AwesomeControls.Theming.BuiltinThemes
{
	public class SlickTheme : OfficeXPTheme
	{
		public override CommandBarMenuAnimationType CommandBarMenuAnimationType { get { return Theming.CommandBarMenuAnimationType.None; } }

		protected override void InitCommonColors()
		{
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(170, 204, 255);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(170, 204, 255);

			ColorTable.CommandBarBackground = Color.FromArgb(227, 239, 255);
			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(71, 101, 151);
			ColorTable.CommandBarBorderOuterFloating = Color.FromArgb(71, 101, 151);

			ColorTable.CommandBarGradientMenuBackgroundBegin = Color.FromArgb(227, 239, 255);
			ColorTable.CommandBarGradientMenuBackgroundEnd = Color.FromArgb(227, 239, 255);
			ColorTable.CommandBarMainMenuBackground = Color.FromArgb(227, 239, 255);

			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(255, 242, 204);
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(214, 191, 141);

			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(71, 101, 151);
			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(227, 239, 255);

			ColorTable.CommandBarGradientMenuBarBackgroundBegin = Color.FromArgb(196, 221, 255);
			ColorTable.CommandBarGradientMenuBarBackgroundEnd = Color.FromArgb(196, 221, 255);

			ColorTable.CommandBarMenuBorder = Color.FromArgb(71, 101, 151);
			ColorTable.CommandBarMenuBackground = Color.FromArgb(227, 239, 255);

			ColorTable.CommandBarImageMarginBackground = Color.FromArgb(237, 245, 255);
		}
		public override void DrawMenuItemBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			base.DrawMenuItemBackground(graphics, item);
		}
	}
}
