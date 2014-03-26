using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AwesomeControls.Designer;

namespace AwesomeControls.TestProject
{
    namespace DesignObjectClasses
    {
        public class UGModelDesignClass
            : DesignerObjectClass
        {
            protected override void RenderClientArea(DesignerObjectPaintEventArgs e)
            {
                Point pt = new Point(e.Item.Left + 20, e.Item.Top + 20);
                // e.Graphics.DrawString(e.Item.Name, SystemFonts.MenuFont, Brushes.Black, pt);
                TextRenderer.DrawText(e.Graphics, e.Item.Name, SystemFonts.MenuFont, pt, Color.Blue);
            }
            protected override void RenderNonClientArea(DesignerObjectPaintEventArgs e)
            {
                Rectangle titleBarRect = new Rectangle(e.Item.Bounds.Left, e.Item.Bounds.Top, e.Item.Bounds.Width, 16);
                e.Graphics.FillRectangle(Brushes.Red, titleBarRect);
                e.Graphics.DrawRectangle(Pens.Red, e.Item.Bounds);
            }
        }
        public class ButtonDesignerObjectClass
            : DesignerObjectClass
        {
            protected override void RenderClientArea(DesignerObjectPaintEventArgs e)
            {
                if (e.Item.Properties.ContainsKey("Text"))
                {
                    TextRenderer.DrawText(e.Graphics, e.Item.Properties["Text"].ToString(), SystemFonts.MenuFont, e.Item.Bounds, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
            protected override void RenderNonClientArea(DesignerObjectPaintEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), e.Item.Bounds);
                DrawingTools.DrawRaisedBorder(e.Graphics, e.Item.Bounds);
            }
        }
    }

    public partial class DesignerTest : Form
    {
        private Dictionary<Designer.DesignerObject, TextBoxTest> textBoxesForDesignerObjects = new Dictionary<Designer.DesignerObject, TextBoxTest>();

        public DesignerTest()
        {
            InitializeComponent();

            Designer.DesignerAreas.GenericDesignerArea area = new Designer.DesignerAreas.GenericDesignerArea();

            DesignerObject obj1 = new DesignerObject("obj1", new DesignObjectClasses.UGModelDesignClass());
            obj1.Bounds = new Rectangle(13, 13, 75, 23);
            obj1.Locked = true;
            area.Objects.Add(obj1);

            DesignerObject obj2 = new DesignerObject("obj2", new DesignObjectClasses.UGModelDesignClass());
            obj2.Bounds = new Rectangle(80, 80, 80, 200);
            area.Objects.Add(obj2);

            designer.Areas.Add(area);
        }

        private void optObjectType_CheckedChanged(object sender, EventArgs e)
        {
            if (optObjectTypeButton.Checked)
            {
                designer.EnableCreation = true;
                designer.DefaultObjectClass = new DesignObjectClasses.ButtonDesignerObjectClass();
            }
        }

        private void designer_DesignerObjectMouseDoubleClick(object sender, DesignerObjectMouseEventArgs e)
        {
            TextBoxTest textbox = null;
            if (textBoxesForDesignerObjects.ContainsKey(e.Item))
            {
                textbox = textBoxesForDesignerObjects[e.Item];
                if (textbox == null) textbox = new TextBoxTest();
                if (textbox.IsDisposed) textbox = new TextBoxTest();
            }
            else
            {
                textbox = new TextBoxTest();
            }
            textBoxesForDesignerObjects[e.Item] = textbox;

            if (!textbox.Visible)
            {
                textbox.Show();
            }
            else
            {
                textbox.Focus();
            }
        }
    }
}
