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
	[DefaultEvent("RequestItemProperties")]
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

		public bool ShowGridLines
		{
			get { return lv.ShowGridLines; }
			set { lv.ShowGridLines = value; }
		}
		public bool HideSelection
		{
			get { return lv.HideSelection; }
			set { lv.HideSelection = value; }
		}
		public bool FullRowSelect
		{
			get { return lv.FullRowSelect; }
			set { lv.FullRowSelect = value; }
		}
		public bool MultiSelect
		{
			get { return lv.MultiSelect; }
			set { lv.MultiSelect = value; }
		}
		public ListView.ListViewItem.ListViewItemCollection Items
		{
			get { return lv.Items; }
		}
		public ListView.ListViewItem.ListViewItemCollection SelectedItems
		{
			get { return lv.SelectedItems; }
		}

		private string mvarItemNameSingular = "item";
		public string ItemNameSingular { get { return mvarItemNameSingular; } set { mvarItemNameSingular = value; } }
		private string mvarItemNamePlural = "items";
		public string ItemNamePlural { get { return mvarItemNamePlural; } set { mvarItemNamePlural = value; } }

		public event ItemPropertiesEventHandler RequestItemProperties;
		protected virtual void OnRequestItemProperties(ItemPropertiesEventArgs e)
		{
			if (RequestItemProperties != null) RequestItemProperties(this, e);
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

			bool allowItemRemove = (mvarAllowItemRemove && lv.SelectedItems.Count > 0);
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

			bool allowItemClear = (mvarAllowItemRemove && lv.Items.Count > 0);
			foreach (ListView.ListViewItem lvi in lv.Items)
			{
				// determine if ALL the items in the selection are eligible to be removed
				ItemEligibilityEventArgs e = new ItemEligibilityEventArgs(lvi);
				OnCheckItemRemoveEligibility(e);
				if (!e.Eligible)
				{
					allowItemClear = false;
					break;
				}
			}

			tsbClear.Visible = mvarAllowItemRemove;
			tsbRemove.Enabled = allowItemClear;

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

			ListView.ListViewItem lvi = new ListView.ListViewItem();
			
			ItemPropertiesEventArgs ee = new ItemPropertiesEventArgs(lvi);
			OnRequestItemProperties(ee);
			if (!ee.Cancel)
			{
				lv.Items.Add(ee.Item);
			}
		}

		private void tsbModify_Click(object sender, EventArgs e)
		{
			if (!mvarAllowItemModify) return;
			OnRequestItemProperties(new ItemPropertiesEventArgs(lv.SelectedItems[0]));
		}

		private void tsbRemove_Click(object sender, EventArgs e)
		{
			if (!mvarAllowItemRemove) return;
			if (lv.SelectedItems.Count <= 0) return;

			if (MessageBox.Show("Are you sure you wish to remove the selected " + GetItemName(lv.SelectedItems.Count) + "?", "Remove " + GetItemName(lv.SelectedItems.Count).Capitalize(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			while (lv.SelectedItems.Count > 0)
			{
				lv.SelectedItems[0].Remove();
			}
			RefreshButtons();
		}

		private string GetItemName(int count)
		{
			if (count == 1) return mvarItemNameSingular;
			return mvarItemNamePlural;
		}

		private void tsbClear_Click(object sender, EventArgs e)
		{
			if (!mvarAllowItemRemove) return;
			if (lv.SelectedItems.Count <= 0) return;

			if (MessageBox.Show("Are you sure you wish to remove all " + GetItemName(lv.SelectedItems.Count) + "?", "Remove " + GetItemName(lv.SelectedItems.Count).Capitalize(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			while (lv.SelectedItems.Count > 0)
			{
				lv.SelectedItems[0].Remove();
			}
			RefreshButtons();
		}

		private void tsbMoveUp_Click(object sender, EventArgs e)
		{

		}

		private void tsbMoveDown_Click(object sender, EventArgs e)
		{

		}
	}
}
