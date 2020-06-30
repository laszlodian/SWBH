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

        /*
                private DirectoryInfo lastBuildDir;

                public DirectoryInfo LastBuildDir
                {
                    get { return lastBuildDir; }
                    set { lastBuildDir = value; }
                }

                private List<string> productsList = new List<string>();

                public List<string> ProductsList
                {
                    get { return productsList; }
                    set { productsList = value; }
                }*/

        private int opCountInFolder;

        public int OPCountInFolder { get { return opCountInFolder; } set { opCountInFolder = value; } }
        /*
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
*/
        private string remoteDropDownPath = @"\\KUKA.int.kuka.com\s\KROS_Pool\Daily\NavigationSolution\master\";

        public string RemoteDropDownRootPath
        {
            get { return remoteDropDownPath; }
            set { remoteDropDownPath = value; }
        }

        /*
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
                        }*/

        private string sunriseWorkbenchVersion = string.Empty;

        public string SunriseWorkbenchVersion
        {
            get { return sunriseWorkbenchVersion; }
            set { sunriseWorkbenchVersion = value; }
        }

        private string sWBZipFilePath;

        public string SWBZipFilePath { get { return sWBZipFilePath; } set { sWBZipFilePath = value; } }

        //     private Thread th;
        private string optionPackageList;

        public string OptionPackageList
        {
            get { return optionPackageList; }
            set
            {
                optionPackageList = value;
            }
        }

        //       private bool allOPsFound;

        /*       public bool AllOPsFound
               {
                   get { return allOPsFound; }
                   set { allOPsFound = value; }
               }*/

        //      private string swbPath;

        //    public string SwbPath { get { return swbPath; } set { swbPath = value; } }

        //       private DirectoryInfo lastBuildPath;

        //       public DirectoryInfo LastBuildPath { get { return lastBuildPath; } set { lastBuildPath = value; } }

        //public string ActProduct { get { return actProduct; } set { actProduct = value; } }

        //      private bool collectionFinished;
        //       public bool CollectionFinished { get { return collectionFinished; } set { collectionFinished = value; } }

        //       private List<string> artifactList = new List<string>();
        //     public List<string> ArtifactList { get { return artifactList; } set { artifactList = value; } }

        //    public string actProduct;//{ get; private set; }

        #endregion Properties

        #region Variables

        public DataGridView dgv = new DataGridView();

        //  private BindingSource bindingSourceForCollectedPackages = new BindingSource();
        //   private BindingSource bindingSourceForIstalledPackages = new BindingSource();
        private List<FileInfo> opsInFolder = new List<FileInfo>();

        //      private bool swbFoundInfolder;
        //      private BackgroundWorker bgWorker;
        private int progress = 0;

        //    private List<string> listOfFeatures = new List<string>();
        private List<string> packagesNames = new List<string>();

        public List<string> features = new List<string>();
        public List<string> versions = new List<string>();
        public int PackagesCount = 0;
        //        private string ArtifactToCopy;

        #endregion Variables

        public CommandControler()
        {
            Instance = this;
            Trace.TraceInformation("CommandControler constructor");

            RemoteDropDownRootPath = @"\\KUKA.int.kuka.com\s\KROS_Pool\Daily\NavigationSolution\master\";
        }

        private Thread unzipSWBThread;
        //       private string lastBuildDirName;
        //       public string LastBuildDirName { get { return lastBuildDirName; } set { lastBuildDirName = value; } }

        private delegate void unzipSWBThreadStarting(Action unzippingSWB);

        public void CollectOptionPackages(string pathIn)
        {
            bool isSWBfound = false;

            DirectoryInfo optionPackagesPath = new DirectoryInfo(pathIn);

            foreach (string package in Directory.GetFiles(pathIn, "*.zip"))
            {
                Form1.Instance.UpdateImportantStatus(String.Format("Found feature: {0}", package));

                if (package.Contains("SunriseWorkbench"))
                {
                    Form1.Instance.UpdateStatus("Decompressing SunriseWorkbench....");
                    SWBZipFilePath = package;

                    UnzipSunriseWorkbench();
                    //Thread thread = CreateTheUnzippingThread();
                    //thread.Priority = ThreadPriority.Highest;
                    //ThreadManager.Instance.StartAndWaitOneThread(thread);
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
            Form1.Instance.UpdateImportantStatus(String.Format("Found package count: {0}", packages.Count));

            OptionPackageList = FormatPackageNames(packages);
        }

        /*
        private Thread CreateTheUnzippingThread()
        {
            unzipSWBThread = new Thread(new ThreadStart(UnzipSunriseWorkbench));
            unzipSWBThread.Name = "UnzipSunriseWorkbench";
            unzipSWBThread.SetApartmentState(ApartmentState.MTA);

            return unzipSWBThread;
        }
   private async void NewMethod(CastingPipelineWithAwait<IAwaitablePipeline<Task>> pipeline)
           {
               await pipeline.Execute(StartingUnzipThreadWithPipeline);
           }

           public async Task StartingUnzipThreadWithPipeline(Thread unzipSWBThread)
           {
               unzipSWBThread.Start();
           }*/

        public void UnzipSunriseWorkbench(FileInfo zipFile, DirectoryInfo destPath)
        {
            //if (Directory.Exists(Form1.Instance.PathOfSWB))
            //{
            //    foreach (string item in Directory.GetFiles(Form1.Instance.PathOfSWB))
            //    {
            //        File.Delete(item);
            //    }
            //}
            //else
            //    Directory.CreateDirectory(Form1.Instance.PathOfSWB);

            new Thread(new ThreadStart(() => SetupTimer())).Start();

            ArtifactHandler.Instance.DecompressZipFile(zipFile, destPath);

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

        public void UnzipSunriseWorkbench()
        {
            SetupTimer();
            Stopwatch s = new Stopwatch();
            s.Start();

            if (!Directory.Exists(Form1.Instance.TbSWBText))
            {
                Directory.CreateDirectory(Form1.Instance.TbSWBText);
            }
            foreach (FileInfo item in new DirectoryInfo(Form1.Instance.TbSWBText).GetFiles("*.*"))
            {
                File.Delete(item.FullName);
            }
            Directory.Delete(Form1.Instance.TbSWBText, true);
            ZipFile.ExtractToDirectory(SWBZipFilePath, Form1.Instance.TbSWBText);

            Trace.TraceInformation("Unzipping takes {0} seconds", s.Elapsed.TotalSeconds);
            Form1.Instance.UpdateStatus(string.Format("unzip takes {0} secs", s.Elapsed.TotalSeconds));
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
            Form1.Instance.UpdateImportantStatus(String.Format("Decompress of SWB finished!"));
            (sender as Timer).Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progress++;
            Form1.Instance.UpdateImportantStatus(String.Format("Decompress SWB: {0}%", progress));
            if (progress >= 100)
            {
                (sender as Timer).Dispose();
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
            Stopwatch stopwatch = new Stopwatch();
            List<string> featuresList = GetFeatures(featuresListResult);
            string featuresCommandPart = BuildFeaturesStringForCommand(featuresList);
            Trace.TraceInformation("Features part of command: {0}", featuresCommandPart);
            Trace.TraceInformation(string.Format("listing of features takes: {0} secs", stopwatch.Elapsed.TotalSeconds));
            ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => IterateOverFeatures(features))));

            commandToRun = string.Format(installCommandFormat, OptionPackageList, Form1.Instance.PathOfSWB, featuresCommandPart);
            Trace.TraceInformation("Command to run for install packages: {0}", commandToRun);
            Run(commandToRun);

            Form1.Instance.UpdateStatus("Storing versions, date and time, username, pc-name to database...");

            if (Properties.Settings.Default.StoreToDB)
            {
                SQLManager.Instance.StoreInstall();
            }
            Form1.Instance.SetButtonsVisible();
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
                    Form1.Instance.UpdateImportantStatus(string.Format("Collected Option Package: {0}", item));
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
                        Form1.Instance.UpdateImportantStatus(string.Format("{0} feature version: {1}", version.Split('=')[0].Trim(), version.Split('=')[1].Trim()));
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

                Form1.Instance.UpdateStatus("The application cleaning up the allocated resources and cleaning up the temporary files....");

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
                Form1.Instance.UpdateImportantStatus(string.Format("Running command: {0}", "Install Features"));
            else if (cmd.Contains("list"))
                Form1.Instance.UpdateImportantStatus(string.Format("Running command: {0}", "Get All Features"));

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

        /*public DirectoryInfo LookUpForLastBuildDirectory(string buildNumber, string lastBuildPath)

            private DirectoryInfo sourceFolderOfRemoteBuild = null;

        Form1.Instance.UpdateStatus("Connected to remote dropdown folder, initializing copy process");

            foreach (string actDirectory in (Directory.GetDirectories(lastBuildPath)))
            {
                if (actDirectory.Contains(buildNumber))
                {
                    sourceFolderOfRemoteBuild = private new DirectoryInfo(actDirectory);

                    break;
                }

Form1.Instance.UpdateStatus(String.Format("Last build directory:", sourceFolderOfRemoteBuild));

            if (sourceFolderOfRemoteBuild == null)
                throw new Exception("lastBuildDirectory couldn't found!");

            return sourceFolderOfRemoteBuild;
        }
        */

        public void CheckPackagesInFolder(string pathOfOptionPackages)
        {
            int opCount = 0;
            Form1.Instance.BindingSourceForFoundPackages = new BindingSource();
            foreach (FileInfo opFile in new DirectoryInfo(pathOfOptionPackages).GetFiles("*.zip", SearchOption.TopDirectoryOnly))
            {
                opsInFolder.Add(opFile);
                opCount++;
                Form1.Instance.BindingSourceForFoundPackages.Add(new PackageGridModel(opFile.Name, opCount, true));
            }
        }

        public void CheckPackagesInFolder(string pathOfOptionPackages, out BindingSource bindingSource_in)
        {
            int opCount = 0;
            bindingSource_in = new BindingSource();

            foreach (FileInfo opFile in new DirectoryInfo(Path.Combine(pathOfOptionPackages, "Product")).GetFiles("*.zip", SearchOption.TopDirectoryOnly))
            {
                if (opFile.Name.Contains("KUKA.NavSolution"))
                {
                    opsInFolder.Add(opFile);
                    opCount++;
                    bindingSource_in.Add(new PackageGridModel(opFile.Name, opCount, true));
                }
            }
            foreach (FileInfo opFile in new DirectoryInfo(Path.Combine(pathOfOptionPackages, "Artifacts")).GetFiles("*.zip", SearchOption.TopDirectoryOnly))
            {
                if (opFile.Name.Contains(".swb."))
                {
                    opsInFolder.Add(opFile);
                    opCount++;
                    bindingSource_in.Add(new PackageGridModel(opFile.Name, opCount, true));
                }
            }
        }

        public List<string> CollectAndShowAvailableBuildDirectories(DirectoryInfo lastBuildDirectory)
        {
            string masterDirectory = lastBuildDirectory.FullName.Substring(0, lastBuildDirectory.FullName.LastIndexOf('\\') + 1);

            foreach (string buildDir in Directory.GetDirectories(masterDirectory))
            {
                if (buildDirectoryRegex.IsMatch(buildDir.Substring(buildDir.LastIndexOf("\\") + 1)))
                {
                    allBuildDirectoryUnderMasterFolder.Add(buildDir.Substring(buildDir.LastIndexOf("\\") + 1));
                }
            }
            return allBuildDirectoryUnderMasterFolder;
        }

        public void FillDatagridView()
        {
            Form1.Instance.UpdateStatus("Building DataGrid for reprezenting features...");
            Form1.Instance.PrepareDataGridView(Form1.Instance.GetDGVForCollectedOPs());

            Form1.Instance.EnumsAndComboBox_Load_For_CollectedOPs();
        }

        public void ShowImportantMessageDialog(string textToShow, string captionOfDialog = "Important Information")
        {
            MessageBox.Show(String.Format("{0}", textToShow), captionOfDialog, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult ShowQuestionDialog(string textToShow, string captionOfDialog = "Question - User answer needed")
        {
            return MessageBox.Show(String.Format("{0}", textToShow), captionOfDialog, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void ConfigureCollectedOPsDatagrid(List<string> collectedOPs)
        {
            Form1.Instance.UpdateStatus("Configuring DataGrid");
            string clearPackageName = string.Empty;
            int i = 0;

            foreach (string pkg in collectedOPs)
            {
                Form1.Instance.UpdateStatus(string.Format("Add to datagrid: {0}", pkg));
                Form1.Instance.bindingSourceForCollectedPackages.Add(new PackageGridModel(pkg, ArtifactHandler.Instance.LastBuildPath.Name, true));
                i++;
            }

            Form1.Instance.dgv_collectedOPs.Font = new Font(dgv.Font, FontStyle.Regular);
            Form1.Instance.EnumsAndComboBox_Load_For_CollectedOPs();
        }
    }
}