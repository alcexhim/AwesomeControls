using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AwesomeControls.MultipleDocumentContainer.Dialogs;

namespace AwesomeControls.MultipleDocumentContainer
{
	[DefaultEvent("DocumentChanged")]
	public partial class MultipleDocumentContainerControl : UserControl
	{
		public event MultipleDocumentContainerDocumentChangingEventHandler DocumentChanging;
		protected virtual void OnDocumentChanging(MultipleDocumentContainerDocumentChangingEventArgs e)
		{
			if (DocumentChanging != null) DocumentChanging(this, e);
		}
		
		public event EventHandler DocumentChanged;
		protected virtual void OnDocumentChanged(EventArgs e)
		{
			if (DocumentChanged != null) DocumentChanged(this, e);
		}

		public event DocumentClosingEventHandler DocumentClosing;
		protected virtual void OnDocumentClosing(DocumentClosingEventArgs e)
		{
			if (DocumentClosing != null) DocumentClosing(this, e);
		}

		public event DocumentClosedEventHandler DocumentClosed;
		protected virtual void OnDocumentClosed(DocumentClosedEventArgs e)
		{
			if (DocumentClosed != null) DocumentClosed(this, e);
		}

		public MultipleDocumentContainerControl()
		{
			InitializeComponent();
			mvarDocuments = new Document.DocumentCollection(this);
			messageFilter = new MultipleDocumentContainerMessageFilter(this);
		}

		private MultipleDocumentContainerMessageFilter messageFilter = null;
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			System.Windows.Forms.Application.AddMessageFilter(messageFilter);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			System.Windows.Forms.Application.RemoveMessageFilter(messageFilter);
		}

		private Document.DocumentCollection mvarDocuments = null;
		public Document.DocumentCollection Documents { get { return mvarDocuments; } }

		#region Event Handlers

		void tsb_MouseDown(object sender, MouseEventArgs e)
		{
			ToolStripButton tsb = (sender as ToolStripButton);
			if (tsb == null) return;

			Document doc = (tsb.Tag as Document);
			if (doc == null) return;

			if (e.Button == System.Windows.Forms.MouseButtons.Middle)
			{
				Close(doc);
			}
			else
			{
				SwitchTo(doc);
			}
		}

		void tsb_MouseUp(object sender, MouseEventArgs e)
		{
			ToolStripButton tsb = (sender as ToolStripButton);
			if (tsb == null) return;

			Document doc = (tsb.Tag as Document);
			if (doc == null) return;

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				mnuDocumentTabContextSave.Text = "&Save " + doc.Title;
				mnuDocumentTabContext.Show(Cursor.Position);
			}
		}

		#endregion

		public void Close(Document item)
		{
			if (item == null) return;

			if (_previewWnd != null && _previewWnd.Visible) _previewWnd.Close();

			DocumentClosingEventArgs dce = new DocumentClosingEventArgs(item);
			OnDocumentClosing(dce);

			if (dce.Cancel) return;

			MultipleDocumentContainerDocumentChangingEventArgs e = new MultipleDocumentContainerDocumentChangingEventArgs(mvarCurrentDocument, item);
			OnDocumentChanging(e);
			if (e.Cancel) return;

			mvarDocuments.Remove(mvarCurrentDocument);

			if (mvarDocuments.Count > 0)
			{
				SwitchToInternal(mvarDocuments[mvarDocuments.Count - 1]);
			}
			else
			{
				mvarCurrentDocument = null;
			}
			OnDocumentChanged(EventArgs.Empty);

			OnDocumentClosed(new DocumentClosedEventArgs(item));
		}

		public void SwitchTo(Document item)
		{
			MultipleDocumentContainerDocumentChangingEventArgs e = new MultipleDocumentContainerDocumentChangingEventArgs(mvarCurrentDocument, item);
			OnDocumentChanging(e);
			if (e.Cancel) return;

			item = e.NewDocument;
			SwitchToInternal(item);

			OnDocumentChanged(EventArgs.Empty);
		}
		private void SwitchToInternal(Document item)
		{
			foreach (Control control in pnlControlContainer.Controls)
			{
				if (control == item.Control)
				{
					control.Enabled = true;
					control.Visible = true;
				}
				else
				{
					control.Visible = false;
					control.Enabled = false;
				}
			}

			foreach (ToolStripItem tsi in tbWindowSwitcher.Items)
			{
				ToolStripButton tsb = (tsi as ToolStripButton);
				if (tsb == null) continue;

				tsb.Checked = (tsb.Tag == item);
			}
			mvarCurrentDocument = item;
		}

		#region Collection Processing
		private Dictionary<Document, ToolStripButton> tsbsByDocument = new Dictionary<Document, ToolStripButton>();

		internal void ClearItems()
		{
			pnlControlContainer.Controls.Clear();
			tbWindowSwitcher.Items.Clear();
			tsbsByDocument.Clear();
		}

		internal void InsertItem(Document item)
		{
			if (!pnlControlContainer.Controls.Contains(item.Control)) pnlControlContainer.Controls.Add(item.Control);
			item.Control.Dock = DockStyle.Fill;

			ToolStripButton tsb = new ToolStripButton();
			tsb.Text = item.Title;
			tsb.ToolTipText = item.ToolTipText;
			if (item.Image != null)
			{
				tsb.Image = item.Image;
			}
			tsb.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			tsb.Tag = item;
			tsb.MouseDown += tsb_MouseDown;
			tsb.MouseMove += tsb_MouseMove;
			tsb.MouseUp += tsb_MouseUp;
			tsb.MouseHover += tsb_MouseHover;
			tsb.MouseLeave += tsb_MouseLeave;
			tbWindowSwitcher.Items.Add(tsb);

			tsbsByDocument.Add(item, tsb);

			SwitchTo(item);
		}

		private DocumentPreviewWindow _previewWnd = null;
		private bool mvarEnableWindowPreviews = true;
		public bool EnableWindowPreviews { get { return mvarEnableWindowPreviews; } set { mvarEnableWindowPreviews = value; tbWindowSwitcher.ShowItemToolTips = !mvarEnableWindowPreviews; } }

		private void tsb_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ToolStripButton tsb = (sender as ToolStripButton);
				if (tsb == null) return;

				Point pt = tbWindowSwitcher.PointToScreen(e.Location);
				Rectangle rect = tbWindowSwitcher.RectangleToScreen(tsb.Bounds);

				Cursor = Cursors.SizeAll;
				if (pt.Y > rect.Bottom || pt.X > rect.Right)
				{
					// Duplicate the form
					Type type = ParentForm.GetType();
					Form f = (type.Assembly.CreateInstance(type.FullName) as Form);
					f.Show();

				}
			}
		}

		private void tsb_MouseLeave(object sender, EventArgs e)
		{
			if (_previewWnd != null)
			{
				_previewWnd.Close();
				_previewWnd = null;
			}
			Cursor = Cursors.Default;
		}

		private void tsb_MouseHover(object sender, EventArgs e)
		{
			if (_previewWnd != null)
			{
				_previewWnd.Close();
				_previewWnd = null;
			}

			ToolStripButton tsb = (sender as ToolStripButton);

			_previewWnd = new DocumentPreviewWindow();
			
			Document doc = (tsb.Tag as Document);
			if (doc.Control != null)
			{
				Rectangle rect = _previewWnd.picPreview.ClientRectangle;
				Bitmap bitmap = new Bitmap(doc.Control.Size.Width, doc.Control.Size.Height);
				doc.Control.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), doc.Control.Size));
				_previewWnd.picPreview.Image = bitmap;
				_previewWnd.FileName = doc.ToolTipText;

				_previewWnd.Location = PointToScreen(tsb.Bounds.Location);
				_previewWnd.Top += Cursor.Size.Height;
				_previewWnd.Show(FindForm());
			}
		}

		internal void RemoveItem(Document item)
		{
			if (pnlControlContainer.Controls.Contains(item.Control)) pnlControlContainer.Controls.Remove(item.Control);
			if (tsbsByDocument.ContainsKey(item))
			{
				tbWindowSwitcher.Items.Remove(tsbsByDocument[item]);
			}
			tsbsByDocument.Remove(item);
		}

		internal void UpdateItem(Document item)
		{
			foreach (ToolStripItem tsi in tbWindowSwitcher.Items)
			{
				if (tsi.Tag == item)
				{
					tsi.Text = item.Title;
					if (item.Image != null) tsi.Image = item.Image;
					break;
				}
			}
		}
		#endregion

		private void pnlControlContainer_Paint(object sender, PaintEventArgs e)
		{

			Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
			Theming.Theme.CurrentTheme.DrawContentAreaBackground(e.Graphics, rect);

		}

		private Document mvarCurrentDocument = null;
		public Document CurrentDocument
		{
			get { return mvarCurrentDocument; }
			set
			{
				MultipleDocumentContainerDocumentChangingEventArgs e = new MultipleDocumentContainerDocumentChangingEventArgs(mvarCurrentDocument, value);
				OnDocumentChanging(e);
				if (e.Cancel) return;

				value = e.NewDocument;

				if (mvarDocuments.Contains(value))
				{
					SwitchToInternal(value);

					OnDocumentChanged(EventArgs.Empty);
				}
			}
		}

		private void mnuDocumentTabContextClose_Click(object sender, EventArgs e)
		{
			if (mvarCurrentDocument != null)
			{
				Close(mvarCurrentDocument);
			}
		}
		private void mnuDocumentTabContextCloseAll_Click(object sender, EventArgs e)
		{
			while (mvarDocuments.Count > 0)
			{
				Close(mvarDocuments[0]);
			}
		}
		private void mnuDocumentTabContextCloseAllButThis_Click(object sender, EventArgs e)
		{
			int i = 0;
			while (mvarDocuments.Count > 1)
			{
				if (mvarDocuments[i] == mvarCurrentDocument)
				{
					i++;
					continue;
				}

				Close(mvarDocuments[i]);
			}
		}

		private WindowListDialog dlgWindowList = null;
		private void PrepareWindowListDialog()
		{
			if (dlgWindowList == null) dlgWindowList = new WindowListDialog();
			if (dlgWindowList.IsDisposed) dlgWindowList = new WindowListDialog();
		}

		public bool IsWindowListDialogVisible
		{
			get
			{
				PrepareWindowListDialog();
				return dlgWindowList.Visible;
			}
		}
		public void HideWindowListDialog()
		{
			PrepareWindowListDialog();
			if (dlgWindowList.SelectedDocument != null)
			{
				CurrentDocument = dlgWindowList.SelectedDocument;
			}
			dlgWindowList.Close();

			if (dlgWindowList.SelectedDocument != null) CurrentDocument = dlgWindowList.SelectedDocument;
		}
		public void ShowWindowListDialog(bool fromKeyboard = false)
		{
			PrepareWindowListDialog();
			if (dlgWindowList.Visible) return;
			
			if (fromKeyboard) dlgWindowList.ShowInTaskbar = false;

			dlgWindowList.Documents.Clear();
			foreach (Document d in mvarDocuments)
			{
				dlgWindowList.Documents.Add(d);
			}

			int index = mvarDocuments.IndexOf(mvarCurrentDocument);
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				index--;
				if (index < 0) index = mvarDocuments.Count - 1;
			}
			else
			{
				index++;
				if (index > mvarDocuments.Count - 1) index = 0;
			}
			dlgWindowList.SelectedDocument = mvarDocuments[index];

			dlgWindowList.ShowDialog();
		}

		public bool IsActive
		{
			get
			{
				bool active = FindForm().ContainsFocus;
				return active;
			}
		}
	}
}
