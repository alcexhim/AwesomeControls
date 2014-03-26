using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Wizard
{
	internal partial class WizardForm : Form
	{
		public WizardForm()
		{
			InitializeComponent();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			lblTitle.Font = new Font(base.Font, FontStyle.Bold);
		}

		private string mvarBrandingText = String.Empty;
		public string BrandingText
		{
			get { return mvarBrandingText; }
			set
			{
				mvarBrandingText = value;
				if (String.IsNullOrEmpty(mvarBrandingText))
				{
					lblBrandingText.Visible = false;
				}
				else
				{
					lblBrandingText.Visible = true;
					lblBrandingText.Text = mvarBrandingText;
				}
			}
		}

		public WizardDialog ParentDialog = null;

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			pictureBox1.Image = this.Icon.ToBitmap();

			foreach (WizardPanel wizpanel in ParentDialog.Panels)
			{
				ContentArea.Controls.Add(wizpanel.Control);
				wizpanel.Control.Dock = DockStyle.Fill;
				wizpanel.Control.Visible = false;
			}

			CurrentPanelIndex = 0;
		}

		private int mvarCurrentPanelIndex = -1;
		public int CurrentPanelIndex
		{
			get { return mvarCurrentPanelIndex; }
			set
			{
				if (value > -2 && value < ParentDialog.Panels.Count)
				{
					mvarCurrentPanelIndex = value;

					foreach (WizardPanel wiz in ParentDialog.Panels)
					{
						if (wiz.Index == value)
						{
							wiz.Control.Enabled = true;
							wiz.Control.Visible = true;

							lblTitle.Text = wiz.Title;
							lblDescription.Text = wiz.Description;
						}
						else
						{
							wiz.Control.Enabled = false;
							wiz.Control.Visible = false;

							lblTitle.Text = String.Empty;
							lblDescription.Text = String.Empty;
						}
					}
				}
			}
		}

		private void cmdBack_Click(object sender, EventArgs e)
		{

		}

		private void cmdNext_Click(object sender, EventArgs e)
		{

		}
	}
}
