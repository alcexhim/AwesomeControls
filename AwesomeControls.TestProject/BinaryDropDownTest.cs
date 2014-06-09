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
	public partial class BinaryDropDownTest : Form
	{
		public BinaryDropDownTest()
		{
			InitializeComponent();
			binaryTextBoxControl1.Value = new byte[]
			{
				0x6D, 0x4A, 0x24, 0x9C, 0x85, 0x29, 0xDE, 0x62, 0xC8, 0xE3, 0x89, 0x39, 0x31, 0xC9, 0xE0, 0xBC
			};
		}
	}
}
