using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.ComponentTray
{
	public partial class ComponentTrayControl : UserControl
	{
		public ComponentTrayControl()
		{
			InitializeComponent();

			mvarComponents = new Component.ComponentCollection(this);
			base.DoubleBuffered = true;
		}

		private Dictionary<Component, Rectangle> componentBounds = new Dictionary<Component, Rectangle>();

		private Component.ComponentCollection mvarComponents = null;
		public Component.ComponentCollection Components { get { return mvarComponents; } }

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			bool found = false;
			foreach (Component cmp in mvarComponents)
			{
				Rectangle rect = GetComponentBounds(cmp);
				if (rect.Contains(e.Location))
				{
					mvarHoverComponent = cmp;
					found = true;
					break;
				}
			}

			if (found)
			{
				Cursor = Cursors.SizeAll;
			}
			else
			{
				Cursor = Cursors.Default;
				mvarHoverComponent = null;
			}
			Refresh();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			mvarHoverComponent = null;
			Refresh();
		}

		private Component mvarHoverComponent = null;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			foreach (Component component in mvarComponents)
			{
				Rectangle rect = GetComponentBounds(component);
				Rectangle imageRect = rect;
				imageRect.X += 9;
				imageRect.Y += 6;
				imageRect.Width = 16;
				imageRect.Height = 16;

				Size textSize = TextRenderer.MeasureText(component.Title, Font);

				Rectangle textRect = rect;
				textRect.X += (8 + 16 + 4);
				textRect.Y += 11;
				textRect.Size = textSize;

				if (component.Image != null) e.Graphics.DrawImage(component.Image, imageRect);

				TextFormatFlags flags = TextFormatFlags.Default;
				TextRenderer.DrawText(e.Graphics, component.Title, Font, textRect, ForeColor, flags);
			}
		}

		private Rectangle GetComponentBounds(Component component)
		{
			if (!componentBounds.ContainsKey(component))
			{
				// TODO: calculate component bounds for this component
				Rectangle rectBounds = new Rectangle(0, 0, 0, 0);
				Size textSize = TextRenderer.MeasureText(component.Title, Font);
				rectBounds.Height = 28;
				rectBounds.Width = (8 + 16 + 4) + textSize.Width + 14;
				componentBounds.Add(component, rectBounds);
			}
			return componentBounds[component];
		}
		private void SetComponentBounds(Component component, Rectangle rect)
		{
			componentBounds[component] = rect;
		}
	}
}
