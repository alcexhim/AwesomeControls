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

		private System.Drawing.Color ColorFromString(string value)
		{
			if (value.StartsWith("@"))
			{
				string color = mvarThemeDefinition.Colors[value.Substring(1)].Value;
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

		private void DrawRenderingAction(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip parent, RenderingAction action)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict.Add("Component.Width", parent.Width);
			dict.Add("Component.Height", parent.Height);

			if (action is RectangleRenderingAction)
			{
				RectangleRenderingAction act = (action as RectangleRenderingAction);
				
				float x = act.X.Evaluate(dict);
				float y = act.Y.Evaluate(dict);
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

		private void DrawRendering(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip parent, Rendering rendering)
		{
			foreach (RenderingAction action in rendering.Actions)
			{
				DrawRenderingAction(graphics, parent, action);
			}
		}

		private void DrawThemeComponent(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip parent, ThemeComponent tc)
		{
			if (tc.InheritsComponent != null)
			{
				DrawThemeComponent(graphics, parent, tc.InheritsComponent);
			}

			foreach (Rendering rendering in tc.Renderings)
			{
				if (rendering.States.Count == 0 || rendering.States.Contains(ThemeComponentStateGuids.Normal))
				{
					// we can use this rendering
					DrawRendering(graphics, parent, rendering);
				}
			}
		}

		public override void DrawCommandBarBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip parent)
		{
			if (parent is System.Windows.Forms.MenuStrip)
			{
				ThemeComponent tc = mvarThemeDefinition.Components[ThemeComponentGuids.CommandBarMenu];
				if (tc != null) DrawThemeComponent(graphics, parent, tc);
			}
			else if (parent is System.Windows.Forms.ToolStripDropDownMenu)
			{
				ThemeComponent tc = mvarThemeDefinition.Components[ThemeComponentGuids.CommandBarPopup];
				if (tc != null) DrawThemeComponent(graphics, parent, tc);
			}
			else if (parent is System.Windows.Forms.ToolStrip)
			{
				ThemeComponent tc = mvarThemeDefinition.Components[ThemeComponentGuids.CommandBar];
				if (tc != null) DrawThemeComponent(graphics, parent, tc);
			}
			else
			{

			}
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
