using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public partial class SWBMainForm : Form
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
        BackgroundWorker bg = new BackgroundWorker();
        public Thread unzipSWBThread;
        public DirectoryInfo tempDir;
        public static SWBMainForm Instance = null;
        public DataGridView dgv = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private bool dgvBuilded;
        private Thread installThread;
        #endregion

        public SWBMainForm()
        {
            Instance = this;
            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.LightCyan;


            tbPathOfPackages.Text = @"c:\_SWB\Test\Packages\";
            tbPathOfSWB.Text = @"c:\_SWB\Test\SWB\";

            this.Load += SWBMainForm_Load;

            InitBackGroundWorker();
        }

        private void InitBackGroundWorker()
        {
            bg.DoWork += Bg_DoWork;
            bg.ProgressChanged += Bg_ProgressChanged; 
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pf.Close();
            SWBMainForm.Instance.Show();

            Refresh();

            PerformLayout();
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
//            throw new NotImplementedException();
        }
        ProgressForm pf = null;
        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            SWBMainForm.Instance.Hide();
            pf=new ProgressForm();
            pf.Show();


        }

        private void SWBMainForm_Load(object sender, EventArgs e)
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
                clearPackageName = pkg.Substring(pkg.LastIndexOf("\\") + 1);
                clearPackageName = clearPackageName.Remove(clearPackageName.IndexOf('!'));
                bindingSource1.Add(new PackageGridModel(clearPackageName, CommandControler.Instance.versions[i], true));
                i++;
            }

            dgv.Font = new Font(dgv.Font, FontStyle.Regular);
           tabControlAllDAta.TabPages["tbPageOptionPackages"].Layout += SWBMainForm_Layout;
        }
        public DataGridViewComboBoxColumn CreateComboBoxWithEnums()
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = true;
            combo.DataPropertyName = "Title";
            combo.Name = "Title";
            return combo;
        }
        private void SWBMainForm_Layout(object sender, LayoutEventArgs e)
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
            this.tabControlAllDAta.TabPages["tbPageOptionPackages"].Controls.Add(dgv);
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
                pathOfOptionPackages = tbPathOfPackages.Text.Trim().Replace('\\', '/');
                if (!CommandControler.Instance.CheckPath(pathOfOptionPackages))
                    return;




                btStart_Click(null, EventArgs.Empty);
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {

            bg.RunWorkerAsync();

            ChangeInfoText("Start processing configured parameters...");

            if (!CommandControler.Instance.CheckPath(tbPathOfPackages.Text.Trim()) || !CommandControler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {

                PathOfOptionPackages = tbPathOfPackages.Text.Trim().Replace('\\', '/'); ;
                PathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/'); ;
            }

            CommandControler.Instance.OptionPackageList = CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);
            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ConfigureDataGridView(CommandControler.Instance.OptionPackageList);

         installThread=  new Thread(new ThreadStart(CommandControler.Instance.InstallOptionPackages));

        }

        private void ChangeInfoText(string infoText)
        {
            if (ProgressForm.Instance != null)
            {
                ProgressForm.Instance.NewInfo = infoText;
                ProgressForm.Instance.ChangeInfoText();
            }
        }

        private void installOptionPackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pathOfOptionPackages = tbPathOfPackages.Text.Trim().Replace('\\', '/');
            CommandControler.Instance.OptionPackageList = CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);

            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ConfigureDataGridView(CommandControler.Instance.OptionPackageList);
            if (!CommandControler.Instance.CheckPath(tbPathOfPackages.Text.Trim()) || !CommandControler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {
                PathOfOptionPackages = tbPathOfPackages.Text.Trim().Replace('\\', '/'); ;
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

            CommandControler.Instance.UnzipSunriseWorkbench();
        }

        private void btPackageFolderBrowse_Click(object sender, EventArgs e)
        {
            OpenFolderBrowser(tbPathOfPackages);
        }

        private void btSWBPathBrowse_Click(object sender, EventArgs e)
        {
            OpenFolderBrowser(tbPathOfSWB);
        }

        private void OpenFolderBrowser(TextBox textBox_in)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox_in.Text = fbd.SelectedPath;
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!CommandControler.Instance.CheckPath(tbPathOfPackages.Text.Trim()) || !CommandControler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {
                PathOfOptionPackages = tbPathOfPackages.Text.Trim().Replace('\\', '/'); ;
                PathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/'); ;
            }

            CommandControler.Instance.OptionPackageList = CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);
            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ConfigureDataGridView(CommandControler.Instance.OptionPackageList);

            CommandControler.Instance.InstallOptionPackages();
        }
    }
}

