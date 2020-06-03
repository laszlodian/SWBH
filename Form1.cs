﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.ConfiguredTaskAwaitable;

namespace SWB_OptionPackageInstaller
{
    public partial class Form1 : Form
    {
        #region Properties

        private Control control;

        public Control Control
        {
            get { return control; }
            set { control = value; }
        }

        internal void SetDefaultValues()
        {
            tbPathOfPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
            tbPathOfSWB.Text = Properties.Settings.Default.DefaultSWBPath;
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

        private bool[] pathsAreValid = new bool[] { false, false };

        public bool[] PathsAreValid
        {
            get { return pathsAreValid; }
            set { pathsAreValid = value; }
        }

        private DirectoryInfo lastBuildPath;

        public DirectoryInfo LastBuildPath
        {
            get { return lastBuildPath; }
            set { lastBuildPath = value; }
        }

        public DataGridView DataGridViewOfArtifacts { get { return dgvInstalledOPs; } set { dgvInstalledOPs = value; } }

        private TextBox tbServerPath;

        public TextBox TbServerPath
        {
            get { return tbServerPath; }
            set { tbServerPath = value; }
        }

        private bool collectionFinished = false;

        public bool CollectionFinished
        {
            get
            {
                return collectionFinished;
            }
            set
            {
                collectionFinished = value;
            }
        }

        public TextBox RemoteDropDown
        {
            get { return tbOptionPackagesServer; }
            set { tbOptionPackagesServer = value; }
        }

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

        private delegate void UpdateTextBoxDelegate(string text);

        public void UpdateTextBox(string text_in)
        {
            if (tbInfo1.InvokeRequired)
            {
                tbInfo1.Invoke(new UpdateTextBoxDelegate(UpdateTextBox), text_in);
            }
            else
                tbInfo1.Text = text_in;
            //  tbInfo1.Refresh();

            if (tbInfo2.InvokeRequired)
            {
                tbInfo2.Invoke(new UpdateTextBoxDelegate(UpdateTextBox), text_in);
            }
            else
                tbInfo2.Text = text_in;
            //    tbInfo1.Refresh();
        }

        private delegate void AppendTextBoxDelegate(string text);

        public void AppendTextBox(string text_in)
        {
            if (tbInfo1.InvokeRequired)
            {
                tbInfo1.Invoke(new AppendTextBoxDelegate(AppendTextBox), text_in);
            }
            else
                tbInfo1.Text += "\r\n" + text_in;

            if (tbInfo2.InvokeRequired)
            {
                tbInfo2.Invoke(new AppendTextBoxDelegate(AppendTextBox), text_in);
            }
            else
                tbInfo2.Text += "\r\n" + text_in;
        }

        private delegate void AppendUpdateStatusDelegate(string status);

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

        public void AppendUpdateStatus(string status)
        {
            if (this.lbInfoText.InvokeRequired)
            {
                this.Invoke(new AppendUpdateStatusDelegate(this.UpdateStatus), new object[] { status });
                return;
            }

            this.lbInfoText.Text += status;
            this.lbInfoText.Refresh();

            if (this.lbInfoText2.InvokeRequired)
            {
                this.Invoke(new AppendUpdateStatusDelegate(this.UpdateStatus), new object[] { status });
                return;
            }

            this.lbInfoText2.Text += status;
            this.lbInfoText2.Refresh();
        }

        #endregion Delegates

        /// <summary>
        /// Default constructor
        /// </summary>
        public Form1()
        {
            Instance = this;
            InitializeComponent();

            theTabControl = mainTabControl;
            tbServerPath = theTabControl.TabPages[2].Controls["tableLayoutPanel2"].Controls["tbOptionPackagesServer"] as TextBox;

            tbPathOfSWB.Text = Properties.Settings.Default.LastUsedSWBPath;
            tbPathOfPackages.Text = Properties.Settings.Default.LastUsedOptionPackagePath;

            TraceHelper.SetupListener();
            UpdateStatus("Loading Form1....");

            dgvInstalledOPs.Dock = DockStyle.Fill;
            dgvInstalledOPs.BackgroundColor = Color.LightCyan;

            CommandControler.Instance.Validate(tbPathOfSWB);
            CommandControler.Instance.Validate(tbPathOfPackages);

            Properties.Settings.Default.Save();

            UpdateStatus("Browse for a path or simlpy type it in to the textbox");

            this.Load += SWBForm1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        public Control GetReferencedControl(string controlName)
        {
            Control c = null;

            if (controlName == "tbOptionPackagesServer")
            {
                c = ((this.Controls[theTabControl.Name] as TabControl).TabPages[2].Controls["tableLayoutPanel2"].Controls[controlName] as TextBox);

                return (c as TextBox);
            }

            foreach (Control control in this.Controls)
            {
                if (control.GetType().Name == theTabControl.Name)
                {
                    foreach (Control item in control.Controls)
                    {
                        if ((item.Name == tableLayoutPanel1.Name) || (item.Name == tableLayoutPanel2.Name))
                        {
                            foreach (Control control1 in item.Controls)
                            {
                                if (control1.Name == controlName)
                                {
                                    c = control1;
                                }
                            }
                        }
                    }
                }
            }
            return c;
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
            Text = string.Format("{0}       {1}", this.Text, SwVersion.Instance.ActualSWVersion);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
            tbPathOfPackages.Text = Properties.Settings.Default.DefaultOptionPackagePath;
            tbPathOfSWB.Text = Properties.Settings.Default.DefaultSWBPath;
        }

        private void SWBForm1_Layout(object sender, LayoutEventArgs e)
        {
            UpdateStatus("Building DataGrid for reprezenting features...");
            EnumsAndComboBox_Load_For_All(sender, e);
        }

        public void ConfigureDgvForPackagesInFolder()
        {
            UpdateStatus("DataGrid has been created, filling it in with corresponding values...");
            // Initialize the DataGridView.
            dgvForPackagesInFolder.AutoGenerateColumns = false;
            dgvForPackagesInFolder.AutoSize = false;
            dgvForPackagesInFolder.Dock = DockStyle.Fill;
            dgvForPackagesInFolder.DataSource = BindingSourceForFoundPackages;

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "No";
            column.Width = 50;
            column.Name = "No.";
            dgvForPackagesInFolder.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Option Package Name";
            column.Width = 500;
            dgvForPackagesInFolder.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Install this package";
            column.Width = 150;
            dgvForPackagesInFolder.Columns.Add(column);

            dgvForPackagesInFolder.AutoResizeColumnHeadersHeight();
            dgvForPackagesInFolder.AutoResizeColumns();
            dgvForPackagesInFolder.AutoResizeRows();
            // Initialize the form.
            dgvForPackagesInFolder.Text = "Founded option packages in folder - Choose which of them to install to SunriseWorkBench";
        }

        public void EnumsAndComboBox_Load_For_All(object sender, EventArgs e)
        {
            UpdateStatus("DataGrid has been created, filling it in with corresponding values...");
            // Initialize the DataGridView.
            dgvInstalledOPs.AutoGenerateColumns = false;
            dgvInstalledOPs.AutoSize = false;
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
            dgvInstalledOPs.AutoSize = true;
            dgvInstalledOPs.Text = "Founded option packages - Choose which of them to install to SunriseWorkBench";
        }

        private delegate void SetStartSWBButtonVisibleDelegate();

        public void SetStartSWBButtonVisible()
        {
            if (btStartSWB.InvokeRequired)
            {
                btStartSWB.Invoke(new SetStartSWBButtonVisibleDelegate(SetStartSWBButtonVisible));
            }
            else
            {
                btStartSWB.Visible = true;
                btStartSWB.Focus();
            }
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

        private void btOK_Click(object sender, EventArgs e)
        {
            UpdateStatus("Process started...");
            CommandControler.Instance.CorrectTextBoxesPath();

            UpdateStatus("Preparing to getting option packages...");
            CommandControler.Instance.CollectOptionPackages(PathOfOptionPackages);

            UpdateStatus("Get versions for SWB and all of the available option packages...");
            CommandControler.Instance.GetFeaturesVersions(CommandControler.Instance.OptionPackageList);

            ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => ConfigureDataGridView(CommandControler.Instance.OptionPackageList))));

            UpdateTextBox("Start installing features, be patient...");
            installThread = new Thread(new ThreadStart(CommandControler.Instance.InstallOptionPackages));
            installThread.SetApartmentState(ApartmentState.MTA);
            installThread.Name = "InstallOptionPackages";
            ThreadManager.Instance.StartAndWaitOneThread(installThread);

            UpdateTextBox(string.Empty);
            UpdateStatus("Progress is done.");
        }

        private void btBrowseSWBPath_Click(object sender, EventArgs e)
        {
            CommandControler.Instance.OpenFolderBrowser(tbPathOfSWB);
        }

        private void btBrowseForPackages_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btBrowseSWBPath")
            {
                CommandControler.Instance.OpenFolderBrowser(tbPathOfSWB);
            }
            else
                CommandControler.Instance.OpenFolderBrowser(tbPathOfPackages);
        }

        private void tbPathOfSWB_TextChanged(object sender, EventArgs e)
        {
            pathOfSWB = Path.GetFullPath(tbPathOfSWB.Text);
        }

        private void tbPathOfPackages_TextChanged(object sender, EventArgs e)
        {
            pathOfOptionPackages = Path.GetFullPath(tbPathOfPackages.Text);
        }

        private void btCollect_Click(object sender, EventArgs e)
        {
            CommandControler.Instance.PrepareAndFinalizeRemoteDropDownCopyingOptionPackages();
        }

        private void tbOptionPackagesServer_MouseHover(object sender, EventArgs e)
        {
            ToolTip remoteLocation = new ToolTip();
            remoteLocation.SetToolTip(tbOptionPackagesServer, tbOptionPackagesServer.Text);
            remoteLocation.Active = true;
        }

        private void btStartSWB_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(PathOfSWB, "SunriseWorkbench.exe"), string.Format("-data \"{0}\"", Path.Combine(PathOfSWB, "_WS")));
        }

        private void CleanUpEverything_Click(object sender, EventArgs e)
        {
            UpdateStatus("Delete all generated,copied resources..");
            Directory.Delete(destinationDir.FullName, true);
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
            dgvForPackagesInFolder.Visible = true;
            PrepareDataGridView(dgvForPackagesInFolder, 0);
            ConfigureDgvForPackagesInFolder();
        }

        public DataGridView GetDGVForCollectedOPs()
        {
            return dgv_collectedOPs;
        }

        public DataGridView GetDGVForInstalledOPs()
        {
            return dgvInstalledOPs;
        }

        public void PrepareDataGridView(DataGridView dataGridView, int tabNumber)
        {
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSize = true;
            if (tabNumber != 0)
                theTabControl.TabPages[tabNumber].Controls.Add(dataGridView);
            else
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
    }
}