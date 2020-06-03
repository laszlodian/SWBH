using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using ThreadPoolHelper;
using ThreadTaskManager.Library;
using ThreadWorker;
using Timer = System.Windows.Forms.Timer;

namespace SWB_OptionPackageInstaller
{
    public class CommandControler
    {
        public Dictionary<string, string> PackagesInfo = new Dictionary<string, string>();
        public static CommandControler Instance = null;
        public Regex buildDirectoryRegex = new Regex("^build1.16.0_NavB[0-9]{4}_.{0,100}$");
        public string remoteNAVRepo = @"\\KUKA.int.kuka.com\s\KROS_Pool\Daily\NavigationSolution\master\";
        public List<string> allBuildDirectoryUnderMasterFolder = new List<string>();

        /// <summary>
        /// The first argument must look like this: jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/"
        /// The second argument is the name of the feature and it looks like this: com.kuka.kmpOmniMove.swb.feature.feature.group -This could be listed out with 'featuresListCommand'
        /// </summary>
        public string installCommandFormat = @"{1}eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} {2}";

        /// <summary>
        /// First argument should look like this: "file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Sunrise/SunriseWorkbench-1.16.2.16-win32-x86.zip!/"
        /// </summary>
        public string featuresListCommandFormat = @"{1}eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} -list";

        private List<string> packages = new List<string>();
        public string realWorkingCommand = "C:/_SWB/SWB/eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash -repository jar:\"file:///C:/_SWB/optionpackages/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345991;01;KUKA Sunrise.Mobility 1.11 KMP 1500.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345992;01;KUKA Sunrise.Mobility 1.11 KMP 200.zip!/\" -installIUs com.kuka.kmpOmniMove.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove1500.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove200.swb.feature.feature.group";

        #region Properties

        private int opCountInFolder;

        public int OPCountInFolder { get { return opCountInFolder; } set { opCountInFolder = value; } }

        private List<string> collectedOPs;

        public List<string> CollectedOPs
        {
            get { return collectedOPs; }
            set { collectedOPs = value; }
        }

        private DirectoryInfo swbDestPath;

        public DirectoryInfo SwbDestPath
        {
            get { return swbDestPath; }
            set { swbDestPath = value; }
        }

        private string remoteDropDownPath = @"\\KUKA.int.kuka.com\s\KROS_Pool\Daily\NavigationSolution\master\";

        public string RemoteDropDownPath
        {
            get { return remoteDropDownPath; }
            set { remoteDropDownPath = value; }
        }

        public DirectoryInfo logDir { get; private set; }

        private DirectoryInfo destinationDir;

        public DirectoryInfo swbDir { get; private set; }

        public DirectoryInfo DestinationDir
        {
            get { return destinationDir; }
            set { destinationDir = value; }
        }

        private FileInfo swbBuildPath;

        public FileInfo SwbBuildPath

        {
            get { return swbBuildPath; }
            set { swbBuildPath = value; }
        }

        private string sunriseWorkbenchVersion = string.Empty;

        public string SunriseWorkbenchVersion
        {
            get { return sunriseWorkbenchVersion; }
            set { sunriseWorkbenchVersion = value; }
        }

        private string sWBZipFilePath;

        public string SWBZipFilePath { get { return sWBZipFilePath; } set { sWBZipFilePath = value; } }

        private Thread th;
        private string optionPackageList;

        public string OptionPackageList
        {
            get { return optionPackageList; }
            set
            {
                optionPackageList = value;
            }
        }

        private bool allOPsFound;

        public bool AllOPsFound
        {
            get { return allOPsFound; }
            set { allOPsFound = value; }
        }

        private string swbPath;

        public string SwbPath { get { return swbPath; } set { swbPath = value; } }

        private DirectoryInfo lastBuildPath;

        public DirectoryInfo LastBuildPath { get { return lastBuildPath; } set { lastBuildPath = value; } }

        public string ActProduct { get { return actProduct; } set { actProduct = value; } }

        private bool collectionFinished;
        public bool CollectionFinished { get { return collectionFinished; } set { collectionFinished = value; } }

        public string actProduct;//{ get; private set; }

        #endregion Properties

        #region Variables

        public DataGridView dgv = new DataGridView();

        //  private BindingSource bindingSourceForCollectedPackages = new BindingSource();
        //   private BindingSource bindingSourceForIstalledPackages = new BindingSource();
        private List<FileInfo> opsInFolder = new List<FileInfo>();

        private bool swbFoundInfolder;
        private BackgroundWorker bgWorker;
        private int progress = 0;
        private List<string> listOfFeatures = new List<string>();
        private List<string> packagesNames = new List<string>();
        public List<string> features = new List<string>();
        public List<string> versions = new List<string>();
        public int PackagesCount = 0;
        private string ArtifactToCopy;

        #endregion Variables

        private DirectoryInfo lastBuildDirectory = null;

        public CommandControler()
        {
            Instance = this;
            TraceHelper.MarkTraceFileForStore("COMMANDCONTROLLER");
            Trace.TraceInformation("Trace marked for store with name: {0}", "COMMANDCONTROLLER");

            remoteDropDownPath = @"\\KUKA.int.kuka.com\s\KROS_Pool\Daily\NavigationSolution\master\";
        }

        private Thread unzipSWBThread;

        private delegate void unzipSWBThreadStarting(Action unzippingSWB);

        public void CollectOptionPackages(string pathIn)
        {
            bool isSWBfound = false;

            DirectoryInfo optionPackagesPath = new DirectoryInfo(pathIn);

            foreach (string package in Directory.GetFiles(pathIn, "*.zip"))
            {
                Form1.Instance.UpdateTextBox(String.Format("Found feature: {0}", package));

                if (package.Contains("SunriseWorkbench"))
                {
                    Form1.Instance.UpdateStatus("Decompressing SunriseWorkbench....");
                    SWBZipFilePath = package;

                    Thread thread = CreateTheUnzippingThread();
                    thread.Priority = ThreadPriority.Highest;
                    ThreadManager.Instance.StartAndWaitOneThread(thread);
                    //    ThreadManager.Instance.WaitAllThreadToFinishWork();

                    isSWBfound = true;
                    continue;
                }
                PackagesCount++;
                packages.Add(package);
                packagesNames.Add(package.Substring(package.LastIndexOf("\\")));
            }
            if (!isSWBfound)
            {
                MessageBox.Show(String.Format("SunriseWorkbench zip file not found at given path: {0}!", Form1.Instance.PathOfOptionPackages));
            }
            Trace.TraceInformation("{0} packages collected.", packages.Count());
            Form1.Instance.UpdateTextBox(String.Format("Found package count: {0}", packages.Count));

            OptionPackageList = FormatPackageNames(packages);
        }

        private Thread CreateTheUnzippingThread()
        {
            unzipSWBThread = new Thread(new ThreadStart(UnzipSunriseWorkbench));
            unzipSWBThread.Name = "UnzipSunriseWorkbench";
            unzipSWBThread.SetApartmentState(ApartmentState.MTA);

            return unzipSWBThread;
        }

        /*   private async void NewMethod(CastingPipelineWithAwait<IAwaitablePipeline<Task>> pipeline)
           {
               await pipeline.Execute(StartingUnzipThreadWithPipeline);
           }

           public async Task StartingUnzipThreadWithPipeline(Thread unzipSWBThread)
           {
               unzipSWBThread.Start();
           }*/

        public void UnzipSunriseWorkbench()
        {
            if (Directory.Exists(Form1.Instance.PathOfSWB))
            {
                foreach (string item in Directory.GetFiles(Form1.Instance.PathOfSWB))
                {
                    File.Delete(item);
                }
            }
            else
                Directory.CreateDirectory(Form1.Instance.PathOfSWB);

            new Thread(new ThreadStart(() => SetupTimer())).Start();

            ZipFile.ExtractToDirectory(SWBZipFilePath, Form1.Instance.PathOfSWB);

            using (StreamReader sr = new StreamReader(Path.Combine(Form1.Instance.PathOfSWB, ".eclipseproduct")))
            {
                string line = sr.ReadLine();
                while (line != string.Empty || line != null)
                {
                    if (line.Contains("version"))
                    {
                        SunriseWorkbenchVersion = line.Split('=')[1].Trim();
                        if (string.IsNullOrEmpty(SunriseWorkbenchVersion))
                        {
                            MessageBox.Show("Couldn't find SWB version!");
                        }
                        Trace.TraceInformation("SWB version: {0}", SunriseWorkbenchVersion);
                        break;
                    }
                    line = sr.ReadLine();
                }
            }
        }

        private void SetupTimer()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1;
            timer.Enabled = true;
            timer.Disposed += Timer_Disposed;
            timer.Start();
        }

        private void Timer_Disposed(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progress++;
            Form1.Instance.UpdateTextBox(String.Format("Decompress SWB: {0}%", progress));
            if (progress >= 100)
            {
                (sender as Timer).Dispose();
            }
        }

        private void TimerForDecompressing(ZipArchive zipArchive)
        {
            int percent = 0;
            foreach (ZipArchiveEntry item in zipArchive.Entries)
            {
                UpdateStatus(string.Format("Decompress SWB: {0}%", percent++), "tbInfo1");
                Thread.Sleep(100);
            }
        }

        public bool CheckPath(string pathIn)
        {
            Trace.TraceInformation("Checking path: {0}", pathIn);

            if (!Directory.Exists(pathIn))
            {
                Directory.CreateDirectory(string.Format("{0}_{1}", pathIn, DateTime.Now.Millisecond));
                ShowImportantMessageDialog("The given path of the SWB does not exists! Created directory: {0}", string.Format("{0}_{1}", pathIn, DateTime.Now.Millisecond));

                return true;
            }
            else
                return true;
        }

        public bool CheckPath(ref string pathIn)
        {
            Trace.TraceInformation("Checking path: {0}", pathIn);
            if (!Directory.Exists(pathIn))
            {
                Directory.CreateDirectory(string.Format("{0}_{1}", pathIn, DateTime.Now.Millisecond));
                ShowImportantMessageDialog("The given path of the SWB does not exists! Created directory: {0}", string.Format("{0}_{1}", pathIn, DateTime.Now.Millisecond));

                return true;
            }
            else
                return true;
        }

        public string FormatPackageNames(List<string> packages)
        {
            string stringToInsertCommand = string.Empty;

            foreach (string item in packages)
            {
                stringToInsertCommand += string.Format(" -repository jar:\"file:///{0}!/\"", item);
            }
            Trace.TraceInformation("Complete command: {0}", stringToInsertCommand);
            return stringToInsertCommand;
        }

        public void InstallOptionPackages()
        {
            Form1.Instance.UpdateStatus("Collecting features to install...");
            string commandToRun = string.Empty;
            optionPackageList = CommandControler.Instance.OptionPackageList;

            if (!CheckOptionPackages())
                return;

            commandToRun = string.Format(featuresListCommandFormat, OptionPackageList, Form1.Instance.PathOfSWB).Replace('\\', '/');
            Trace.TraceInformation("Command to run for list features:{1} {0}", commandToRun, Environment.NewLine);

            string featuresListResult = Run(commandToRun);
            Trace.TraceInformation("Feature command result: \r\n{0}", featuresListResult);

            List<string> featuresList = GetFeatures(featuresListResult);
            string featuresCommandPart = BuildFeaturesStringForCommand(featuresList);
            Trace.TraceInformation("Features part of command: {0}", featuresCommandPart);

            ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => IterateOverFeatures(features))));

            commandToRun = string.Format(installCommandFormat, OptionPackageList, Form1.Instance.PathOfSWB, featuresCommandPart);
            Trace.TraceInformation("Command to run for install packages: {0}", commandToRun);
            Run(commandToRun);

            Form1.Instance.UpdateStatus("Storing versions, date and time, username, pc-name to database...");

            if (Properties.Settings.Default.StoreToDB)
            {
                SQLManager.Instance.StoreInstall();
            }
            Form1.Instance.SetStartSWBButtonVisible();
        }

        public void IterateOverFeatures(List<string> features)
        {
            foreach (string item in features)
            {
                Form1.Instance.UpdateStatus(String.Format("Installing feature: {0}", item));
            }
        }

        private bool CheckOptionPackages()
        {
            Form1.Instance.UpdateStatus("Checking option packages");
            if (OptionPackageList == string.Empty || OptionPackageList == null)
            {
                return false;
            }
            else
            {
                OptionPackageList = OptionPackageList.Replace('\\', '/');
                Trace.TraceInformation("Option packages: {0}", OptionPackageList);
                foreach (string item in OptionPackageList.Split('/'))
                {
                    Form1.Instance.UpdateTextBox(string.Format("Collected Option Package: {0}", item));
                }

                return true;
            }
        }

        public void GetFeaturesVersions(string optionPackageList)
        {
            Form1.Instance.UpdateStatus("Getting features version...");
            string versionCommand = string.Format(featuresListCommandFormat, optionPackageList, Form1.Instance.PathOfSWB);

            string versionCommandResult = Run(versionCommand);

            Trace.TraceInformation(versionCommandResult);
            foreach (string version in versionCommandResult.Split('\n'))
            {
                Trace.TraceInformation(version);
                if (version.Contains("feature.group"))
                {
                    if (!features.Contains(version.Split('=')[0].Trim()))
                    {
                        Form1.Instance.UpdateTextBox(string.Format("{0} feature version: {1}", version.Split('=')[0].Trim(), version.Split('=')[1].Trim()));
                        PackagesInfo.Add(version.Split('=')[0].Trim(), version.Split('=')[1].Trim());
                        features.Add(version.Split('=')[0].Trim());
                        versions.Add(version.Split('=')[1].Trim());
                    }
                }
            }
            Trace.TraceInformation("PackagesInfo Count: {0}; Versions Count: {1}", PackagesInfo.Count, versions.Count);
          //  Form1.Instance.handle.Set();
            if (versions.Count == 0)
            {
                ShowImportantMessageDialog(String.Format("No compatible file found in the given folder.\r\nMay be a mistake?\r\nYou gave this path: {0}", Form1.Instance.PathOfSWB));

                Trace.TraceError("No version collected");

                UpdateStatus("The application cleaning up the allocated resources and cleaning up the temporary files....", "lbInfoText");

                Application.Exit();
            }
        }

        private string BuildFeaturesStringForCommand(List<string> featuresList)
        {
            string featuresForCommand = string.Empty;
            foreach (string feature in featuresList)
            {
                featuresForCommand += string.Format(" -installIUs {0}", feature);
            }

            return featuresForCommand;
        }

        private List<string> GetFeatures(string featuresListResult)
        {
            List<string> featuresNeededForCommand = new List<string>();

            foreach (string row in featuresListResult.Split('\n'))
            {
                if ((row.Contains("feature.feature.group")) || ((row.Contains("feature.group")) && (row.Contains("nav.core"))))
                {
                    string feature = row.Split('=')[0];
                    if (!featuresNeededForCommand.Contains(feature.Trim()))
                    {
                        Trace.TraceInformation("Feature found: {0}", feature.Trim());
                        featuresNeededForCommand.Add(feature.Trim());
                    }
                }
            }
            return featuresNeededForCommand;
        }

        public string Run(string cmd)
        {
            if (cmd.Contains("installIUs"))
                Form1.Instance.UpdateTextBox(string.Format("Running command: {0}", "Install Features"));
            else if (cmd.Contains("list"))
                Form1.Instance.UpdateTextBox(string.Format("Running command: {0}", "Get All Features"));

            Trace.TraceInformation("Command to Run: {0}", cmd);
            var processInfo = new ProcessStartInfo("cmd.exe", string.Format("/c {0}", cmd))
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = @"C:\temp\"
            };

            StringBuilder sb = new StringBuilder();
            Process p = Process.Start(processInfo);
            p.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);

            p.BeginOutputReadLine();
            p.WaitForExit();

            return sb.ToString();
        }

        public void CorrectTextBoxesPath()
        {
            Form1.Instance.PathOfSWB = Path.GetFullPath(Form1.Instance.PathOfSWB);
            if (!(Form1.Instance.PathOfSWB.EndsWith("\\") || Form1.Instance.PathOfSWB.EndsWith("/")))
            {
                Form1.Instance.PathOfSWB += "/";
            }
            Form1.Instance.PathOfOptionPackages = Path.GetFullPath(Form1.Instance.PathOfOptionPackages);
        }

        public DirectoryInfo LookUpForLastBuildDirectory(string buildNumber, string lastBuildPath)
        {
            DirectoryInfo sourceFolderOfRemoteBuild = null;

            Form1.Instance.UpdateStatus("Connected to remote dropdown folder, initializing copy process");

            foreach (string actDirectory in (Directory.GetDirectories(lastBuildPath)))
            {
                if (actDirectory.Contains(buildNumber))
                {
                    sourceFolderOfRemoteBuild = new DirectoryInfo(actDirectory);
                }
            }

            Form1.Instance.UpdateStatus(String.Format("Last build directory:", sourceFolderOfRemoteBuild));

            if (sourceFolderOfRemoteBuild == null)
            {
                throw new Exception("lastBuildDirectory couldn't found!");
            }

            return sourceFolderOfRemoteBuild;
        }

        public void CheckPackagesInFolder(string pathOfOptionPackages)
        {
            Form1.Instance.BindingSourceForFoundPackages = new BindingSource();
            foreach (FileInfo opFile in new DirectoryInfo(pathOfOptionPackages).GetFiles("*.zip", SearchOption.TopDirectoryOnly))
            {
                opsInFolder.Add(opFile);
                opCountInFolder++;
                Form1.Instance.BindingSourceForFoundPackages.Add(new PackageGridModel(opFile.Name, opCountInFolder, true));
            }
        }

        public void CollectAndShowAvailableBuildDirectories(DirectoryInfo lastBuildDirectory)
        {
            string masterDirectory = lastBuildDirectory.FullName.Substring(0, lastBuildDirectory.FullName.LastIndexOf('\\') + 1);

            foreach (string buildDir in Directory.GetDirectories(masterDirectory))
            {
                if (buildDirectoryRegex.IsMatch(buildDir.Substring(buildDir.LastIndexOf("\\") + 1)))
                {
                    allBuildDirectoryUnderMasterFolder.Add(buildDir);
                }
            }

            DialogResult res = new ChooseDirectoryForm().ShowDialog();
            if (res == DialogResult.Yes)
            {
                PrepareAndFinalizeRemoteDropDownCopyingOptionPackages();
            }
        }

        public void PrepareAndFinalizeRemoteDropDownCopyingOptionPackages()
        {
            PrepareThenCopyResources();
            Form1.Instance.UpdateStatus("Nececcary option packages has been copied from the latest NAV master build");

            FillDatagridView();
            ConfigureCollectedOPsDatagrid(collectedOPs);

            // ThreadManager.Instance.WaitAllThreadToFinishWork();

            CommandControler.Instance.CheckIfInstallationNeeded();
        }

        public void FillDatagridView()
        {
            UpdateStatus("Building DataGrid for reprezenting features...", "lbInfoText");
            Form1.Instance.PrepareDataGridView(Form1.Instance.GetDGVForCollectedOPs(), 3);

            Form1.Instance.EnumsAndComboBox_Load_For_CollectedOPs();
        }

        public void CheckIfInstallationNeeded()
        {
            DialogResult res = MessageBox.Show("Continue with installation?", "Packages are ready for installation", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Form1.Instance.SetInstallationPageValues();
            }
        }

        private void CreateSWBTestDirectoryHierarchy()
        {
            string destinationDirString = string.Format(@"c:\_SWB\{0}_{1}_{2}\", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day);

            DialogResult res = MessageBox.Show(String.Format("The program will create the following libraries:\r\n-'logs'\r\n-{0} (to unzip SWB)\r\nAre u agreed?", destinationDirString), "Creating Logs directory", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                UpdateStatus("Cleaning up resources...", "lbInfoText");
                new CleanUpManager();
            }

            logDir = Directory.CreateDirectory(Path.Combine(string.Format(@"c:\_SWB\{0}_{1}_{2}\{3}", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day, "logs")));
            destinationDir = Directory.CreateDirectory(Path.Combine(string.Format(@"c:\_SWB\{0}_{1}_{2}\", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day)));
            swbDir = Directory.CreateDirectory(string.Format(@"c:\_SWB\{0}_{1}_{2}\SWB", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day));

            if (logDir.Exists && destinationDir.Exists && swbDir.Exists)
            {
                Trace.TraceInformation("Directories succcesfully created");
            }
        }

        private void ReadOutLastBuildNumber(string lastBuildPathFile)
        {
            FileStream fileStream = new FileStream(lastBuildPathFile, FileMode.Open, FileAccess.Read);
            string line = string.Empty;

            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                line = streamReader.ReadLine();
                streamReader.Close();
            }

            if (string.IsNullOrEmpty(line))
            {
                ShowImportantMessageDialog("Last Build Number Not Found!", "Couldn't read out last build number", MessageBoxIcon.Error);
                ShowImportantMessageDialog("Application is now exit...", "Exit application - No Build number found");

                Application.Exit();
            }
            //else
            //    ShowImportantMessageDialog(string.Format("Last build number: {0}", line.Trim()));

            Form1.Instance.UpdateStatus(string.Format("Last build number: {0}", line.Trim()));
            PreparingToCopyOPsFromRemotePath(lastBuildPathFile, line);
        }

        private void ShowImportantMessageDialog(string textToShow, string captionOfDialog = "Important Information")
        {
            MessageBox.Show(String.Format("{0}", textToShow), captionOfDialog, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowQuestionDialog(string textToShow, string captionOfDialog = "Question - User answer needed")
        {
            MessageBox.Show(String.Format("{0}", textToShow), captionOfDialog, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void PreparingToCopyOPsFromRemotePath(string lastBuildPathFile, string line)
        {
            UpdateStatus("Application starts to copy the nececcary option packages, please be patient..", "lbInfoText");
            ShowImportantMessageDialog("Application starts to copy the nececcary option packages, please be patient..");

            lastBuildPath = GetLastBuildDirectory(line, lastBuildPathFile);

            ReadOutSWBPathFromJSON(lastBuildPath);

            CopyOptionPackagesFromRemoteDropDownFolder(line, RemoteDropDownPath);
        }

        private DirectoryInfo GetLastBuildDirectory(string buildNumber, string lastBuildPathFile)
        {
            foreach (string actDirectory in (Directory.GetDirectories(lastBuildPathFile.Substring(0, lastBuildPathFile.LastIndexOf('L')))))
            {
                if (actDirectory.Contains(buildNumber))
                {
                    lastBuildDirectory = new DirectoryInfo(actDirectory);
                    break;
                }
            }

            if (lastBuildDirectory == null)
            {
                ShowImportantMessageDialog("Last Build Directory not found!");
                Application.Exit();
            }

            return lastBuildDirectory;
        }

        private void ReadOutSWBPathFromJSON(DirectoryInfo lastBuildDir)
        {
            string readedLine = string.Empty;
            foreach (string item in Directory.GetFiles(lastBuildDir.FullName, "*.json"))
            {
                if (item.EndsWith("settings_core.json"))
                {
                    using (StreamReader sr = new StreamReader(item))
                    {
                        readedLine = sr.ReadLine();
                        while (readedLine != string.Empty)
                        {
                            if (readedLine.Contains("CoreBranchRepo"))
                            {
                                swbPath = readedLine.Split(':')[1].Trim(new char[] { '"', ',' });
                                swbPath = swbPath.Substring(swbPath.IndexOf("//"));

                                Thread copyProductThread = new Thread(new ThreadStart(() => CopySWBFromRemoteLocation(Path.GetFullPath(Path.Combine(swbPath, "Product")))));
                                //   copyProductThread.Start();
                                ThreadManager.Instance.StartAndWaitOneThread(copyProductThread);
                                break;
                            }
                            readedLine = sr.ReadLine();
                        }
                    }
                }
            }
        }

        private void ConfigureCollectedOPsDatagrid(List<string> collectedOPs)
        {
            UpdateStatus("Configuring DataGrid", "lbInfoText");
            string clearPackageName = string.Empty;
            int i = 0;

            foreach (string pkg in collectedOPs)
            {
                Form1.Instance.UpdateStatus(string.Format("Add to datagrid: {0}", pkg));
                Form1.Instance.bindingSourceForCollectedPackages.Add(new PackageGridModel(pkg, lastBuildDirectory.Name, true));
                i++;
            }

            Form1.Instance.dgv_collectedOPs.Font = new Font(dgv.Font, FontStyle.Regular);
            Form1.Instance.EnumsAndComboBox_Load_For_CollectedOPs();
        }

        public void ShowImportantMessageDialog(string textToShow, string captionOfDialog, MessageBoxIcon iconToShow = MessageBoxIcon.Error)
        {
            MessageBox.Show(String.Format("{0}", textToShow), captionOfDialog, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private delegate void UpdateStatusDelegate(string txt, string name);

        public void UpdateStatus(string info, string name)
        {
            Control c = Form1.Instance.GetReferencedControl(name);

            if ((c != null) && (c.GetType() == typeof(Control)))
            {
                if ((c as TextBox).Name.Contains("lbInfo") || (c as TextBox).Name.Contains("tbInfo"))
                {
                    if ((c as TextBox).InvokeRequired)
                    {
                        (c as TextBox).Invoke(new UpdateStatusDelegate(UpdateStatus), new string[] { info, name });
                    }
                    else
                        (c as TextBox).Text = info;
                }
            }
        }

        private void CopyProcessOfArtifactsAndProducts(DirectoryInfo lastBuildDirectory)
        {
            Thread cpyArtifacts = new Thread(new ThreadStart(() =>
            {
                foreach (string item in Directory.GetFiles(String.Format("{0}{1}{2}", lastBuildDirectory.FullName, Path.DirectorySeparatorChar, "Artifacts"), "*.zip"))
                {
                    Form1.Instance.UpdateStatus(string.Format("Processing artifact: {0}", item));
                    if (Properties.Settings.Default.ArtifactsNeededToCopy.Contains(Path.GetFileName(item)))
                    {
                        Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));

                        CopyArtifactsFromRemoteFolder(item);

                        collectedOPs.Add(Path.GetFileName(item));
                    }
                }
            }));
            ThreadManager.Instance.StartAndWaitOneThread(cpyArtifacts);

            Thread cpyProducts = new Thread(new ThreadStart(() =>
            {
                foreach (string item in Directory.GetFiles(Path.GetFullPath(Path.Combine(lastBuildDirectory.FullName, "Product")), "*.zip"))
                {
                    Form1.Instance.UpdateStatus(string.Format("Processing product: {0}", item));
                    if (Properties.Settings.Default.ProductsNeededToCopy.Contains(Path.GetFileName(item)))
                    {
                        Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));

                        CopyProductsFromRemoteFolder(item);
                        // File.Copy(item, String.Format("{0}{1}{2}", Path.GetDirectoryName(destinationDir.FullName), Path.DirectorySeparatorChar, Path.GetFileName(item)), true);//));
                        collectedOPs.Add(Path.GetFileName(item));
                        UpdateStatus("Copy Product option packages has finished.", "tbInfo");
                    }
                }
            }));
            ThreadManager.Instance.StartAndWaitOneThread(cpyProducts);

            if (!CheckOptionPackagesCount(destinationDir.GetFiles("*.zip", SearchOption.TopDirectoryOnly).Length - 1, Properties.Settings.Default.ArtifactsNeededToCopy.Count + Properties.Settings.Default.ProductsNeededToCopy.Count))
            {
                DialogResult result = MessageBox.Show("The option packages in the last Build directory is less than the expected. Some of them are missing.\r\nDo you want to search for a previously build?", "Not all option packages are found!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CommandControler.Instance.CollectAndShowAvailableBuildDirectories(lastBuildDirectory);
                }
            }
            else
                CollectionFinished = true;
        }

        public bool Validate(TextBox textbox_in)
        {
            bool res = true;

            if (!textbox_in.Name.EndsWith("SWB"))
            {
                if (string.IsNullOrEmpty(Path.GetDirectoryName(textbox_in.Text)))
                    res &= false;
                else
                    res &= true;
            }
            else if (textbox_in.Name.EndsWith("Packages"))
            {
                if (!Directory.Exists(textbox_in.Text.Trim()))
                {
                    res &= false;
                }
                else
                    res &= true;
            }

            return res;
        }

        public void OpenFolderBrowser(TextBox textBox_in)
        {
            UpdateStatus("Lookup for folder...", "tbInfo");
            UpdateStatus(String.Format("Prepare to browsing path {0}", textBox_in.Text), "tbInfo");

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Path.GetFullPath(textBox_in.Text);

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    textBox_in.Text = fbd.SelectedPath;
            }
        }

        private bool CheckPath(TextBox sender)
        {
            return Validate(sender as TextBox);
        }

        private bool CheckOptionPackagesCount(int localOPCount, int expectedOPCount)
        {
            if (localOPCount != expectedOPCount)
            {
                allOPsFound = false;
                return false;
            }
            else
            {
                allOPsFound = true;
                return true;
            }
        }

        private void CopyProductsFromRemoteFolder(string actProductToCopy)
        {
            actProduct = Path.GetFileName(actProductToCopy);

            CustomFileCopier customFileCopier = new CustomFileCopier(actProductToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actProductToCopy))));
            customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged3;

            Thread customCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
            // customCopyThread.Start();
            ThreadManager.Instance.StartAndWaitOneThread(customCopyThread);

            UpdateStatus(string.Format("\r\nCurrently copying: {0}", actProductToCopy.Substring(actProductToCopy.LastIndexOf("\\") + 1)), "tbInfo");
        }

        private void CustomFileCopier_OnProgressChanged3(double Persentage, ref bool Cancel)
        {
            UpdateStatus(string.Format("Copying {0} file...", actProduct), "lbInfo");
            UpdateStatus(string.Format("Copying progress: {0:0}%", Persentage), "tbInfo");
        }

        private void CopyArtifactsFromRemoteFolder(string actArtifactToCopy)
        {
            ArtifactToCopy = Path.GetFileName(actArtifactToCopy);
            CustomFileCopier customFileCopier = new CustomFileCopier(actArtifactToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actArtifactToCopy))));
            customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged2;

            Thread customArtifactsCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
            //  customArtifactsCopyThread.Start();

            ThreadManager.Instance.StartAndWaitOneThread(customArtifactsCopyThread);

            UpdateStatus(string.Format("\r\nCurrently copying: {0}", actArtifactToCopy.Substring(actArtifactToCopy.LastIndexOf("\\") + 1)), "lbInfoText");
        }

        private void CustomFileCopier_OnProgressChanged2(double Persentage, ref bool Cancel)
        {
            UpdateStatus(string.Format("Copying file: {0}", ArtifactToCopy), "lbInfo");
            Form1.Instance.UpdateTextBox(string.Format("Copying Progress: {0:0}%", Persentage));
        }

        private void CopySWBFromRemoteLocation(string swbLocationRemotePath)
        {
            if (Directory.Exists(swbLocationRemotePath))
            {
                foreach (string swbZipFile in Directory.GetFiles(swbLocationRemotePath, "*.zip"))
                {
                    if (swbZipFile.Contains("SunriseWorkbench"))
                    {
                        swbBuildPath = new FileInfo(swbZipFile);
                    }
                }
                UpdateStatus("Preparing to copy the SWB zip file....", "lbInfoText");
                CustomFileCopier customFileCopier = new CustomFileCopier(SwbBuildPath.FullName, Path.GetFullPath(Path.Combine(destinationDir.FullName, Properties.Settings.Default.SWBZipFileName)));
                customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged;

                Thread customArtifactsCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
                //   customArtifactsCopyThread.Start();

                ThreadManager.Instance.StartAndWaitOneThread(customArtifactsCopyThread);
            }
        }

        private void CustomFileCopier_OnProgressChanged(double Persentage, ref bool Cancel)
        {
            UpdateStatus("Copy SWB zip file to local path", "lbInfo");
            UpdateStatus(String.Format("{1}Copying SWB zip file: {0:0}%", Persentage, Environment.NewLine), "tbInfo");
        }

        private void CopyOptionPackagesFromRemoteDropDownFolder(string buildNumber, string lastBuildPath)
        {
            DirectoryInfo lastBuildDirectory = null;

            lastBuildDirectory = CommandControler.Instance.LookUpForLastBuildDirectory(buildNumber, lastBuildPath);

            Thread copyThread = new Thread(new ThreadStart(() => CopyProcessOfArtifactsAndProducts(lastBuildDirectory)));
            ThreadManager.Instance.StartAndWaitOneThread(copyThread);
        }

        public void PrepareThenCopyResources()
        {
            CreateSWBTestDirectoryHierarchy();

            ReadOutLastBuildNumber(Path.Combine(RemoteDropDownPath.Trim(), Properties.Settings.Default.LastBuildNumberTextFile));

            PreparingToCopyOPsFromRemotePath(RemoteDropDownPath, RemoteDropDownPath.Substring(RemoteDropDownPath.LastIndexOf("\\") + 1));

            return;
        }
    }
}