using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using CheckBox = System.Windows.Forms.CheckBox;
using Control = System.Windows.Forms.Control;
using TabControl = System.Windows.Forms.TabControl;
using TextBox = System.Windows.Forms.TextBox;

namespace SWB_OptionPackageInstaller
{
    public partial class Form1 : Form
    {
        #region Properties

        private string localPath;

        public string LocalPath
        {
            get { return localPath; }
            set { localPath = value; }
        }

        //private bool isLocalPathSet;

        //public bool IsLocalPathSet
        //{
        //    get { return isLocalPathSet; }
        //    set { isLocalPathSet = value; }
        //}

        //private Control control;

        //public Control Control
        //{
        //    get { return control; }
        //    set { control = value; }
        //}

        internal void SetDefaultValues()
        {
            //tbPathOfPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
            //tbPathOfSWB.Text = Properties.Settings.Default.DefaultSWBPath;
        }

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

        //private bool[] pathsAreValid = new bool[] { false, false };

        //public bool[] PathsAreValid
        //{
        //    get { return pathsAreValid; }
        //    set { pathsAreValid = value; }
        //}

        //public void InsertSavedValues()
        //{
        //    //tbPathOfSWB.Text = Properties.Settings.Default.DefaultSWBPath;
        //    //tbPathOfPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
        //}

        //private DirectoryInfo lastBuildPath;

        //public DirectoryInfo LastBuildPath
        //{
        //    get { return lastBuildPath; }
        //    set { lastBuildPath = value; }
        //}

        //public DataGridView DataGridViewOfArtifacts { get { return dgvInstalledOPs; } set { dgvInstalledOPs = value; } }

        private TextBox tbServerPath;

        public TextBox TbServerPath
        {
            get { return tbServerPath; }
            set { tbServerPath = value; }
        }

        //private bool collectionFinished = false;

        //public bool CollectionFinished
        //{
        //    get
        //    {
        //        return collectionFinished;
        //    }
        //    set
        //    {
        //        collectionFinished = value;
        //    }
        //}

        //public TextBox RemoteDropDown
        //{
        //    get { return tbOptionPackagesServer; }
        //    set { tbOptionPackagesServer = value; }
        //}

        private BindingSource bindingSourceForFoundPackages = new BindingSource();
        public BindingSource BindingSourceForFoundPackages { get { return bindingSourceForFoundPackages; } set { bindingSourceForFoundPackages = value; } }

        #endregion Properties

        #region Variables

        public bool[] threadsAlive = new bool[3] { false, false, false };

        public BackgroundWorker bgWorker = new BackgroundWorker();
        public Thread unzipSWBThread;
        public Thread progressThread;
        public DirectoryInfo tempDir;
        public static Form1 Instance = null;
        public DataGridView dgvInstalledOPs = new DataGridView();
        public DataGridView dgv_collectedOPs = new DataGridView();
        public BindingSource bindingSourceForIstalledPackages = new BindingSource();
        public BindingSource bindingSourceForCollectedPackages = new BindingSource();

        public bool swbCopied = false;
        private Thread installThread;
        public ProgressForm pf = null;
        public DirectoryInfo destinationDir;

        public List<string> collectedOPs = new List<string>();
        private string lastBuildNumber = string.Empty;
        public TabControl theTabControl;
        private bool[] givenPaths = new bool[2] { false, false };

        #endregion Variables

        #region Delegates

        private delegate void SetBackColorOfPathTextBoxDelegate(TextBox textBox, Color color);

        public void SetBackColorOfPathTextBox(TextBox textBox, Color color)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new SetBackColorOfPathTextBoxDelegate(SetBackColorOfPathTextBox), textBox, color);
            }
            else
                textBox.BackColor = color;
        }

        public delegate void SetLocalPathTextBoxDelegate(bool active);

        public void SetLocalPathTextBox(bool active)
        {
            if (active)
            {
                ArtifactHandler.Instance.LocalPath = new DirectoryInfo(tbPathOfLocalFolder.Text);
                if (tbPathOfLocalFolder.InvokeRequired)
                {
                    tbPathOfLocalFolder.Invoke(new SetLocalPathTextBoxDelegate(SetLocalPathTextBox), active);
                }
                else
                {
                    tbPathOfLocalFolder.Enabled = true;
                    tbPathOfLocalFolder.BackColor = Color.White;
                    tbPathOfLocalFolder.ForeColor = Color.Black;
                }
            }
            else
            {
                ArtifactHandler.Instance.LocalPath = new DirectoryInfo(tbPathOfSWB.Text);

                if (tbPathOfLocalFolder.InvokeRequired)
                {
                    tbPathOfLocalFolder.Invoke(new SetLocalPathTextBoxDelegate(SetLocalPathTextBox), active);
                }
                else
                {
                    tbPathOfLocalFolder.Enabled = true;
                    tbPathOfLocalFolder.BackColor = Color.Gainsboro;
                    tbPathOfLocalFolder.ForeColor = Color.Gray;
                }
            }
        }

        private delegate void SetStartSWBButtonVisibleDelegate();

        public void SetButtonsVisible()
        {
            if (btCleanUp.InvokeRequired)
            {
                btCleanUp.Invoke(new SetStartSWBButtonVisibleDelegate(SetButtonsVisible));
            }
            else
                btCleanUp.Visible = true;

            if (btStartSWB.InvokeRequired)
            {
                btStartSWB.Invoke(new SetStartSWBButtonVisibleDelegate(SetButtonsVisible));
            }
            else
            {
                btStartSWB.Visible = true;
                btStartSWB.Focus();
            }
        }

        public delegate void SetInstallationPageValuesDelegate();

        public void SetInstallationPageValues()
        {
            if (theTabControl.InvokeRequired)
            {
                theTabControl.Invoke(new SetInstallationPageValuesDelegate(SetInstallationPageValues));
            }
            else
            {
                theTabControl.SelectedIndex = 0;
                theTabControl.TabPages[0].Select();
            }

            SetTbText(tbPathOfSWB, String.Format("{0}{1}", destinationDir.FullName, "SWB"));
            SetTbText(tbPathOfPackages, destinationDir.FullName);
        }

        private delegate void SetTbTextDelegate(TextBox textBox, string text);

        public void SetTbText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new SetTbTextDelegate(SetTbText));
            }
            else
                textBox.Text = text;
        }

        private delegate void UpdateStatusDelegate(string status);

        public void UpdateStatus(string status)
        {
            if (this.lbInfoText.InvokeRequired)
            {
                this.Invoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { status });
                return;
            }

            this.lbInfoText.Text = status;
            this.lbInfoText.Refresh();

            if (this.lbInfoText2.InvokeRequired)
            {
                this.Invoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { status });
                return;
            }

            this.lbInfoText2.Text = status;
            this.lbInfoText2.Refresh();
        }

        //public void AppendUpdateStatus(string status)
        //{
        //    if (this.lbInfoText.InvokeRequired)
        //    {
        //        this.Invoke(new AppendUpdateStatusDelegate(this.UpdateStatus), new object[] { status });
        //        return;
        //    }

        //    this.lbInfoText.Text += status;
        //    this.lbInfoText.Refresh();

        //    if (this.lbInfoText2.InvokeRequired)
        //    {
        //        this.Invoke(new AppendUpdateStatusDelegate(this.UpdateStatus), new object[] { status });
        //        return;
        //    }

        //    this.lbInfoText2.Text += status;
        //    this.lbInfoText2.Refresh();
        //}

        #endregion Delegates

        /// <summary>
        /// Default constructor
        /// </summary>
        public Form1()
        {
            Instance = this;

            if (Properties.Settings.Default.IsAppInQuickMode)
            {
            }
            InitializeComponent();

            if (cbPathOfLocalFolder.Checked)
            {
                pathOfSWB = tbPathOfLocalFolder.Text;
            }
            else
                pathOfSWB = Properties.Settings.Default.LastUsedSWBPath;

            cbPathOfLocalFolder_CheckedChanged(null, EventArgs.Empty);

            theTabControl = mainTabControl;
            tbServerPath = theTabControl.TabPages[2].Controls["tableLayoutPanel2"].Controls["tbOptionPackagesServer"] as TextBox;

            if (Properties.Settings.Default.HasSavedValues)
            {
                tbPathOfSWB.Text = Properties.Settings.Default.LastUsedSWBPath;
                tbPathOfPackages.Text = Properties.Settings.Default.LastUsedOptionPackagePath;
            }

            //    TraceHelper.SetupListener();
            UpdateStatus("Loading Form1....");

            dgvInstalledOPs.Dock = DockStyle.Fill;
            dgvInstalledOPs.BackgroundColor = Color.LightCyan;

            ArtifactHandler.Instance.Validate(tbPathOfSWB);
            ArtifactHandler.Instance.Validate(tbPathOfPackages);

            UpdateStatus("Browse for a path or simlpy type it in to the textbox");

            this.Shown += Form1_Shown;
            this.Load += SWBForm1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        internal void SetUIStateToWaitingCommands()
        {
            // this.Activate();
            UpdateStatus("Every given command has been finished successfully.");

            // this.Refresh();
            // this.Focus();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            UpdateStatus("Initializing functions, and features...");
            Form1.Instance.SetLocalPathTextBox(((Form1.Instance.theTabControl.TabPages[2].Controls["tableLayoutPanel2"] as TableLayoutPanel).Controls["cbPathOfLocalFolder"] as CheckBox).Checked);
            tbSWBText = tbPathOfSWB.Text;
        }

        private string tbSWBText;

        public string TbSWBText
        {
            get { return tbSWBText; }
            set { tbSWBText = value; }
        }

        public bool IsLocalPathConfigured()
        {
            bool state = false;

            if (cbPathOfLocalFolder.Checked)
            {
                state = true;
                localPath = tbPathOfLocalFolder.Text;
            }
            else
            {
                localPath = tbPathOfSWB.Text;
                state = false;
            }
            return state;
        }

        public void ConfigureDataGridView(string packagesList)
        {
            bindingSourceForIstalledPackages = new BindingSource();
            UpdateStatus("Configuring DataGrid");
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
                UpdateStatus(string.Format("Package currently in progress: {0}", clearPackageName));

                bindingSourceForIstalledPackages.Add(new PackageGridModel(clearPackageName, CommandControler.Instance.versions[i], true));
                i++;
            }

            mainTabControl.TabPages["tbPageOptionPackages"].Layout += SWBForm1_Layout;
        }

        #region EventHandlers

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (SettingsProperty item in Properties.Settings.Default.Properties)
            {
                if (item.Name == "LastUsedOptionPackagePath")
                {
                    item.DefaultValue = tbPathOfPackages.Text;
                }
                else if (item.Name == "LastUsedSWBPath")
                {
                    item.DefaultValue = tbPathOfSWB.Text;
                }
            }
            Properties.Settings.Default.LastUsedOptionPackagePath = tbPathOfPackages.Text;
            Properties.Settings.Default.LastUsedSWBPath = tbPathOfSWB.Text;

            Properties.Settings.Default.Save();
        }

        private void SWBForm1_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0}       {1}", "SunriseWorkbench option package manager", SwVersion.Instance.ActualSWVersion);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
            //    tbPathOfPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
            //    tbPathOfSWB.Text = Properties.Settings.Default.DefaultSWBPath;
        }

        private void SWBForm1_Layout(object sender, LayoutEventArgs e)
        {
            UpdateStatus("Building DataGrid for reprezenting features...");
            EnumsAndComboBox_Load_For_All(sender, e);
        }

        public void ConfigureDgvForPackagesInFolder(DataGridView dgv, BindingSource bindingSrc)
        {
            UpdateStatus("DataGrid has been created, filling it in with corresponding values...");
            // Initialize the DataGridView.
            dgv.AutoGenerateColumns = false;
            dgv.AutoSize = false;
            dgv.Dock = DockStyle.Fill;
            dgv.DataSource = bindingSrc;

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "No";
            column.Width = 50;
            column.Name = "No.";
            dgv.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Option Package Name";
            column.Width = 500;
            dgv.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Collect this package";
            column.Width = 150;
            dgv.Columns.Add(column);

            dgv.AutoResizeColumnHeadersHeight();
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
            // Initialize the form.
            dgv.Text = "Founded option packages in folder - Choose which of them to install to SunriseWorkBench";
        }

        public void EnumsAndComboBox_Load_For_All(object sender, EventArgs e)
        {
            UpdateStatus("DataGrid has been created, filling it in with corresponding values...");
            // Initialize the DataGridView.
            dgvInstalledOPs.AutoGenerateColumns = false;
            dgvInstalledOPs.AutoSize = true;
            dgvInstalledOPs.AutoResizeColumnHeadersHeight();
            dgvInstalledOPs.DataSource = bindingSourceForIstalledPackages;

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Option Package Name";
            column.Width = 350;
            dgvInstalledOPs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Version";
            column.Width = 300;
            column.Name = "Version";
            dgvInstalledOPs.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Is Installed";
            column.Width = 140;
            dgvInstalledOPs.Columns.Add(column);

            dgvInstalledOPs.AutoResizeColumnHeadersHeight();
            dgvInstalledOPs.AutoResizeColumns();
            dgvInstalledOPs.AutoResizeRows();
            // Initialize the form.
            this.mainTabControl.TabPages["tbPageOptionPackages"].Controls.Add(dgvInstalledOPs);
            dgvInstalledOPs.Dock = DockStyle.Fill;
            dgvInstalledOPs.Text = "Founded option packages - Choose which of them to install to SunriseWorkBench";
        }

        public void EnumsAndComboBox_Load_For_CollectedOPs()
        {
            UpdateStatus("DataGrid has been created, filling it in with corresponding values...");
            // Initialize the DataGridView.
            dgv_collectedOPs.AutoGenerateColumns = false;
            dgv_collectedOPs.AutoSize = false;
            dgv_collectedOPs.DataSource = bindingSourceForCollectedPackages;

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Option Package Neve";
            column.Width = 300;
            dgv_collectedOPs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Version";
            column.Width = 200;
            column.Name = "Verzió";
            dgv_collectedOPs.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Installed";
            column.Width = 140;
            dgv_collectedOPs.Columns.Add(column);
            dgv_collectedOPs.AutoResizeColumnHeadersHeight();
            dgv_collectedOPs.AutoResizeColumns();
            dgv_collectedOPs.AutoResizeRows();
            // Initialize the form.
            this.mainTabControl.TabPages[3].Controls.Add(dgv_collectedOPs);
            dgv_collectedOPs.AutoSize = true;
            dgv_collectedOPs.Text = "Founded option packages - Choose which of them to install to SunriseWorkBench";
        }

        public EventWaitHandle handle;
        private BindingSource bindingSrcForRemotePackages = new BindingSource();
        public List<string> installationForPackages = new List<string>();

        private void btOK_Click(object sender, EventArgs e)
        {
            GetCheckedOptionPackagesFirstTab();
            UpdateStatus("Process started...");
            CommandControler.Instance.CorrectTextBoxesPath();

            UpdateStatus("Preparing to getting option packages...");
            CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);

            UpdateStatus("Get versions for SWB and all of the available option packages...");
            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => ConfigureDataGridView(CommandControler.Instance.OptionPackageList))));

            installThread = new Thread(new ThreadStart(CommandControler.Instance.InstallOptionPackages));
            installThread.SetApartmentState(ApartmentState.MTA);
            installThread.Name = "InstallOptionPackages";
            ThreadManager.Instance.StartAndWaitOneThread(installThread);

            UpdateStatus("Progress is done.");
        }

        private void btBrowseSWBPath_Click(object sender, EventArgs e)
        {
            ArtifactHandler.Instance.OpenFolderBrowser(tbPathOfSWB);
        }

        private void btBrowseForPackages_Click(object sender, EventArgs e)
        {
            if ((sender as System.Windows.Forms.Button).Name == "btBrowseSWBPath")
            {
                ArtifactHandler.Instance.OpenFolderBrowser(tbPathOfSWB);
            }
            else
                ArtifactHandler.Instance.OpenFolderBrowser(tbPathOfPackages);
        }

        private void tbPathOfSWB_TextChanged(object sender, EventArgs e)
        {
            pathOfSWB = Path.GetFullPath(tbPathOfSWB.Text);
        }

        private void tbPathOfPackages_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.GetFullPath(tbPathOfPackages.Text)))
            {
                pathOfOptionPackages = Path.GetFullPath(tbPathOfPackages.Text);
            }
            else
                SetBackColorOfPathTextBox(tbPathOfPackages, Color.Red);
        }

        private void btCollect_Click(object sender, EventArgs e)
        {
            DisableAllControllOnTabThree();
            LocalPath = cbPathOfLocalFolder.Checked ? tbPathOfLocalFolder.Text : TbSWBText;
            GetCheckedOptionPackagesThirdTab();
            ArtifactHandler.Instance.PrepareAndFinalizeRemoteDropDownCopyingOptionPackages();
        }

        private void GetCheckedOptionPackagesThirdTab()
        {
            installationForPackages = new List<string>();
            if (dgvOptionPackagesInFolder.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvOptionPackagesInFolder.Rows)
                {
                    if ((Convert.ToBoolean((row.Cells[2] as DataGridViewCheckBoxCell).Value)))
                    {
                        installationForPackages.Add(Convert.ToString((row.Cells[1] as DataGridViewTextBoxCell).Value));
                    }
                    else
                        continue;
                }
            }
        }

        private void GetCheckedOptionPackagesFirstTab()
        {
            installationForPackages = new List<string>();
            if (dgvForPackagesInFolder.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvForPackagesInFolder.Rows)
                {
                    if ((Convert.ToBoolean((row.Cells[2] as DataGridViewCheckBoxCell).Value)))
                    {
                        installationForPackages.Add(Convert.ToString((row.Cells[1] as DataGridViewTextBoxCell).Value));
                    }
                    else
                        continue;
                }
            }
        }

        private delegate void DisableAllControllOnTabThreeDelegate();

        private void DisableAllControllOnTabThree()
        {
            if (theTabControl.TabPages[2].InvokeRequired)
            {
                theTabControl.TabPages[2].Invoke(new DisableAllControllOnTabThreeDelegate(DisableAllControllOnTabThree));
            }
            else
                foreach (Control item in theTabControl.TabPages[2].Controls)
                {
                    if (item.Name.EndsWith("Install") || item.GetType() == typeof(DataGridView))
                    {
                        continue;
                    }
                    else
                        item.Enabled = false;
                }
        }

        private void btStartSWB_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(PathOfSWB, "SunriseWorkbench.exe"), string.Format("-data \"{0}\"", Path.Combine(PathOfSWB, "_WS")));
        }

        private void CleanUpEverything_Click(object sender, EventArgs e)
        {
            EnableAllControllOnTabThree();
            UpdateStatus("Delete all generated,copied resources..");
            Directory.Delete(destinationDir.FullName, true);
        }

        private delegate void EnableAllControllOnTabThreeDelegate();

        private void EnableAllControllOnTabThree()
        {
            if (theTabControl.TabPages[2].InvokeRequired)
            {
                theTabControl.TabPages[2].Invoke(new EnableAllControllOnTabThreeDelegate(EnableAllControllOnTabThree));
            }
            else
                foreach (Control item in theTabControl.TabPages[2].Controls)
                {
                    item.Enabled = true;
                }
        }

        public System.Windows.Forms.Button GetButtonStartWithInstall()
        {
            return btContinueWithInstall;
        }

        private void btGenPath_Click(object sender, EventArgs e)
        {
            DirectoryInfo genPath = new DirectoryInfo(string.Format("{0}_{1}_{2}_{3}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour));
            if (tbPathOfSWB.Text.EndsWith("\\") || tbPathOfSWB.Text.EndsWith("/"))
            {
                tbPathOfSWB.AppendText(String.Format("{0}/", genPath.Name));
            }
            else
                tbPathOfSWB.AppendText(String.Format("/{0}/", genPath.Name));
        }

        #endregion EventHandlers

        private void btCheckOPs_Click(object sender, EventArgs e)
        {
            CommandControler.Instance.CheckPackagesInFolder(PathOfOptionPackages);

            PrepareDataGridView(dgvForPackagesInFolder);
            ConfigureDgvForPackagesInFolder(dgvForPackagesInFolder, bindingSrcForRemotePackages);
        }

        public DataGridView GetDGVForCollectedOPs()
        {
            return dgv_collectedOPs;
        }

        public DataGridView GetDGVForInstalledOPs()
        {
            return dgvInstalledOPs;
        }

        public void PrepareDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.Name == "dgv_collectedOPs" || dataGridView.Name == "dgvInstalledOPs")
            {
                Form1.Instance.theTabControl.TabPages[1].Controls.Add(dgvInstalledOPs);
                Form1.Instance.theTabControl.TabPages[3].Controls.Add(dgv_collectedOPs);
            }
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSize = true;
            dataGridView.Visible = true;
        }

        private void btOptions_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
        }

        private void btCleanUp_Click(object sender, EventArgs e)
        {
            UpdateStatus(string.Format("Deleting directory: {0}", PathOfSWB));
            Directory.Delete(Path.GetDirectoryName(PathOfSWB), true);
            UpdateStatus("Cleaning up finished");
        }

        private void cbPathOfLocalFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPathOfLocalFolder.Checked)
            {
                SetLocalPathTextBox(true);
            }
            else
            {
                tbPathOfLocalFolder.Enabled = true;
                tbPathOfLocalFolder.BackColor = Color.Gainsboro;
                tbPathOfLocalFolder.ForeColor = Color.Gray;
            }
        }

        private void cbSpecifiedBuild_CheckedChanged(object sender, EventArgs e)
        {
            List<string> availableBuilds = new List<string>();

            ArtifactHandler.Instance.ReadOutLastBuildNumber(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));

            availableBuilds = CommandControler.Instance.CollectAndShowAvailableBuildDirectories(new DirectoryInfo(ArtifactHandler.Instance.RemoteDropDownRootPath));

            cbAllBuildsOnServer.Items.AddRange(availableBuilds.ToArray<object>());
            cbAllBuildsOnServer.Enabled = true;
            cbAllBuildsOnServer.Text = Convert.ToString(cbAllBuildsOnServer.Items[0]);

            cbAllBuildsOnServer.Refresh();

            tbOptionPackagesServer.Enabled = false;
        }

        private void cbAllBuildsOnServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbInvoker();
        }

        private delegate void CbInvokerDelegate();

        public void CbInvoker()
        {
            if (cbAllBuildsOnServer.InvokeRequired)
            {
                cbAllBuildsOnServer.Invoke(new CbInvokerDelegate(CbInvoker));
            }
            else
                SetLastBuildPath();
        }

        public void SetLastBuildPath()
        {
            if (!cbSpecifiedBuild.Checked && string.IsNullOrEmpty(ArtifactHandler.Instance.RemoteDropDownRootPath))
            {
                ArtifactHandler.Instance.LastBuildPath = ArtifactHandler.Instance.ReadOutLastBuildPath(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            }
            else
                GetLastBuildPath();
        }

        private delegate void GetLastBuildPathDelegate();

        private void GetLastBuildPath()
        {
            if (cbAllBuildsOnServer.InvokeRequired)
            {
                cbAllBuildsOnServer.Invoke(new GetLastBuildPathDelegate(GetLastBuildPath));
            }
            else
                ArtifactHandler.Instance.LastBuildPath = new DirectoryInfo(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, cbAllBuildsOnServer.Text));
        }

        private void btShowOptionPackagesInFolder_Click(object sender, EventArgs e)
        {
            ArtifactHandler.Instance.LastBuildPath = ArtifactHandler.Instance.ReadOutLastBuildPath(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            CommandControler.Instance.CheckPackagesInFolder(ArtifactHandler.Instance.LastBuildPath.FullName, out bindingSrcForRemotePackages);
            dgvOptionPackagesInFolder.Visible = true;
            //  PrepareDataGridView(dgvOptionPackagesInFolder);
            ConfigureDgvForPackagesInFolder(dgvOptionPackagesInFolder, bindingSrcForRemotePackages);
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArtifactHandler.Instance.LastBuildPath = ArtifactHandler.Instance.ReadOutLastBuildPath(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            SetLastBuildPath();
            if (theTabControl.SelectedIndex == 2)
            {
                UpdateStatus(string.Format("Last Build Number: {0}", ArtifactHandler.Instance.LastBuildNumber));
                UpdateImportantStatus(string.Format("Last Build Path: {0}", ArtifactHandler.Instance.LastBuildPath));
            }
        }

        private delegate void UpdateImportantStatusDelegate(string text);

        public void UpdateImportantStatus(string text)
        {
            if (lbInfo2.InvokeRequired)
            {
                lbInfo2.Invoke(new UpdateImportantStatusDelegate(UpdateImportantStatus), text);
            }
            else
                lbInfo2.Text = string.Format("Important:{0}{1}", Environment.NewLine, text);
        }

        private void btContinueWithInstall_Click(object sender, EventArgs e)
        {
            ContinueWithInstall(LocalPath);
        }

        private delegate void ChangeToInstallTabDelegate(string path);

        private void ContinueWithInstall(string localPath)
        {
            ChangeToInstallTab(localPath);
        }

        public void ChangeToInstallTab(string path)
        {
            if (this.theTabControl.InvokeRequired)
            {
                theTabControl.Invoke(new ChangeToInstallTabDelegate(ChangeToInstallTab), path);
            }
            else
            {
                theTabControl.SelectedTab = theTabControl.TabPages[0];
                SetOPFolderText(path);
            }
        }

        private delegate void SetOPFolderTextDelegate(string path);

        private void SetOPFolderText(string path)
        {
            if (tbPathOfPackages.InvokeRequired)
            {
                tbPathOfPackages.Invoke(new SetOPFolderTextDelegate(SetOPFolderText), path);
            }
            else
            {
                tbPathOfPackages.Text = path;
                DialogResult res = CommandControler.Instance.ShowQuestionDialog("Do you want to continue with insallation?");
                if (res == DialogResult.No)
                {
                    SetSWBPathTextBoxFocused();
                    return;
                }
                else
                    btOK_Click(btOK, EventArgs.Empty);
            }
        }

        private delegate void SetSWBPathTextBoxFocusedDelegate();

        private void SetSWBPathTextBoxFocused()
        {
            if (tbPathOfSWB.InvokeRequired)
            {
                tbPathOfSWB.Invoke(new SetSWBPathTextBoxFocusedDelegate(SetSWBPathTextBoxFocused));
            }
            else
                tbPathOfSWB.Focus();
        }

        public Form popOutForm = new Form();
        public DataGridView dgvPopedOut = new DataGridView();

        private void btPopOut_Click(object sender, EventArgs e)
        {
            dgvPopedOut = dgvOptionPackagesInFolder;

            popOutForm.Controls.Add(dgvPopedOut);
            dgvPopedOut.Dock = DockStyle.Fill;
            popOutForm.WindowState = FormWindowState.Maximized;

            popOutForm.FormClosing += PopOutForm_FormClosing;
            popOutForm.FormClosed += PopOutForm_FormClosed;
            popOutForm.BringToFront();
            popOutForm.Show();
        }

        public DataGridView data = new DataGridView();

        private void PopOutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            data = dgvPopedOut;
        }

        private void PopOutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dgvOptionPackagesInFolder = new DataGridView();
            this.tableLayoutPanel2.Controls.Add(this.dgvOptionPackagesInFolder, 1, 6);
            dgvOptionPackagesInFolder.Dock = DockStyle.Fill;

            dgvOptionPackagesInFolder = data;
            dgvOptionPackagesInFolder.Visible = true;
            popOutForm.Dispose();
        }

        private void btShowPackagesInRemotePath_Click(object sender, EventArgs e)
        {
            ArtifactHandler.Instance.LastBuildPath = ArtifactHandler.Instance.ReadOutLastBuildPath(Path.Combine(ArtifactHandler.Instance.RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            CommandControler.Instance.CheckPackagesInFolder(ArtifactHandler.Instance.LastBuildPath.FullName, out bindingSrcForRemotePackages);
            dgvOptionPackagesInFolder.Visible = true;
            //  PrepareDataGridView(dgvOptionPackagesInFolder);
            ConfigureDgvForPackagesInFolder(dgvOptionPackagesInFolder, bindingSrcForRemotePackages);
        }
    }
}