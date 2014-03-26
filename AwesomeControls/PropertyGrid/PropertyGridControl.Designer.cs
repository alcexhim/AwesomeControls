namespace AwesomeControls.PropertyGrid
{
    partial class PropertyGridControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyGridControl));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cboObject = new System.Windows.Forms.ComboBox();
			this.scPropertiesDescription = new System.Windows.Forms.SplitContainer();
			this.scPropertiesCommands = new System.Windows.Forms.SplitContainer();
			this.pnlCommands = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.lblPropertyName = new System.Windows.Forms.Label();
			this.tb = new AwesomeControls.CommandBars.CBToolBar();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.propertyGridPanel1 = new AwesomeControls.PropertyGrid.PropertyGridPanel();
			this.tsbCategorized = new System.Windows.Forms.ToolStripButton();
			this.tsbAlphabetical = new System.Windows.Forms.ToolStripButton();
			this.tsbProperties = new System.Windows.Forms.ToolStripButton();
			this.tsbEvents = new System.Windows.Forms.ToolStripButton();
			this.tsbPropertyPages = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.scPropertiesDescription.Panel1.SuspendLayout();
			this.scPropertiesDescription.Panel2.SuspendLayout();
			this.scPropertiesDescription.SuspendLayout();
			this.scPropertiesCommands.Panel1.SuspendLayout();
			this.scPropertiesCommands.Panel2.SuspendLayout();
			this.scPropertiesCommands.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tb.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.cboObject, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tb, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.scPropertiesDescription, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(297, 302);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// cboObject
			// 
			this.cboObject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboObject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cboObject.FormattingEnabled = true;
			this.cboObject.Location = new System.Drawing.Point(3, 3);
			this.cboObject.Name = "cboObject";
			this.cboObject.Size = new System.Drawing.Size(291, 21);
			this.cboObject.TabIndex = 1;
			this.cboObject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboObject_DrawItem);
			this.cboObject.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			// 
			// scPropertiesDescription
			// 
			this.scPropertiesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scPropertiesDescription.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scPropertiesDescription.Location = new System.Drawing.Point(3, 55);
			this.scPropertiesDescription.Name = "scPropertiesDescription";
			this.scPropertiesDescription.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scPropertiesDescription.Panel1
			// 
			this.scPropertiesDescription.Panel1.Controls.Add(this.scPropertiesCommands);
			// 
			// scPropertiesDescription.Panel2
			// 
			this.scPropertiesDescription.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.scPropertiesDescription.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.sc_Panel_Paint);
			this.scPropertiesDescription.Size = new System.Drawing.Size(291, 244);
			this.scPropertiesDescription.SplitterDistance = 190;
			this.scPropertiesDescription.TabIndex = 3;
			this.scPropertiesDescription.Paint += new System.Windows.Forms.PaintEventHandler(this.scPropertiesDescription_Paint);
			// 
			// scPropertiesCommands
			// 
			this.scPropertiesCommands.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scPropertiesCommands.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scPropertiesCommands.Location = new System.Drawing.Point(0, 0);
			this.scPropertiesCommands.Name = "scPropertiesCommands";
			this.scPropertiesCommands.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scPropertiesCommands.Panel1
			// 
			this.scPropertiesCommands.Panel1.Controls.Add(this.propertyGridPanel1);
			this.scPropertiesCommands.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.sc_Panel_Paint);
			// 
			// scPropertiesCommands.Panel2
			// 
			this.scPropertiesCommands.Panel2.Controls.Add(this.pnlCommands);
			this.scPropertiesCommands.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.sc_Panel_Paint);
			this.scPropertiesCommands.Size = new System.Drawing.Size(291, 190);
			this.scPropertiesCommands.SplitterDistance = 148;
			this.scPropertiesCommands.TabIndex = 0;
			// 
			// pnlCommands
			// 
			this.pnlCommands.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCommands.Location = new System.Drawing.Point(0, 0);
			this.pnlCommands.Name = "pnlCommands";
			this.pnlCommands.Size = new System.Drawing.Size(291, 38);
			this.pnlCommands.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.lblPropertyName, 0, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(289, 46);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(3, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(283, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "Property Description";
			// 
			// lblPropertyName
			// 
			this.lblPropertyName.AutoSize = true;
			this.lblPropertyName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPropertyName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPropertyName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblPropertyName.Location = new System.Drawing.Point(3, 0);
			this.lblPropertyName.Name = "lblPropertyName";
			this.lblPropertyName.Size = new System.Drawing.Size(283, 20);
			this.lblPropertyName.TabIndex = 0;
			this.lblPropertyName.Text = "Property Name";
			// 
			// tb
			// 
			this.tb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCategorized,
            this.tsbAlphabetical,
            this.toolStripSeparator1,
            this.tsbProperties,
            this.tsbEvents,
            this.toolStripSeparator2,
            this.tsbPropertyPages});
			this.tb.Location = new System.Drawing.Point(0, 26);
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(297, 25);
			this.tb.TabIndex = 2;
			this.tb.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// propertyGridPanel1
			// 
			this.propertyGridPanel1.BackColor = System.Drawing.SystemColors.Window;
			this.propertyGridPanel1.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.propertyGridPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridPanel1.GridColor = System.Drawing.SystemColors.Control;
			this.propertyGridPanel1.Group = null;
			this.propertyGridPanel1.HighlightBackColor = System.Drawing.SystemColors.Highlight;
			this.propertyGridPanel1.HighlightForeColor = System.Drawing.SystemColors.HighlightText;
			this.propertyGridPanel1.ItemHeight = 16;
			this.propertyGridPanel1.Location = new System.Drawing.Point(0, 0);
			this.propertyGridPanel1.Name = "propertyGridPanel1";
			this.propertyGridPanel1.SelectedPropertyIndex = 0;
			this.propertyGridPanel1.Size = new System.Drawing.Size(291, 148);
			this.propertyGridPanel1.SplitterPosition = 0.4D;
			this.propertyGridPanel1.TabIndex = 0;
			this.propertyGridPanel1.View = AwesomeControls.PropertyGrid.PropertyGridView.Unsorted;
			// 
			// tsbCategorized
			// 
			this.tsbCategorized.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCategorized.Image = ((System.Drawing.Image)(resources.GetObject("tsbCategorized.Image")));
			this.tsbCategorized.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCategorized.Name = "tsbCategorized";
			this.tsbCategorized.Size = new System.Drawing.Size(23, 22);
			this.tsbCategorized.Text = "Categorized";
			// 
			// tsbAlphabetical
			// 
			this.tsbAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAlphabetical.Image = ((System.Drawing.Image)(resources.GetObject("tsbAlphabetical.Image")));
			this.tsbAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAlphabetical.Name = "tsbAlphabetical";
			this.tsbAlphabetical.Size = new System.Drawing.Size(23, 22);
			this.tsbAlphabetical.Text = "Alphabetical";
			// 
			// tsbProperties
			// 
			this.tsbProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbProperties.Image")));
			this.tsbProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbProperties.Name = "tsbProperties";
			this.tsbProperties.Size = new System.Drawing.Size(23, 22);
			this.tsbProperties.Text = "Properties";
			// 
			// tsbEvents
			// 
			this.tsbEvents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEvents.Image = ((System.Drawing.Image)(resources.GetObject("tsbEvents.Image")));
			this.tsbEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEvents.Name = "tsbEvents";
			this.tsbEvents.Size = new System.Drawing.Size(23, 22);
			this.tsbEvents.Text = "Events";
			// 
			// tsbPropertyPages
			// 
			this.tsbPropertyPages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbPropertyPages.Image = ((System.Drawing.Image)(resources.GetObject("tsbPropertyPages.Image")));
			this.tsbPropertyPages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPropertyPages.Name = "tsbPropertyPages";
			this.tsbPropertyPages.Size = new System.Drawing.Size(23, 22);
			this.tsbPropertyPages.Text = "Property Pages";
			// 
			// PropertyGridControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Tahoma", 8F);
			this.Name = "PropertyGridControl";
			this.Size = new System.Drawing.Size(297, 302);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.scPropertiesDescription.Panel1.ResumeLayout(false);
			this.scPropertiesDescription.Panel2.ResumeLayout(false);
			this.scPropertiesDescription.ResumeLayout(false);
			this.scPropertiesCommands.Panel1.ResumeLayout(false);
			this.scPropertiesCommands.Panel2.ResumeLayout(false);
			this.scPropertiesCommands.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tb.ResumeLayout(false);
			this.tb.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.ComboBox cboObject;
		private AwesomeControls.CommandBars.CBToolBar tb;
        private System.Windows.Forms.SplitContainer scPropertiesDescription;
        private System.Windows.Forms.SplitContainer scPropertiesCommands;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPropertyName;
        private System.Windows.Forms.Panel pnlCommands;
        private System.Windows.Forms.ToolStripButton tsbCategorized;
        private System.Windows.Forms.ToolStripButton tsbAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbProperties;
        private System.Windows.Forms.ToolStripButton tsbEvents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPropertyPages;
        private PropertyGridPanel propertyGridPanel1;

    }
}
