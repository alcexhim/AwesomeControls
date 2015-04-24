using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.CollectionListView
{
	public partial class CollectionListViewControl : UserControl
	{
		public CollectionListViewControl()
		{
			InitializeComponent();

			tsbAdd.Image = Theming.Theme.CurrentTheme.GetImage("Buttons/ListViewItemAdd.png");
			tsbModify.Image = Theming.Theme.CurrentTheme.GetImage("Buttons/ListViewItemModify.png");
			tsbRemove.Image = Theming.Theme.CurrentTheme.GetImage("Buttons/ListViewItemRemove.png");
			tsbClear.Image = Theming.Theme.CurrentTheme.GetImage("Buttons/ListViewItemClear.png");
		}

		private bool mvarAllowItemInsert = true;
		public bool AllowItemInsert
		{
			get { return mvarAllowItemInsert; }
			set 
			{
				mvarAllowItemInsert = value;
				RefreshButtons();
			}
		}

		private bool mvarAllowItemModify = true;
		public bool AllowItemModify
		{
			get { return mvarAllowItemModify; }
			set
			{
				mvarAllowItemModify = value;
				RefreshButtons();
			}
		}

		private bool mvarAllowItemRemove = true;
		public bool AllowItemRemove
		{
			get { return mvarAllowItemRemove; }
			set
			{
				mvarAllowItemRemove = value;
				RefreshButtons();
			}
		}

		private bool mvarAllowItemReorder = false;
		public bool AllowItemReorder
		{
			get { return mvarAllowItemReorder; }
			set
			{
				mvarAllowItemReorder = value;
				RefreshButtons();
			}
		}

		protected virtual void OnDisplayItemProperties(ItemPropertiesEventArgs e)
		{

		}

		public event ItemEligibilityEventHandler CheckItemModifyEligibility;
		protected virtual void OnCheckItemModifyEligibility(ItemEligibilityEventArgs e)
		{
			if (CheckItemModifyEligibility != null) CheckItemModifyEligibility(this, e);
		}

		public event ItemEligibilityEventHandler CheckItemRemoveEligibility;
		protected virtual void OnCheckItemRemoveEligibility(ItemEligibilityEventArgs e)
		{
			if (CheckItemRemoveEligibility != null) CheckItemRemoveEligibility(this, e);
		}

		private void RefreshButtons()
		{
			tsbAdd.Visible = mvarAllowItemInsert;
			tsbAdd.Enabled = mvarAllowItemInsert;
			
			bool allowItemModify = (mvarAllowItemModify && lv.SelectedItems.Count == 1);
			if (lv.SelectedItems.Count == 1)
			{
				// determine if the item in the selection is eligible to be modified
				ItemEligibilityEventArgs e = new ItemEligibilityEventArgs(lv.SelectedItems[0]);
				OnCheckItemRemoveEligibility(e);
				if (!e.Eligible) allowItemModify = false;
			}
			tsbModify.Visible = mvarAllowItemModify;
			tsbModify.Enabled = allowItemModify;

			bool allowItemRemove = (mvarAllowItemRemove && lv.SelectedItems.Count >= 1);
			foreach (ListView.ListViewItem lvi in lv.SelectedItems)
			{
				// determine if ALL the items in the selection are eligible to be removed
				ItemEligibilityEventArgs e = new ItemEligibilityEventArgs(lvi);
				OnCheckItemRemoveEligibility(e);
				if (!e.Eligible)
				{
					allowItemRemove = false;
					break;
				}
			}
			tsbRemove.Visible = mvarAllowItemRemove;
			tsbRemove.Enabled = allowItemRemove;

			tsbSep1.Visible = mvarAllowItemReorder;
			tsbMoveUp.Visible = mvarAllowItemReorder;
			tsbMoveDown.Visible = mvarAllowItemReorder;

			tsbMoveUp.Enabled = (mvarAllowItemReorder && lv.Items.IndexOf(lv.SelectedItems[0]) > 0);
			tsbMoveDown.Enabled = (mvarAllowItemReorder && lv.Items.IndexOf(lv.SelectedItems[lv.SelectedItems.Count - 1]) < lv.Items.Count - 1);
		}

		private void lv_ItemActivate(object sender, EventArgs e)
		{
			tsbModify_Click(sender, e);
		}

		private void lv_SelectionChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void tsbAdd_Click(object sender, EventArgs e)
		{
			if (!mvarAllowItemInsert) return;
			OnDisplayItemProperties(new ItemPropertiesEventArgs(null));
		}

		private void tsbModify_Click(object sender, EventArgs e)
		{
			if (!mvarAllowItemModify) return;
			OnDisplayItemProperties(new ItemPropertiesEventArgs(null));
		}

		private void tsbRemove_Click(object sender, EventArgs e)
		{

		}

		private void tsbClear_Click(object sender, EventArgs e)
		{

		}

		private void tsbMoveUp_Click(object sender, EventArgs e)
		{

		}

		private void tsbMoveDown_Click(object sender, EventArgs e)
		{

		}
	}
}
