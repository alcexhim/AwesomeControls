using System;
using System.Drawing;
using System.Windows.Forms;

using AwesomeControls.Ribbon;

namespace AwesomeControls.TestProject
{
	public class RibbonFormTest : RibbonForm
	{
        private RichTextBox richTextBox1;
    
		public RibbonFormTest ()
		{
			RibbonTab rtHome = new RibbonTab ();
			rtHome.Name = "rtHome";
			rtHome.Text = "Home";

            RibbonControlGroup rgNew = new RibbonControlGroup();
            rgNew.Name = "rgNew";
            rgNew.Text = "New";
            rgNew.ActionButtonVisible = true;

            RibbonControlButton rcNewFile = new RibbonControlButton();
            rcNewFile.Name = "rcNewFile";
            rcNewFile.Text = "File";
            rcNewFile.DisplayStyle = RibbonControlDisplayStyle.ImageAboveText;
            rcNewFile.Enabled = true;
            rgNew.Controls.Add(rcNewFile);

            RibbonControlButton rcNewProject = new RibbonControlButton();
            rcNewProject.Name = "rcNewProject";
            rcNewProject.Text = "Project";
            rcNewProject.DisplayStyle = RibbonControlDisplayStyle.ImageAboveText;
            rcNewProject.Enabled = true;
            rgNew.Controls.Add(rcNewProject);

            rtHome.Groups.Add(rgNew);

            RibbonControlGroup rgClipboard = new RibbonControlGroup();
            rgClipboard.Name = "rgClipboard";
            rgClipboard.Text = "Clipboard";
			rgClipboard.ActionButtonVisible = true;

            RibbonControlSplitButton rcClipboardPaste = new RibbonControlSplitButton();
            rcClipboardPaste.Name = "rcClipboardPaste";
            rcClipboardPaste.DisplayStyle = RibbonControlDisplayStyle.ImageAboveText;
            rcClipboardPaste.Text = "Paste";
            rcClipboardPaste.ButtonEnabled = false;

            RibbonMenuItem mnuClipboardPastePaste = new RibbonMenuItem();
            mnuClipboardPastePaste.Name = "mnuClipboardPastePaste";
            mnuClipboardPastePaste.Text = "&Paste";
            rcClipboardPaste.MenuItems.Add(mnuClipboardPastePaste);

            RibbonMenuItem mnuClipboardPastePasteSpecial = new RibbonMenuItem();
            mnuClipboardPastePasteSpecial.Name = "mnuClipboardPastePasteSpecial";
            mnuClipboardPastePasteSpecial.Text = "Paste &special";
            rcClipboardPaste.MenuItems.Add(mnuClipboardPastePasteSpecial);

            rgClipboard.Controls.Add(rcClipboardPaste);

            RibbonControlButton rcClipboardCut = new RibbonControlButton();
            rcClipboardCut.Name = "rcClipboardCut";
            rcClipboardCut.DisplayStyle = RibbonControlDisplayStyle.ImageBesideText;
            rcClipboardCut.Text = "Cut";

            rgClipboard.Controls.Add(rcClipboardCut);

            rtHome.Groups.Add(rgClipboard);

            
            RibbonTab rtView = new RibbonTab();
            rtView.Name = "rtView";
            rtView.Text = "View";

            RibbonControlButton btn = new RibbonControlButton();
            btn.Name = "btn";
            btn.Text = "Test button";
            btn.DisplayStyle = RibbonControlDisplayStyle.ImageBesideText;
            base.QuickAccessToolbar.Controls.Add(btn);

            RibbonTab rtConvert = new RibbonTab();
            rtConvert.Name = "rtConvert";
            rtConvert.Text = "Convert";

            RibbonTab rtSynthesize = new RibbonTab();
            rtSynthesize.Name = "rtSynthesize";
            rtSynthesize.Text = "Synthesize";

            base.Ribbon.Tabs.Add(rtHome);
            base.Ribbon.Tabs.Add(rtView);
            base.Ribbon.Tabs.Add(rtConvert);
            base.Ribbon.Tabs.Add(rtSynthesize);

            InitializeComponent();
		}

        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(416, 259);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // c
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(416, 283);
            this.Controls.Add(this.richTextBox1);
            this.Name = "c";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.richTextBox1, 0);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
	}
}

