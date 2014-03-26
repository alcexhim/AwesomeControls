namespace AwesomeControls.Toolbox
{
	partial class ToolboxControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General goes here");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("General", new System.Windows.Forms.TreeNode[] {
            treeNode1});
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Blank");
			this.tvToolbox = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// tvToolbox
			// 
			this.tvToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvToolbox.FullRowSelect = true;
			this.tvToolbox.Location = new System.Drawing.Point(0, 0);
			this.tvToolbox.Name = "tvToolbox";
			treeNode1.Name = "nodeGeneral1";
			treeNode1.Text = "General goes here";
			treeNode2.Name = "nodeGeneral";
			treeNode2.Text = "General";
			treeNode3.Name = "nodeBlank";
			treeNode3.Text = "Blank";
			this.tvToolbox.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
			this.tvToolbox.ShowLines = false;
			this.tvToolbox.ShowPlusMinus = false;
			this.tvToolbox.ShowRootLines = false;
			this.tvToolbox.Size = new System.Drawing.Size(377, 310);
			this.tvToolbox.TabIndex = 0;
			// 
			// ToolboxControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tvToolbox);
			this.Name = "ToolboxControl";
			this.Size = new System.Drawing.Size(377, 310);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvToolbox;
	}
}
