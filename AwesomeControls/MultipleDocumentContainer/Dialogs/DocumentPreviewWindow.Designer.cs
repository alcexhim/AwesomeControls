namespace AwesomeControls.MultipleDocumentContainer.Dialogs
{
    partial class DocumentPreviewWindow
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
            this.pnlFileName = new System.Windows.Forms.Panel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFileName
            // 
            this.pnlFileName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlFileName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFileName.Location = new System.Drawing.Point(0, 136);
            this.pnlFileName.Name = "pnlFileName";
            this.pnlFileName.Size = new System.Drawing.Size(194, 58);
            this.pnlFileName.TabIndex = 0;
            this.pnlFileName.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // picPreview
            // 
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(194, 136);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            // 
            // DocumentPreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 194);
            this.ControlBox = false;
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.pnlFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentPreviewWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFileName;
        internal System.Windows.Forms.PictureBox picPreview;
    }
}