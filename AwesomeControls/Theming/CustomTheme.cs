﻿using AwesomeControls.ObjectModels.Theming;
using AwesomeControls.ObjectModels.Theming.Metrics;
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
		private System.Drawing.Brush BrushFromFill(Fill fill, System.Drawing.RectangleF rect)
		{
			if (fill is SolidFill)
			{
				SolidFill fil = (fill as SolidFill);
				return new System.Drawing.SolidBrush(ColorFromString(fil.Color));
			}
			else if (fill is LinearGradientFill)
			{
				LinearGradientFill fil = (fill as LinearGradientFill);

				System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, LinearGradientFillOrientationToLinearGradientMode(fil.Orientation));
				if (fil.ColorStops.Count > 0)
				{
					List<System.Drawing.Color> colorList = new List<System.Drawing.Color>();
					List<float> positionList = new List<float>();

					for (int i = 0; i < fil.ColorStops.Count; i++)
					{
						colorList.Add(ColorFromString(fil.ColorStops[i].Color));
						positionList.Add(FloatFromString(fil.ColorStops[i].Position));
					}

					System.Drawing.Drawing2D.ColorBlend blend = new System.Drawing.Drawing2D.ColorBlend(fil.ColorStops.Count);
					blend.Colors = colorList.ToArray();
					blend.Positions = positionList.ToArray();
					brush.InterpolationColors = blend;
				}
				return brush;
			}
			return null;
		}

		private float FloatFromString(string value)
		{
			if (value.EndsWith("%"))
			{
				value = value.Substring(0, value.Length - 1);
				float val = (float)(Double.Parse(value) / 100);
				return val;
			}
			else
			{
				float val = 0.0f;
				if (Single.TryParse(value, out val)) return val;
			}
			return 0;
		}

		private System.Drawing.Drawing2D.LinearGradientMode LinearGradientFillOrientationToLinearGradientMode(LinearGradientFillOrientation orientation)
		{
			switch (orientation)
			{
				case LinearGradientFillOrientation.BackwardDiagonal: return System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
				case LinearGradientFillOrientation.ForwardDiagonal: return System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
				case LinearGradientFillOrientation.Horizontal: return System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
				case LinearGradientFillOrientation.Vertical: return System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			}
			return System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
		}

		private void DrawRenderingAction(System.Drawing.Graphics graphics, object component, RenderingAction action)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();

			System.Drawing.RectangleF bounds = new System.Drawing.RectangleF();
			if (component is System.Windows.Forms.Control)
			{
				bounds = (component as System.Windows.Forms.Control).ClientRectangle;
				bounds = new System.Drawing.RectangleF(0, 0, bounds.Width, bounds.Height);
			}
			else if (component is System.Windows.Forms.ToolStripItem)
			{
				System.Windows.Forms.ToolStripItem tsi = (component as System.Windows.Forms.ToolStripItem);
				bounds = tsi.Bounds;
				bounds = new System.Drawing.RectangleF(0, 0, bounds.Width, bounds.Height);
				
				if (tsi is System.Windows.Forms.ToolStripMenuItem)
				{
					PaddingMetric padding = (GetMetric("MenuItemMargin") as PaddingMetric);
					if (padding != null)
					{
						bounds.X += padding.Left;
						bounds.Y += padding.Top;
						bounds.Width -= padding.Right;
						bounds.Height -= padding.Bottom;
					}
				}
			}
			else if (component is System.Drawing.Rectangle)
			{
				bounds = (System.Drawing.Rectangle)component;
			}

			dict.Add("Component.Width", bounds.Width);
			dict.Add("Component.Height", bounds.Height);

			if (component is System.Windows.Forms.ToolStripDropDownMenu)
			{
				System.Windows.Forms.ToolStripDropDownMenu tsddm = (component as System.Windows.Forms.ToolStripDropDownMenu);
				if (tsddm.OwnerItem != null)
				{
					dict.Add("Component.Parent.Width", tsddm.OwnerItem.Width);
					dict.Add("Component.Parent.Height", tsddm.OwnerItem.Height);
				}
			}
			if (component is System.Windows.Forms.ToolStripSplitButton)
			{
				dict.Add("Component.ButtonWidth", (component as System.Windows.Forms.ToolStripSplitButton).ButtonBounds.Width);
				dict.Add("Component.DropDownButtonWidth", (component as System.Windows.Forms.ToolStripSplitButton).DropDownButtonBounds.Width);
			}

			if (action is RectangleRenderingAction)
			{
				RectangleRenderingAction act = (action as RectangleRenderingAction);
				
				float x = act.X.Evaluate(dict) + bounds.X;
				float y = act.Y.Evaluate(dict) + bounds.Y;
				float w = act.Width.Evaluate(dict);
				float h = act.Height.Evaluate(dict);

				if (act.Fill != null)
				{
					graphics.FillRectangle(BrushFromFill(act.Fill, new System.Drawing.RectangleF(x, y, w, h)), x, y, w, h);
                }
                if (act.Outline != null)
                {
                    graphics.DrawRectangle(PenFromOutline(act.Outline), x, y, w - 1, h - 1);
                }
			}
			else if (action is LineRenderingAction)
			{
				LineRenderingAction act = (action as LineRenderingAction);

				float x1 = act.X1.Evaluate(dict) + bounds.X;
				float y1 = act.Y1.Evaluate(dict) + bounds.Y;
				float x2 = act.X2.Evaluate(dict);
				float y2 = act.Y2.Evaluate(dict);

				if (act.Outline != null)
				{
					graphics.DrawLine(PenFromOutline(act.Outline), x1, y1, x2, y2);
				}
			}
		}

		private ThemeMetric GetMetric(string name, AwesomeControls.ObjectModels.Theming.Theme theme = null)
		{
			if (theme == null) theme = mvarThemeDefinition;

			ThemeMetric tc = theme.Metrics[name];
			if (tc == null && theme.InheritsTheme != null) return GetMetric(name, theme.InheritsTheme);

			return tc;
		}

		private void DrawRendering(System.Drawing.Graphics graphics, object component, Rendering rendering)
		{
			foreach (RenderingAction action in rendering.Actions)
			{
				DrawRenderingAction(graphics, component, action);
			}
		}

		private void DrawThemeComponent(System.Drawing.Graphics graphics, object component, ThemeComponent tc, Guid stateID)
		{
			if (tc.InheritsComponent != null)
			{
				DrawThemeComponent(graphics, component, tc.InheritsComponent, stateID);
			}

			foreach (Rendering rendering in tc.Renderings)
			{
				if (rendering.States.Count == 0 || rendering.States.Contains(stateID))
				{
					// we can use this rendering
					DrawRendering(graphics, component, rendering);
				}
			}
		}

		public override void DrawSeparator(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Drawing.Rectangle rectangle, bool vertical)
		{
			if (vertical)
			{
				graphics.DrawLine(new System.Drawing.Pen(ColorFromString("@SeparatorForeground")), 3, 2, 3, item.Bounds.Height - 2);
				graphics.DrawLine(new System.Drawing.Pen(ColorFromString("@SeparatorBackground")), 4, 2, 4, item.Bounds.Height - 2);
			}
			else
			{
				graphics.DrawLine(new System.Drawing.Pen(ColorFromString("@SeparatorForeground")), 2, 3, item.Bounds.Width - 2, 3);
				graphics.DrawLine(new System.Drawing.Pen(ColorFromString("@SeparatorBackground")), 2, 4, item.Bounds.Width - 2, 4);
			}
		}

        private Guid GetThemeStateGUIDForControlState(ControlState state, bool focused, bool selected)
        {
            switch (state)
            {
                case ControlState.Normal:
                {
					if (focused)
					{
						if (selected)
						{
							return ThemeComponentStateGuids.NormalFocusedSelected;
						}
						else
						{
							return ThemeComponentStateGuids.NormalFocused;
						}
					}
					else
					{
						if (selected)
						{
							return ThemeComponentStateGuids.NormalSelected;
						}
					}
                    return ThemeComponentStateGuids.Normal;
                }
                case ControlState.Hover:
                {
					if (focused)
					{
						if (selected)
						{
							return ThemeComponentStateGuids.HoverFocusedSelected;
						}
						else
						{
							return ThemeComponentStateGuids.HoverFocused;
						}
					}
					else
					{
						if (selected)
						{
							return ThemeComponentStateGuids.HoverSelected;
						}
					}
                    return ThemeComponentStateGuids.Hover;
                }
                case ControlState.Pressed:
				{
					if (focused)
					{
						if (selected)
						{
							return ThemeComponentStateGuids.PressedFocusedSelected;
						}
						else
						{
							return ThemeComponentStateGuids.PressedFocused;
						}
					}
					else
					{
						if (selected)
						{
							return ThemeComponentStateGuids.PressedSelected;
						}
					}
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
				if (tc != null) DrawThemeComponent(graphics, parent, tc, ThemeComponentStateGuids.Normal);
			}
			else if (parent is System.Windows.Forms.ToolStripDropDownMenu)
			{
				System.Windows.Forms.ToolStripDropDownMenu tsddm = (parent as System.Windows.Forms.ToolStripDropDownMenu);
				ThemeComponent tc = null;
				if (tsddm.OwnerItem != null && !tsddm.OwnerItem.IsOnDropDown)
				{
					tc = GetComponent(ThemeComponentGuids.CommandBarTopLevelPopup);
				}
				if (tc == null) tc = GetComponent(ThemeComponentGuids.CommandBarPopup);
				if (tc != null) DrawThemeComponent(graphics, parent, tc, ThemeComponentStateGuids.Normal);
			}
			else if (parent is System.Windows.Forms.ToolStrip)
			{
				ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBar);
				if (tc != null) DrawThemeComponent(graphics, parent, tc, ThemeComponentStateGuids.Normal);
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

		public override void DrawDocumentTabBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectTab, ControlState controlState, DockingWindows.DockPosition position, bool selected, bool focused)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.DocumentTab);
			if (tc != null)
			{
				DrawThemeComponent(graphics, rectTab, tc, GetThemeStateGUIDForControlState(controlState, focused, selected));
			}
		}

		public override void DrawContentAreaBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.ContentArea);
			if (tc != null) DrawThemeComponent(graphics, rectangle, tc, ThemeComponentStateGuids.Normal);
		}

        public override void DrawMenuItemBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item)
        {
            ThemeComponent tc = null;
            if (!item.IsOnDropDown)
            {
				tc = GetComponent(ThemeComponentGuids.CommandBarTopLevelItem);
            }
			if (tc == null) tc = GetComponent(ThemeComponentGuids.CommandBarMenuItem);
			if (tc == null) tc = GetComponent(ThemeComponentGuids.CommandBarItem);

			Guid state = ThemeComponentStateGuids.Normal;
			if (item.Selected) state = ThemeComponentStateGuids.Hover;
			if (item.Pressed) state = ThemeComponentStateGuids.Pressed;
			if (!item.Enabled) state = ThemeComponentStateGuids.Disabled;

            if (tc != null) DrawThemeComponent(graphics, item, tc, state);
        }
		public override void DrawCommandButtonBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarItem);

			Guid state = ThemeComponentStateGuids.Normal;
			if (item.Selected) state = ThemeComponentStateGuids.Hover;
			if (item.Pressed) state = ThemeComponentStateGuids.Pressed;
			if (!item.Enabled) state = ThemeComponentStateGuids.Disabled;
			if (tc != null) DrawThemeComponent(graphics, item, tc, state);
		}
		public override void DrawSplitButtonBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarSplitItem);
			if (tc == null) tc = GetComponent(ThemeComponentGuids.CommandBarItem);

			Guid state = ThemeComponentStateGuids.Normal;
			if (item.Selected) state = ThemeComponentStateGuids.Hover;
			if (item.Pressed) state = ThemeComponentStateGuids.Pressed;
			if (!item.Enabled) state = ThemeComponentStateGuids.Disabled;
			if (tc != null) DrawThemeComponent(graphics, item, tc, state);
		}
        public override void DrawText(System.Drawing.Graphics graphics, string text, System.Drawing.Color color, System.Drawing.Font font, System.Drawing.Rectangle textRectangle, System.Windows.Forms.TextFormatFlags textFormat, System.Windows.Forms.ToolStripTextDirection textDirection, System.Windows.Forms.ToolStripItem item)
        {
            color = ColorFromString("@CommandBarItemForeground");
            base.DrawText(graphics, text, color, font, textRectangle, textFormat, textDirection, item);
        }

        public override void DrawCommandBarPanelBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
        {
            ThemeComponent tc = GetComponent(ThemeComponentGuids.CommandBarRaftingContainer);
			if (tc != null) DrawThemeComponent(graphics, rectangle, tc, ThemeComponentStateGuids.Normal);
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

		public override void DrawListViewBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.ListView);
			if (tc != null)
			{
				DrawThemeComponent(graphics, rectangle, tc, ThemeComponentStateGuids.Normal);
			}
		}
		public override void DrawListItemBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state, bool selected, bool focused)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.ListViewItem);
			if (tc != null)
			{
				Guid guid = GetThemeStateGUIDForControlState(state, focused, selected);
				DrawThemeComponent(g, rect, tc, guid);
			}
		}

		public override void DrawListSelectionRectangle(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
		{
			ThemeComponent tc = GetComponent(ThemeComponentGuids.ListViewSelectionRectangle);
			if (tc != null) DrawThemeComponent(g, rect, tc, ThemeComponentStateGuids.Normal);
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
