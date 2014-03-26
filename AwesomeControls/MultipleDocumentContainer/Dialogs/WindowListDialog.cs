using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// 2013-08-28 09:52		fixed bug where selecting a specific document in the
//						window list did not affect the actual selected document

namespace AwesomeControls.MultipleDocumentContainer.Dialogs
{
	public partial class WindowListDialog : Form
	{
		public WindowListDialog()
		{
			InitializeComponent();

			lv.Columns.Add("Title", 331);
			lv.Columns.Add("FileName");
		}

		private Document mvarSelectedDocument = null;
		public Document SelectedDocument { get { return mvarSelectedDocument; } set { mvarSelectedDocument = value; } }

		private List<Document> mvarDocuments = new List<Document>();
		public List<Document> Documents { get { return mvarDocuments; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			lv.Items.Clear();
			foreach (Document doc in mvarDocuments)
			{
				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.Text = doc.Title;
				lvi.Details.Add(doc.ToolTipText);
				lvi.Data = doc;

				if (doc.Control != null)
				{
					Bitmap bmp = new Bitmap(doc.Control.Width, doc.Control.Height);
					doc.Control.DrawToBitmap(bmp, new Rectangle(new Point(0, 0), doc.Control.Size));

					Bitmap bmp2 = bmp.Resize(128, 128);
					imlIcons.Images.Add(bmp2);

					lvi.ImageIndex = mvarDocuments.IndexOf(doc);
				}

				if (doc == mvarSelectedDocument)
				{
					lvi.Selected = true;
				}
				lv.Items.Add(lvi);
			}

			lv.Focus();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			
			if (lv.Items.Count == 0) return;
			if (e.KeyCode == Keys.Tab && e.Control)
			{
				if (lv.SelectedItems.Count == 0)
				{
					if (e.Shift)
					{
						lv.Items[lv.Items.Count - 1].Selected = true;
					}
					else
					{
						lv.Items[0].Selected = true;
					}
					lv.SelectedItems[0].EnsureVisible();
					lv.Refresh();
					return;
				}

				int oldIndex = lv.Items.IndexOf(lv.SelectedItems[0]);
				int currentIndex = oldIndex;
				if (e.Shift)
				{
					currentIndex--;
					if (currentIndex < 0) currentIndex = lv.Items.Count - 1;
				}
				else
				{
					currentIndex++;
					if (currentIndex > lv.Items.Count - 1) currentIndex = 0;
				}

				lv.Items[oldIndex].Selected = false;
				lv.Items[currentIndex].Selected = true;

				if (lv.SelectedItems.Count > 0) lv.SelectedItems[0].EnsureVisible();

				lv.Refresh();
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (lv.SelectedItems.Count > 0) mvarSelectedDocument = (lv.SelectedItems[0].Data as Document);

			base.OnClosing(e);
		}
	}
}
