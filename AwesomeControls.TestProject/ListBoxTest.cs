using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TestProject
{
	public partial class ListBoxTest : Form
	{
		public ListBoxTest()
		{
			InitializeComponent();

			listView1.MultiSelect = false;

			listView1.Columns.Add("File name", 280);
			listView1.Columns.Add("Type", 100);
			listView1.Columns.Add("Size", 60);

            {
                ListView.ListViewItem lvi = new ListView.ListViewItem();
                lvi.Text = "kagami";
                lvi.Details.Add("Folder");
                lvi.Details.Add("3 files, 1 folder");
                {
                    ListView.ListViewItem lvi1 = new ListView.ListViewItem();
                    lvi1.Text = "KAGAMATRIX";
                    lvi1.Details.Add("Folder");
                    lvi1.Details.Add("2 files");
                    for (int i = 0; i < 2; i++)
                    {
                        lvi1.Items.Add("KAGAMTRX." + i.ToString().PadLeft(3, '0'), "File", "0 KB");
                    }
                    lvi.Items.Add(lvi1);
                }
                lvi.Items.Add("system.txt", "Text document", "1.7 KB");
                lvi.Items.Add("mscore.dll", "Dynamic link library", "303 KB");
                lvi.Items.Add("kagm0011.dat", "Unknown", "705 MB");
                listView1.Items.Add(lvi);
            }
			{
				AwesomeControls.ListView.ListViewItem lvi = new ListView.ListViewItem();
				lvi.Text = "IO.SYS";
				lvi.Details.Add("System file");
				lvi.Details.Add("0 KB");
				listView1.Items.Add(lvi);
			}
			{
				AwesomeControls.ListView.ListViewItem lvi = new ListView.ListViewItem();
				lvi.Text = "MinGW.7z";
				lvi.Details.Add("7z archive");
				lvi.Details.Add("128 193 KB");
				listView1.Items.Add(lvi);
			}
			{
				AwesomeControls.ListView.ListViewItem lvi = new ListView.ListViewItem();
				lvi.Text = "MSDOS.SYS";
				lvi.Details.Add("System file");
				lvi.Details.Add("0 KB");
				listView1.Items.Add(lvi);
			}
			{
				AwesomeControls.ListView.ListViewItem lvi = new ListView.ListViewItem();
				lvi.Text = "MyVSSettings.vssettings";
				lvi.Details.Add("Visual Studio Settings File");
				lvi.Details.Add("209 KB");
				listView1.Items.Add(lvi);
			}
			{
				AwesomeControls.ListView.ListViewItem lvi = new ListView.ListViewItem();
				lvi.Text = "NTDETECT.COM";
				lvi.Details.Add("MS-DOS Application");
				lvi.Details.Add("47 KB");
				listView1.Items.Add(lvi);
			}
		}
	}
}
