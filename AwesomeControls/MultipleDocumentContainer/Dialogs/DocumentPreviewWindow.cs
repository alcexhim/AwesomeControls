using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.MultipleDocumentContainer.Dialogs
{
    public partial class DocumentPreviewWindow : Form
    {
        public DocumentPreviewWindow()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
            base.Opacity = 0;
        }

        private bool animationInProgress = false;

        private bool mvarAnimate = true;
        public bool Animate { get { return mvarAnimate; } set { mvarAnimate = value; } }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!animationInProgress && mvarAnimate)
            {
                animationInProgress = true;
                for (int i = 0; i < 100; i++)
                {
                    base.Opacity = (double)i / 100;
                    Application.DoEvents();

                    System.Threading.Thread.Sleep(2);
                }
                animationInProgress = false;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!animationInProgress && mvarAnimate)
            {
                animationInProgress = true;
                for (int i = 100; i >= 0; i--)
                {
                    base.Opacity = (double)i / 100;
                    Application.DoEvents();

                    System.Threading.Thread.Sleep(2);
                }
                animationInProgress = false;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; pnlFileName.Refresh(); } }

        private string BadWrap(string input, Rectangle bounds, char[] wrapAt = null)
        {
            string currentString = String.Empty;
            string currentLine = String.Empty;

            if (wrapAt != null)
            {
                string[] wrapPoints = input.Split(wrapAt);
                for (int i = 0; i < wrapPoints.Length; i++)
                {
                    currentString += wrapPoints[i];
                    if (i < wrapPoints.Length - 1)
                    {
                        currentString += wrapAt[0];
                    }

                    currentLine += wrapPoints[i];

                    if (i < input.Length - 1)
                    {
                        if (TextRenderer.MeasureText(currentLine + wrapPoints[i].ToString(), Font).Width >= bounds.Width)
                        {
                            currentString += "\r\n";
                            currentLine = String.Empty;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    currentString += input[i];
                    currentLine += input[i];

                    if (i < input.Length - 1)
                    {
                        if (TextRenderer.MeasureText(currentLine + input[i].ToString(), Font).Width >= bounds.Width)
                        {
                            currentString += "\r\n";
                            currentLine = String.Empty;
                        }
                    }
                }
            }
            return currentString;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle textRect = new Rectangle(8, 8, pnlFileName.Width - 16, pnlFileName.Height - 16);

            TextRenderer.DrawText(e.Graphics, BadWrap(mvarFileName, textRect, new char[] { '\\' }), Font, textRect, Color.Black, TextFormatFlags.Default);
        }
    }
}
