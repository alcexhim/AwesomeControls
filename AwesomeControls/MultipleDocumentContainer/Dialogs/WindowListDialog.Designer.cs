namespace AwesomeControls.MultipleDocumentContainer.Dialogs
{
    partial class WindowListDialog
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
            this.components = new System.ComponentModel.Container();
            this.lv = new AwesomeControls.ListView.ListViewControl();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdActivate = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCloseWindow = new System.Windows.Forms.Button();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv.FullRowSelect = true;
            this.lv.ShowGridLines = true;
            this.lv.HideSelection = false;
            this.lv.LargeImageList = this.imlIcons;
            this.lv.Location = new System.Drawing.Point(12, 12);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(345, 257);
            this.lv.SmallImageList = this.imlIcons;
            this.lv.TabIndex = 0;
            this.lv.Mode = ListView.ListViewMode.Tiles;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClose.Location = new System.Drawing.Point(362, 275);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(91, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdActivate
            // 
            this.cmdActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdActivate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdActivate.Location = new System.Drawing.Point(363, 12);
            this.cmdActivate.Name = "cmdActivate";
            this.cmdActivate.Size = new System.Drawing.Size(91, 23);
            this.cmdActivate.TabIndex = 1;
            this.cmdActivate.Text = "&Activate";
            this.cmdActivate.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdSave.Location = new System.Drawing.Point(363, 41);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(91, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            // 
            // cmdCloseWindow
            // 
            this.cmdCloseWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCloseWindow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCloseWindow.Location = new System.Drawing.Point(363, 70);
            this.cmdCloseWindow.Name = "cmdCloseWindow";
            this.cmdCloseWindow.Size = new System.Drawing.Size(91, 23);
            this.cmdCloseWindow.TabIndex = 3;
            this.cmdCloseWindow.Text = "Close &Window";
            this.cmdCloseWindow.UseVisualStyleBackColor = true;
            // 
            // imlIcons
            // 
            this.imlIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlIcons.ImageSize = new System.Drawing.Size(128, 128);
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // WindowListDialog
            // 
            this.AcceptButton = this.cmdActivate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(466, 310);
            this.Controls.Add(this.cmdCloseWindow);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdActivate);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lv);
            this.KeyPreview = true;
            this.Name = "WindowListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows";
            this.ResumeLayout(false);

        }

        #endregion

        private AwesomeControls.ListView.ListViewControl lv;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdActivate;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCloseWindow;
        private System.Windows.Forms.ImageList imlIcons;
    }
}