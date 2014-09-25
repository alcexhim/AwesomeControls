using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Web.StyleSheet;
using UniversalEditor.DataFormats.Web.StyleSheet;

namespace AwesomeControls.Theming.BuiltinThemes
{
	public class StyleSheetTheme : ClassicTheme
	{
		private StyleSheetObjectModel mvarStyleSheet = null;
		public StyleSheetObjectModel StyleSheet { get { return mvarStyleSheet; } set { mvarStyleSheet = value; } }

		public StyleSheetTheme(string fileName)
		{
			mvarStyleSheet = new StyleSheetObjectModel();
			Document.Load(mvarStyleSheet, new CSSDataFormat(), new FileAccessor(fileName));
		}

		private void RenderStyleSheetRules(StyleSheetRule[] rules, Graphics graphics, Rectangle rectangle)
		{
			foreach (StyleSheetRule rule in rules)
			{
				RenderStyleSheetRule(rule, graphics, rectangle);
			}
		}

		private void RenderStyleSheetRule(StyleSheetRule rule, Graphics graphics, Rectangle rectangle)
		{
		}

		private static string MakeCascadingStyleSheetSelector(string selector, ControlState state)
		{
			string css_state = String.Empty;
			switch (state)
			{
				case ControlState.Hover:
					{
						css_state = "hover";
						break;
					}
				case ControlState.Pressed:
					{
						css_state = "active";
						break;
					}
			}
			return selector + (String.IsNullOrEmpty(css_state) ? String.Empty : ":" + css_state);
		}

		public override void DrawDropDownBackground(Graphics graphics, Rectangle rectangle, ControlState state)
		{
			StyleSheetRule[] rules = mvarStyleSheet.GetRulesForSelector(MakeCascadingStyleSheetSelector("dropdown", state));
			RenderStyleSheetRules(rules, graphics, rectangle);
		}

		public override void DrawDropDownButton(Graphics graphics, Rectangle rectangle, ControlState dropdownState, ControlState buttonState)
		{
			// TODO: Figure out how to handle both the dropdown state and the button state a la
			// dropdown { button { } }
			StyleSheetRule[] rules = mvarStyleSheet.GetRulesForSelector(MakeCascadingStyleSheetSelector("dropdownbutton", dropdownState));
			RenderStyleSheetRules(rules, graphics, rectangle);
		}

		public override void DrawDropDownMenuBackground(Graphics graphics, Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		public override void DrawButtonBackground(Graphics g, Rectangle rect, ControlState state)
		{
			throw new NotImplementedException();
		}

		public override void DrawTextBoxBackground(Graphics g, Rectangle rect, ControlState state)
		{
			throw new NotImplementedException();
		}

		public override void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused)
		{
			throw new NotImplementedException();
		}

		public override void DrawListSelectionRectangle(Graphics g, Rectangle rect)
		{
			throw new NotImplementedException();
		}

		public override void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted)
		{
			throw new NotImplementedException();
		}

		public override void DrawListViewTreeGlyph(Graphics g, Rectangle rect, ControlState state, bool expanded)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarBackground(Graphics g, Rectangle rect, Orientation orientation)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarChunk(Graphics g, Rectangle rect, Orientation orientation)
		{
			throw new NotImplementedException();
		}

		public override void DrawProgressBarPulse(Graphics g, Rectangle rect, Orientation orientation)
		{
			throw new NotImplementedException();
		}
	}
}
