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
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            this.Load += OptionsForm_Load;
            this.FormClosed += OptionsForm_FormClosed;
        }

        private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.Instance.SetDefaultValues();
            Form1.Instance.BringToFront();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 3, Screen.PrimaryScreen.WorkingArea.Height / 3);

            tbDefaultPathForPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
            tbDefaultPathForSWB.Text = Properties.Settings.Default.DefaultSWBPath;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOptionsOK_Click(object sender, EventArgs e)
        {
            StoreThatThereSavedValues();

            Properties.Settings.Default.DefaultOptionPackagePath = tbDefaultPathForPackages.Text;
            Properties.Settings.Default.DefaultSWBPath = tbDefaultPathForSWB.Text;
            Properties.Settings.Default.InstallAllFoundPackages = cbInstallAllFoundOPs.Checked;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void StoreThatThereSavedValues()
        {
            Properties.Settings.Default.HasSavedValues = true;
            Properties.Settings.Default.Save();
        }
    }
}