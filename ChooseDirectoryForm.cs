using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public partial class ChooseDirectoryForm : Form
    {
        public ChooseDirectoryForm()
        {
            InitializeComponent();
            cbAllBuildDirectories.Items.AddRange(CommandControler.Instance.allBuildDirectoryUnderMasterFolder.ToArray());
            Refresh();
        }

        private void btChooseDirectory_Click(object sender, EventArgs e)
        {
            Form1.Instance.TbServerPath.Text = cbAllBuildDirectories.Text;
            Form1.Instance.UpdateStatus(string.Format("Selected build: {0}", cbAllBuildDirectories.Text.Substring(cbAllBuildDirectories.Text.LastIndexOf("\\") + 1)));
            this.Close();
        }
    }
}