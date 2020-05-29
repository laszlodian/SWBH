using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SWB_OptionPackageInstaller
{
    public partial class MainForm : Form
    {
        #region Properties
        private string pathOfSWB;

        public string PathOfSWB
        {
            get { return pathOfSWB; }
            set { pathOfSWB = value; }
        }

        private string pathOfOptionPackages;

        public string PathOfOptionPackages
        {
            get { return pathOfOptionPackages; }
            set { pathOfOptionPackages = value; }
        }

        public DataGridView DataGridViewOfArtifacts { get { return dgv; } set { dgv = value; } }

        #endregion


        #region Variables
        public Thread unzipSWBThread;
        public DirectoryInfo tempDir;
        public static MainForm Instance = null;
        public DataGridView dgv = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private bool dgvBuilded;
        private Thread installThread;
        #endregion

        public MainForm()
        {
            Instance = this;
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
          
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.LightCyan;
          

            tbPathOfOptionpackages.Text = @"C:\_SWB\optionpackages";
            tbPathOfSWB.Text = @"C:\_SWB\SWB";

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0}       {1}", this.Text, SwVersion.Instance.ActualSWVersion);

        }

        public void ConfigureDataGridView(string packagesList)
        {
            string clearPackageName = string.Empty;
            int i = 0;
            foreach (string pkg in packagesList.Split(new string[] { "-repository" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(pkg.Trim()))
                {
                    continue;
                }
                clearPackageName = pkg.Substring(pkg.LastIndexOf("\\")+1);
                clearPackageName = clearPackageName.Remove(clearPackageName.IndexOf('!'));
                bindingSource1.Add(new PackageGridModel(clearPackageName, CommandControler.Instance.versions[i], true));
                i++;
            }
            
            dgv.Font = new Font(dgv.Font, FontStyle.Regular);
            tabControl1.TabPages["tbPageOptionPackages"].Layout += MainForm_Layout;
        }
        public DataGridViewComboBoxColumn CreateComboBoxWithEnums()
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = true;
            combo.DataPropertyName = "Title";
            combo.Name = "Title";
            return combo;
        }
        private void MainForm_Layout(object sender, LayoutEventArgs e)
        {

            if (!dgvBuilded)
            {
                EnumsAndComboBox_Load_For_All(sender, e);
                dgvBuilded = true;
            }

        }

        public void EnumsAndComboBox_Load_For_All(object sender, EventArgs e)
        {
            // Initialize the DataGridView.
            dgv.AutoGenerateColumns = false;
            dgv.AutoSize = true;
            dgv.DataSource = bindingSource1;

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Optionpackage Neve";
            dgv.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Version";
            column.Name = "Verzió";
            dgv.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Install";
            column.Width = 140;
            dgv.Columns.Add(column);
            dgv.AutoResizeColumnHeadersHeight();
            // Initialize the form.
            this.tabControl1.TabPages["tbPageOptionPackages"].Controls.Add(dgv);
            dgv.AutoSize = true;
            dgv.Text = "Founded option packages - Choose which of them to install to SunriseWorkBench";
        }

        private void tbPathOfSWB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/');
                if (!CommandControler.Instance.CheckPath(PathOfSWB))
                    return;

                btStart_Click(null, EventArgs.Empty);
            }

        }

        private void tbPathOfOptionpackages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/');
                if (!CommandControler.Instance.CheckPath(pathOfOptionPackages))
                    return;




                btStart_Click(null, EventArgs.Empty);
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            pathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/');
            CommandControler.Instance.OptionPackageList = CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);

            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ConfigureDataGridView(CommandControler.Instance.OptionPackageList);
            if (!CommandControler.Instance.CheckPath(tbPathOfOptionpackages.Text.Trim()) || !CommandControler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {
                PathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/'); ;
                PathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/'); ;
            }


            CommandControler.Instance.InstallOptionPackages();

        }

        private void installOptionPackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/');
            CommandControler.Instance.OptionPackageList = CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);

            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ConfigureDataGridView(CommandControler.Instance.OptionPackageList);
            if (!CommandControler.Instance.CheckPath(tbPathOfOptionpackages.Text.Trim()) || !CommandControler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {
                PathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/'); ;
                PathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/'); ;
            }

            unzipSWBThread = new Thread(new ThreadStart(CommandControler.Instance.UnzipSunriseWorkbench));
            unzipSWBThread.Start();
            while (unzipSWBThread.IsAlive)
            {
                MessageBox.Show("Please wait SWB is extracting...");
            }
            installThread = new Thread(new ThreadStart(CommandControler.Instance.InstallOptionPackages));
            installThread.Start();
            while (unzipSWBThread.IsAlive)
            {
                MessageBox.Show("Please wait packages being installed...");
            }

            MessageBox.Show("Installation Completed!");
        }

        private void showConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandControler.Instance.StartCmd();
        }

        private void sWBExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            CommandControler.Instance.UnzipSunriseWorkbench(CommandControler.Instance.SWBZipFilePath,PathOfSWB);
        }
    }
}
