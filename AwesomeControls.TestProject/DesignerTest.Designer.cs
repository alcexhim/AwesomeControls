namespace AwesomeControls.TestProject
{
    partial class DesignerTest
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sc = new System.Windows.Forms.SplitContainer();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.optObjectTypeButton = new System.Windows.Forms.RadioButton();
            this.designer = new AwesomeControls.Designer.DesignerControl();
            this.sc.Panel1.SuspendLayout();
            this.sc.Panel2.SuspendLayout();
            this.sc.SuspendLayout();
            this.SuspendLayout();
            // 
            // sc
            // 
            this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc.Location = new System.Drawing.Point(0, 0);
            this.sc.Name = "sc";
            // 
            // sc.Panel1
            // 
            this.sc.Panel1.Controls.Add(this.radioButton5);
            this.sc.Panel1.Controls.Add(this.radioButton4);
            this.sc.Panel1.Controls.Add(this.radioButton3);
            this.sc.Panel1.Controls.Add(this.radioButton2);
            this.sc.Panel1.Controls.Add(this.optObjectTypeButton);
            // 
            // sc.Panel2
            // 
            this.sc.Panel2.Controls.Add(this.designer);
            this.sc.Size = new System.Drawing.Size(592, 296);
            this.sc.SplitterDistance = 197;
            this.sc.TabIndex = 0;
            // 
            // radioButton5
            // 
            this.radioButton5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton5.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton5.Location = new System.Drawing.Point(12, 132);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(172, 24);
            this.radioButton5.TabIndex = 0;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "&Label";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.optObjectType_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton4.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton4.Location = new System.Drawing.Point(12, 102);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(172, 24);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "&TextBox";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.optObjectType_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton3.Location = new System.Drawing.Point(12, 72);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(172, 24);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "&Radio";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.optObjectType_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton2.Location = new System.Drawing.Point(12, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(172, 24);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "&Checkbox";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.optObjectType_CheckedChanged);
            // 
            // optObjectTypeButton
            // 
            this.optObjectTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optObjectTypeButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.optObjectTypeButton.Location = new System.Drawing.Point(12, 12);
            this.optObjectTypeButton.Name = "optObjectTypeButton";
            this.optObjectTypeButton.Size = new System.Drawing.Size(172, 24);
            this.optObjectTypeButton.TabIndex = 0;
            this.optObjectTypeButton.TabStop = true;
            this.optObjectTypeButton.Text = "&Button";
            this.optObjectTypeButton.UseVisualStyleBackColor = true;
            this.optObjectTypeButton.CheckedChanged += new System.EventHandler(this.optObjectType_CheckedChanged);
            // 
            // designer
            // 
            this.designer.BackColor = System.Drawing.SystemColors.Window;
            this.designer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.designer.DefaultObjectClass = null;
            this.designer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designer.EnableCreation = false;
            this.designer.Location = new System.Drawing.Point(0, 0);
            this.designer.Name = "designer";
            this.designer.Size = new System.Drawing.Size(391, 296);
            this.designer.TabIndex = 0;
            this.designer.DesignerObjectMouseDoubleClick += new AwesomeControls.Designer.DesignerObjectMouseEventHandler(this.designer_DesignerObjectMouseDoubleClick);
            // 
            // DesignerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 296);
            this.Controls.Add(this.sc);
            this.Name = "DesignerTest";
            this.Text = "DesignerTest";
            this.sc.Panel1.ResumeLayout(false);
            this.sc.Panel2.ResumeLayout(false);
            this.sc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sc;
        private Designer.DesignerControl designer;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton optObjectTypeButton;
    }
}