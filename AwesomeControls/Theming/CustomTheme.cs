using AwesomeControls.ObjectModels.Theming;
using AwesomeControls.ObjectModels.Theming.RenderingActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Theming
{
	public class CustomTheme : Theme
	{
		private AwesomeControls.ObjectModels.Theming.Theme mvarThemeDefinition = null;
		public AwesomeControls.ObjectModels.Theming.Theme ThemeDefinition { get { return mvarThemeDefinition; } }

		public CustomTheme(AwesomeControls.ObjectModels.Theming.Theme themeDefinition)
		{
			mvarThemeDefinition = themeDefinition;

			base.ID = themeDefinition.ID;
			base.Name = themeDefinition.Name;
			base.Title = themeDefinition.Title;
		}

		private System.Drawing.Color ColorFromString(string value, AwesomeControls.ObjectModels.Theming.Theme theme = null)
		{
			if (theme == null) theme = mvarThemeDefinition;

			if (value.StartsWith("@"))
			{
                string name = value.Substring(1);
                if (!theme.Colors.Contains(name))
                {
					if (theme.InheritsTheme != null) return ColorFromString(value, theme.InheritsTheme);

                    Console.WriteLine("ac-theme: theme definition does not contain color '" + name + "'");
                    return System.Drawing.Color.Empty;
                }
				string color = theme.Colors[name].Value;
				return ColorFromString(color);
			}
			else if (value.StartsWith("#") && value.Length == 7)
			{
				string RRGGBB = value.Substring(1);
				byte RR = Byte.Parse(RRGGBB.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
				byte GG = Byte.Parse(RRGGBB.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
				byte BB = Byte.Parse(RRGGBB.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
				return System.Drawing.Color.FromArgb(RR, GG, BB);
			}
			else if (value.StartsWith("rgb(") && value.EndsWith(")"))
			{
				string r_g_b = value.Substring(3, value.Length - 4);
				string[] rgb = r_g_b.Split(new char[] { ',' });
				if (rgb.Length == 3)
				{
					byte r = Byte.Parse(rgb[0].Trim());
					byte g = Byte.Parse(rgb[1].Trim());
					byte b = Byte.Parse(rgb[2].Trim());
				}
			}
			else if (value.StartsWith("rgba(") && value.EndsWith(")"))
			{

			}
			else
			{
				try
				{
					System.Drawing.Color color = System.Drawing.Color.FromName(value);
					return color;
				}
				catch
				{

				}
			}
			return System.Drawing.Color.Empty;
		}

		private System.Drawing.Pen PenFromOutline(Outline outline)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(ColorFromString(outline.Color), outline.Width);
			return pen;
		}
		private System.Drawing.Brush BrushFromFill(Fill fill)
		{
			if (fill is SolidFill)
			{
				SolidFill fil = (fill as SolidFill);
				return new System.Drawing.SolidBrush(ColorFromString(fil.Color));
			}
			return null;
		}

		private void DrawRenderingAction(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds, RenderingAction action)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict.Add("Component.Width", bounds.Width);
			dict.Add("Component.Height", bounds.Height);

			if (action is RectangleRenderingAction)
			{
				RectangleRenderingAction act = (action as RectangleRenderingAction);
				
				float x = act.X.Evaluate(dict) + bounds.X;
				float y = act.Y.Evaluate(dict) + bounds.Y;
				float w = act.Width.Evaluate(dict);
				float h = act.Height.Evaluate(dict);

				if (act.Fill != null)
				{
					graphics.FillRectangle(BrushFromFill(act.Fill), x, y, w, h);
                }
                if (act.Outline != null)
                {
                    graphics.DrawRectangle(PenFromOutline(act.Outline), x, y, w - 1, h - 1);
                }
			}
		}

		private void DrawRendering(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds, Rendering rendering)
		{
			foreach (RenderingAction action in rendering.Actions)
			{
				DrawRenderingAction(graphics, bounds, action);
			}
		}

		private void DrawThemeComponent(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds, ThemeComponent tc, ControlState state)
		{
			if (tc.InheritsComponent != null)
			{
				DrawThemeComponent(graphics, bounds, tc.InheritsComponent, state);
			}

			foreach (Rendering rendering in tc.Renderings)
			{
				if (rendering.States.Count == 0 || rendering.States.Contains(GetThemeStateGUIDForControlState(state)))
				{
					// we can use this rendering
					DrawRendering(graphics, bounds, rendering);
				}
			}
		}

        private Guid GetThemeStateGUIDForControlState(ControlState state)
        {
            switch (state)
            {
                case ControlState.Normal:
                {
                    return ThemeComponentStateGuids.Normal;
                }
                case ControlState.Hover:
                {
                    return ThemeComponentStateGuids.Hover;
                }
                case ControlState.Pressed:
                {
                    return ThemeComponentStateGuids.Pressed;
                }
                case ControlState.Disabled:
                {
                    return ThemeComponentStateGuids.Disabled;
                }
            }
            return ThemeComponentStateGuids.None;
        }

		public override void DrawCommandBarBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip parent)
		{
			if (parent is System.Windows.Forms.MenuStrip)
			{
				ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarMenu);
				if (tc != null) DrawThemeComponent(graphics, parent.ClientRectangle, tc, ControlState.Normal);
			}
			else if (parent is System.Windows.Forms.ToolStripDropDownMenu)
			{
				ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarPopup);
                if (tc != null) DrawThemeComponent(graphics, parent.ClientRectangle, tc, ControlState.Normal);
			}
			else if (parent is System.Windows.Forms.ToolStrip)
			{
				ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBar);
                if (tc != null) DrawThemeComponent(graphics, parent.ClientRectangle, tc, ControlState.Normal);
			}
			else
			{

			}
		}

		private ThemeComponent GetComponent(Guid id, AwesomeControls.ObjectModels.Theming.Theme theme = null)
		{
			if (theme == null) theme = mvarThemeDefinition;
			
			ThemeComponent tc = theme.Components[id];
			if (tc == null && theme.InheritsTheme != null) return GetComponent(id, theme.InheritsTheme);
			
			return tc;
		}

        public override void DrawMenuItemBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item)
        {
            ThemeComponent tc = null;
            if (!item.IsOnDropDown)
            {
				tc = GetComponent(ThemeComponentGuids.CommandBarTopLevelItem);
            }
            if (tc == null) tc = GetComponent(ThemeComponentGuids.CommandBarItem);

            ControlState state = ControlState.Normal;
            if (item.Selected) state = ControlState.Hover;
            if (item.Pressed) state = ControlState.Pressed;
            if (tc != null) DrawThemeComponent(graphics, new System.Drawing.Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height), tc, state);
        }
        public override void DrawText(System.Drawing.Graphics graphics, string text, System.Drawing.Color color, System.Drawing.Font font, System.Drawing.Rectangle textRectangle, System.Windows.Forms.TextFormatFlags textFormat, System.Windows.Forms.ToolStripTextDirection textDirection, System.Windows.Forms.ToolStripItem item)
        {
            color = ColorFromString("@CommandBarItemForeground");
            base.DrawText(graphics, text, color, font, textRectangle, textFormat, textDirection, item);
        }

        public override void DrawCommandBarPanelBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
        {
            ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarRaftingContainer);
            if (tc != null) DrawThemeComponent(graphics, rectangle, tc, ControlState.Normal);
        }

		public override void DrawDropDownBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle, ControlState state)
		{
			throw new NotImplementedException();
		}

		public override void DrawDropDownButton(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle, ControlState dropdownState, ControlState buttonState)
		{
			throw new NotImplementedException();
		}

		public override void DrawDropDownMenuBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		public override void DrawButtonBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state)
		{
			throw new NotImplementedException();
		}

		public override void DrawTextBoxBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state)
		{
			throw new NotImplementedException();
		}

		public override void DrawListItemBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state, bool selected, bool focused)
		{
			throw new NotImplementedException();
		}

		public override void DrawListSelectionRectangle(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
		{
			throw new NotImplementedException();
		}

		public override void DrawListColumnBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state, bool sorted)
		{
			// throw new NotImplementedException();
		}

		public override void DrawListViewTreeGlyph(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state, bool expanded)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarChunk(System.Drawing.Graphics g, System.Drawing.Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarPulse(System.Drawing.Graphics g, System.Drawing.Rectangle rect, System.Windows.Forms.Orientation orientation)
		{
			throw new NotImplementedException();
		}
	}
}
