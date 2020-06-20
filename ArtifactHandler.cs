using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class ArtifactHandler
    {
        #region Variables

        public List<Dictionary<string, bool>> copiedArtifacts = new List<Dictionary<string, bool>>();
        public static ArtifactHandler Instance = null;
        private StringCollection _artifacts;
        public List<string> packages = new List<string>();
        public List<string> artifactList = new List<string>();
        private List<string> packagesNames = new List<string>();
        private string[] artifactsArray = new string[13];
        public int artifactPercentageOverall = 0;
        private ProgressForm pbForm;
        internal bool startProgresses = false;
        public int prodPercentageOverall = 0;

        #endregion Variables

        #region Properties

        //private byte[] copyBuffer;

        //public byte[] CopyBuffer
        //{
        //    get { return copyBuffer; }
        //    set { copyBuffer = value; }
        //}

        private DirectoryInfo destinationDir;

        public DirectoryInfo DestinationDir
        {
            get { return destinationDir; }
            set { destinationDir = value; }
        }

        //public List<string> PackagesNames
        //{
        //    get { return packagesNames; }
        //    set { packagesNames = value; }
        //}

        private DirectoryInfo logDir;

        private DirectoryInfo lastBuildDir;

        //private string artifactToCopy;

        //public string ArtifactToCopy
        //{
        //    get { return artifactToCopy; }
        //    set { artifactToCopy = value; }
        //}

        //public DirectoryInfo LastBuildDir
        //{
        //    get { return lastBuildDir; }
        //    set { lastBuildDir = value; }
        //}

        private List<string> productsList = new List<string>();

        public List<string> ProductsList
        {
            get { return productsList; }
            set { productsList = value; }
        }

        private int opCountInFolder;

        public int OPCountInFolder { get { return opCountInFolder; } set { opCountInFolder = value; } }

        private List<string> collectedOPs = new List<string>();

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

        public string RemoteDropDownRootPath
        {
            get { return remoteDropDownPath; }
            set { remoteDropDownPath = value; }
        }

        public DirectoryInfo swbDir { get; private set; }

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

        public string ActProduct { get { return actProduct; } set { actProduct = value; } }

        private bool collectionFinished;
        public bool CollectionFinished { get { return collectionFinished; } set { collectionFinished = value; } }

        public string actProduct;//{ get; private set; }

        private StringCollection _products;
        public string[] productsArray = new string[4];

        public string LastBuildNumber { get { return lastBuildNumber; } set { lastBuildNumber = value; } }
        private string lastBuildNumber;

        private DirectoryInfo lastBuildFilePath;

        private DirectoryInfo localPath;

        public DirectoryInfo LocalPath
        {
            get { return localPath; }
            set { localPath = value; }
        }

        public DirectoryInfo LastBuildFilePath { get { return lastBuildFilePath; } set { lastBuildFilePath = value; } }

        public DirectoryInfo LastBuildPath { get { return lastBuildPath; } set { lastBuildPath = value; } }

        #endregion Properties

        //  public void OpenFolderBrowser(TextBox textBox_in);

        public void OpenFolderBrowser(TextBox textBox_in)
        {
            //UpdateStatus("Lookup for folder...", "tbInfo");
            //UpdateStatus(String.Format("Prepare to browsing path {0}", textBox_in.Text), "tbInfo");

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Path.GetFullPath(textBox_in.Text);

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    textBox_in.Text = fbd.SelectedPath;
            }
        }

        public ArtifactHandler()
        {
            Instance = this;

            _artifacts = Properties.Settings.Default.ArtifactsNeededToCopy;
            Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(artifactsArray, 0);
            artifactList.AddRange(artifactsArray);

            _products = Properties.Settings.Default.ProductsNeededToCopy;
            Properties.Settings.Default.ProductsNeededToCopy.CopyTo(productsArray, 0);
            productsList.AddRange(productsArray);

            StartProgressEvent += ArtifactHandler_StartProgressEvent;
        }

        private Action ArtifactHandler_StartProgressEvent()
        {
            StartProgressForm();
            return new Action(StartProgressForm);
        }

        private void StartProgressForm()
        {
            if (ProgressForm.Instance == null)
            {
                pbForm = new ProgressForm();/*)).Start();*/
                pbForm.Invoke(new StartProgressDelegate(StartProgress));
            }
        }

        //public void CheckIfInstallationNeeded()
        //{
        //    DialogResult res = MessageBox.Show("Continue with installation?", "Packages are ready for installation", MessageBoxButtons.YesNo);
        //    if (res == DialogResult.Yes)
        //    {
        //        Form1.Instance.SetInstallationPageValues();
        //    }
        //}

        private void CreateSWBTestDirectoryHierarchy()
        {
            if (Form1.Instance.IsLocalPathConfigured())
            {
                destinationDir = new DirectoryInfo(Form1.Instance.LocalPath);
            }
            else
                destinationDir = new DirectoryInfo(string.Format(@"c:\_SWB\{0}_{1}_{2}\", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day));

            logDir = Directory.CreateDirectory(Path.Combine(DestinationDir.FullName, "logs"));
            swbDir = Directory.CreateDirectory(Path.Combine(DestinationDir.FullName, "SWB"));

            if (logDir.Exists && swbDir.Exists)
            {
                Trace.TraceInformation("Directories succcesfully created");
            }
        }

        public string ReadOutLastBuildNumber(string lastBuildFilePath)
        {
            string line = string.Empty;

            using (FileStream fileStream = new FileStream(lastBuildFilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    line = streamReader.ReadLine();
                    streamReader.Close();
                }

                fileStream.Close();
                lastBuildNumber = line;
            }
            if (string.IsNullOrEmpty(LastBuildNumber))
            {
                //MessageBox.Show("Last Build Number Not Found!", "Couldn't read out last build number", MessageBoxIcon.Error);
                MessageBox.Show("Application is now exit...", "Exit application - No Build number found");

                Application.Exit();
            }

            Form1.Instance.UpdateImportantStatus(string.Format("Last build number: {0}", LastBuildNumber));
            Form1.Instance.UpdateStatus(string.Format("Last build number: {0}", LastBuildNumber));

            return LastBuildNumber;
            //      PreparingToCopyOPsFromRemotePath(lastBuildPathFile, LastBuildDirName);
        }

        public delegate void ProductsCopiedSuccessfullDelegate();

        public event ProductsCopyingOnChangeDelegate ProductsCopyingOnChange;

        public delegate void ProductsCopyingOnChangeDelegate(double percent);

        public event ArtifactsCopyingOnChangeDelegate ArtifactsCopyingOnChange;

        public delegate void ArtifactsCopyingOnChangeDelegate(double percent);

        public event ProductsCopiedSuccessfullDelegate ProductsCopiedSuccessfull;

        public delegate void ArtifactsCopiedSuccessfullDelegate();

        public event ArtifactsCopiedSuccessfullDelegate ArtifactsCopiedSuccessfull;

        public void PrepareThenCopyResources()
        {
            new Thread(new ThreadStart(() => HideMainForm())).Start();
            //Create the folder architecture on local computer
            Task createTestLibraryHierarchy = new Task(CreateSWBTestDirectoryHierarchy);
            createTestLibraryHierarchy.Start();
            //createTestLibraryHierarchy.Wait();

            //Read out the build number from the txt file on the server
            ReadOut();

            Thread thread = new Thread(new ThreadStart(() => ReadOutSWBPathFromJSON(LastBuildPath)));
            //thread.Start();
            ThreadManager.Instance.StartTasks(thread);
            Thread.Sleep(30);
            //Copy the artifacts and products from the newest build
            CancellationToken cancellationToken = new CancellationToken(true);
            while (LastBuildPath.FullName.EndsWith("\\master\\"))
            {
                Form1.Instance.SetLastBuildPath();
                Thread.Sleep(50);
            }
            //   CopyEveryFilesFromDirectoryToDestinationDir(productsList, Path.Combine(LastBuildPath.FullName, "Product"), LocalPath.FullName, cancellationToken).Wait();
            //    CopyEveryFilesFromDirectoryToDestinationDir(artifactList, Path.Combine(LastBuildPath.FullName, "Artifacts"), LocalPath.FullName, cancellationToken).Wait();
            Thread thread2 = new Thread(new ThreadStart(() => CopyArtifacts()));
            thread2.Start();

            Thread thread3 = new Thread(new ThreadStart(() => CopyProducts()));
            thread3.Start();
            thread3.Join();
            Form1.Instance.SetUIStateToWaitingCommands();
            //  PreparingToCopyOPsFromRemotePath(RemoteDropDownRootPath);
        }

        private delegate void HideMainFormDelegate();

        private void HideMainForm()
        {
            if (Form1.Instance.InvokeRequired)
            {
                Form1.Instance.Invoke(new HideMainFormDelegate(HideMainForm));
            }
            else
                Form1.Instance.WindowState = FormWindowState.Minimized;
        }

        public void ReadOut()
        {
            lastBuildFilePath = new DirectoryInfo(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            lastBuildNumber = ReadOutLastBuildNumber(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
        }

        private void CopyProducts()
        {
            foreach (string item in productsList)
            {
                if (Form1.Instance.installationForPackages.Count < 1)
                {
                    CustomFileCopier productCopier = new CustomFileCopier(Path.Combine(Path.Combine(lastBuildPath.FullName, "Product"), item), Path.Combine(LocalPath.FullName, item));
                    productCopier.OnProgressChanged += ProductCopier_OnProgressChanged;
                    //  ProductsCopyingOnChange += ProductHandler_ProductsCopyingEventHandler;
                    //   this.ProductsCopiedSuccessfull += ProductsCopiedSuccessfullEventHandler;
                    Thread productCopyThread = new Thread(new ThreadStart(() => productCopier.Copy()));
                    productCopyThread.Start();
                }
                else
                    if (!Form1.Instance.installationForPackages.Contains(item))
                {
                    continue;
                }
                else
                {
                    CustomFileCopier productCopier = new CustomFileCopier(Path.Combine(Path.Combine(lastBuildPath.FullName, "Product"), item), Path.Combine(LocalPath.FullName, item));
                    productCopier.OnProgressChanged += ProductCopier_OnProgressChanged;
                    //  ProductsCopyingOnChange += ProductHandler_ProductsCopyingEventHandler;
                    //   this.ProductsCopiedSuccessfull += ProductsCopiedSuccessfullEventHandler;
                    Thread productCopyThread = new Thread(new ThreadStart(() => productCopier.Copy()));
                    productCopyThread.Start();
                    //      productCopyThread.Join();
                }
            }
        }

        private void CopyArtifacts()
        {
            foreach (string item in artifactList)
            {
                if (Form1.Instance.installationForPackages.Count < 1)
                {
                    CustomFileCopier artifactsCopier = new CustomFileCopier(Path.Combine(Path.Combine(LastBuildPath.FullName, "Artifacts"), item), Path.Combine(LocalPath.FullName, item));
                    artifactsCopier.OnProgressChanged += ArtifactsCopier_OnProgressChanged;
                    Thread artifactCopyThread = new Thread(new ThreadStart(() => artifactsCopier.Copy()));
                    artifactCopyThread.Start();
                }
                else

                    if (!Form1.Instance.installationForPackages.Contains(item))
                {
                    continue;
                }
                else
                {
                    CustomFileCopier artifactsCopier = new CustomFileCopier(Path.Combine(Path.Combine(LastBuildPath.FullName, "Artifacts"), item), Path.Combine(LocalPath.FullName, item));
                    artifactsCopier.OnProgressChanged += ArtifactsCopier_OnProgressChanged;
                    Thread artifactCopyThread = new Thread(new ThreadStart(() => artifactsCopier.Copy()));
                    artifactCopyThread.Start();
                    //artifactCopyThread.Join();
                }
            }
        }

        public int waitCounterArtifact = 0;

        private void ArtifactsCopier_OnProgressChanged(string fileName, double Persentage)
        {
            if (waitCounterArtifact > 8)
            {
                Form1.Instance.UpdateStatus(string.Format("{1} file copy process percentage: {0:00}%", Persentage, fileName));
                Form1.Instance.UpdateImportantStatus(string.Format("Overall Percent of artifacts copying process: {0:0}%", Persentage));
                ProgressForm.Instance.ArtifactsPercent = Persentage;
                waitCounterArtifact = 0;
            }
            else
                waitCounterArtifact++;
        }

        public int waitCounterProduct = 0;

        private void ProductCopier_OnProgressChanged(string fileName, double Persentage)
        {
            if (waitCounterProduct > 8)
            {
                Form1.Instance.UpdateStatus(string.Format("{1} file copy process percentage: {0:00}%", Persentage, fileName));
                Form1.Instance.UpdateImportantStatus(string.Format("Overall Percent of products copying process: {0:0}%", Persentage));
                ProgressForm.Instance.ProductPercent = Persentage;
                waitCounterProduct = 0;
            }
            else
                waitCounterProduct++;
        }

        public void StartCustomFileCopierUntilTokenCancelled(CustomFileCopier customFileCopier, CancellationToken token, CancellationTokenSource tokenSource)
        {
            Task taskToWaitFor = Task.Run(() =>
             {
                 while (!token.IsCancellationRequested)
                 {
                     tokenSource = customFileCopier.CopyWithReturningCancellationTokenSource();
                     //Execute this when finished:
                     tokenSource.Cancel();
                 }
                 token.ThrowIfCancellationRequested();
             }, token)
            .ContinueWith(t =>
         {
             t.Exception?.Handle(e => true);
             Trace.TraceError("You have canceled the task");
         }, TaskContinuationOptions.OnlyOnCanceled);
            if (tokenSource.IsCancellationRequested)
            {
                return;
            }
            // taskToWaitFor.Wait();
        }

        public DirectoryInfo ReadOutLastBuildPath(string fileNameFullPath)
        {
            string res = string.Empty;
            using (StreamReader fStream = new StreamReader(fileNameFullPath))
            {
                res = fStream.ReadLine();
                fStream.Close();
            }
            lastBuildNumber = res;

            lastBuildPath = new DirectoryInfo(Path.Combine(RemoteDropDownRootPath, LastBuildNumber));

            return LastBuildPath;
        }

        private async Task CopyProcessOfArtifactsAndProductsAsync(DirectoryInfo localPath, DirectoryInfo lastBuildDirectory)
        {
            CancellationToken cancellationToken = new CancellationToken(false);
            //CopyArtifactsFromLatestBuild(lastBuildDirectory);
            string[] artifactsArray = new string[13]; ;
            Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(artifactsArray, 0);
            artifactList.AddRange(artifactsArray);
            await CopyEveryFilesFromDirectoryToDestinationDir(artifactList, Path.Combine(lastBuildDirectory.FullName, "Artifacts"), localPath.FullName, cancellationToken);
            await Task.Delay(4800000);

            cancellationToken = new CancellationToken(false);
            string[] productsArray = new string[4];
            Properties.Settings.Default.ProductsNeededToCopy.CopyTo(productsArray, 0);
            ProductsList.AddRange(productsArray);
            await CopyEveryFilesFromDirectoryToDestinationDir(ProductsList, Path.Combine(lastBuildDirectory.FullName, "Product"), localPath.FullName, cancellationToken);
            await Task.Delay(4800000);
            //  CopyProductsFromLatestBuild(lastBuildDirectory);

            CheckEachOPsIsCopied();
        }

        private void CheckEachOPsIsCopied()
        {
            return;
            foreach (FileInfo fileInfo in destinationDir.GetFiles("*.zip"))
            {
                if (Properties.Settings.Default.ArtifactsNeededToCopy.Contains(fileInfo.Name))
                {
                    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
                    dictionary.Add(fileInfo.Name, true);
                    copiedArtifacts.Add(dictionary);
                }
                if (Properties.Settings.Default.ProductsNeededToCopy.Contains(fileInfo.Name))
                {
                    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
                    dictionary.Add(fileInfo.Name, true);
                    copiedArtifacts.Add(dictionary);
                }
            }
            if (copiedArtifacts.Count != Properties.Settings.Default.ArtifactsNeededToCopy.Count + Properties.Settings.Default.ProductsNeededToCopy.Count)
            {
                Dictionary<string, bool> missingDict = new Dictionary<string, bool>();
                //       MessageBox.Show("Missing option packages!", "Missing artifacts!", MessageBoxIcon.Warning);

                foreach (string item in Properties.Settings.Default.ArtifactsNeededToCopy)
                {
                    foreach (Dictionary<string, bool> copiedOPs in copiedArtifacts)
                    {
                        if (!copiedOPs.ContainsKey(item))
                        {
                            Dictionary<string, bool> dict = new Dictionary<string, bool>();
                            dict.Add(item, false);
                            copiedArtifacts.Add(dict);
                        }
                    }
                }
                foreach (string item in Properties.Settings.Default.ProductsNeededToCopy)
                {
                    foreach (Dictionary<string, bool> copiedOPs in copiedArtifacts)
                    {
                        if (!copiedOPs.ContainsKey(item))
                        {
                            Dictionary<string, bool> dict = new Dictionary<string, bool>();
                            dict.Add(item, false);
                            copiedArtifacts.Add(dict);
                        }
                    }
                }
                foreach (Dictionary<string, bool> copiedOP in copiedArtifacts)
                {
                    List<string> missingOPs = new List<string>();
                    bool res = false;

                    copiedOP.TryGetValue(copiedOP.Keys.First(), out res);
                    if (!res)
                    {
                        missingDict.Add(copiedOP.Keys.First(), res);
                    }
                }
                //    MessageBox.Show(string.Format("Missing option packages: {0}", missingDict.Keys.ItemsToString(";")), "Missing Option Packages!", MessageBoxIcon.Error);
            }
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
                        break;
                    }
                }
                Form1.Instance.UpdateStatus("Preparing to copy the SWB zip file....");
                //await ArtifactHandler.Instance.CopyEveryFilesFromDirectoryToDestinationDir(Properties.Settings.Default.SWBZipFileName, SwbBuildPath.FullName, Path.GetFullPath(Path.Combine(destinationDir.FullName)));

                CustomFileCopier customFileCopier = new CustomFileCopier(SwbBuildPath.FullName, Path.GetFullPath(Path.Combine(destinationDir.FullName, Properties.Settings.Default.SWBZipFileName)));
                customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged;
                CancellationTokenSource tokenSource = new CancellationTokenSource(new TimeSpan(0, 0, 40));
                CancellationToken cancellationToken = new CancellationToken(false);
                StartCustomFileCopierUntilTokenCancelled(customFileCopier, cancellationToken, tokenSource); ;
                //  ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => customFileCopier.Copy())));

                CommandControler.Instance.UnzipSunriseWorkbench(new FileInfo(Path.Combine(destinationDir.FullName, Properties.Settings.Default.SWBZipFileName)), new DirectoryInfo(Form1.Instance.tbPathOfLocalFolder.Text));
            }
        }

        public int waitCounterSWB = 0;

        private void CustomFileCopier_OnProgressChanged(string file, double Persentage)
        {
            if (waitCounterSWB > 8)
            {
                //   Form1.Instance.UpdateStatus(string.Format("Copy {0} zip file to local path", Properties.Settings.Default.SWBZipFileName));
                Form1.Instance.UpdateImportantStatus(String.Format("Copying SWB zip file: {0:0}%", Persentage));
                ProgressForm.Instance.SWBPercent = Persentage;
                waitCounterSWB = 0;
            }
            else
                waitCounterSWB++;
        }

        public void DecompressZipFile(FileInfo zipFile, DirectoryInfo destDir)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipFile.FullName))
            {
                //Thread unzipThread = new Thread(new ThreadStart(() =>
                //  {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Gets the full path to ensure that relative segments are removed.
                    string destinationPath = Path.GetFullPath(Path.Combine(destDir.FullName, entry.FullName));
                    entry.ExtractToFile(destinationPath);
                }
                //}));
                //ThreadManager.Instance.StartAndWaitOneThread(unzipThread);
            }
        }

        private void CopyOptionPackagesFromRemoteDropDownFolder(string buildNumber)
        {
            Thread copyThread = new Thread(new ThreadStart(() => CopyProcessOfArtifactsAndProductsAsync(DestinationDir, LastBuildPath)));
            ThreadManager.Instance.StartAndWaitOneThread(copyThread);
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

        private async void ReadOutSWBPathFromJSON(DirectoryInfo lastBuildDir)
        {
            string readedLine = string.Empty;

            foreach (string item in Directory.GetFiles(lastBuildDir.FullName, "settings_core.json"))
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
                            Form1.Instance.UpdateStatus(string.Format("SWB zip file path: {0}", swbPath));

                            CopySWBFromRemoteLocation(Path.GetFullPath(Path.Combine(swbPath, "Product")));

                            //    ThreadManager.Instance.StartAndWaitOneThread(copyProductThread);
                            sr.Close();
                            //  return swbPath;
                        }
                        readedLine = sr.ReadLine();
                    }
                }
            }
        }

        public event StartProgressDelegate StartProgressEvent;

        public void PrepareAndFinalizeRemoteDropDownCopyingOptionPackages()
        {
            StartProgressEvent();

            new Thread(new ThreadStart(() => PrepareThenCopyResources())).Start();

            Form1.Instance.UpdateStatus("Nececcary option packages has been copied from the latest NAV master build");
            Form1.Instance.UpdateImportantStatus("Process Finished!");

            //TODO: Uncomment and debug these
            CommandControler.Instance.FillDatagridView();
            CommandControler.Instance.ConfigureCollectedOPsDatagrid(collectedOPs);

            //  ThreadManager.Instance.WaitAllThreadToFinishWork();
            Form1.Instance.SetUIStateToWaitingCommands();
            SetStartInstallVisible();
            //CommandControler.Instance.CheckIfInstallationNeeded();
        }

        private delegate void SetStartInstallVisibleDelegate();

        private void SetStartInstallVisible()
        {
            Button bt = Form1.Instance.GetButtonStartWithInstall();
            if (bt.InvokeRequired)
            {
                bt.Invoke(new SetStartInstallVisibleDelegate(SetStartInstallVisible));
            }
            else
                bt.Visible = true;
        }

        ///THIS looks FINE!
        public static async Task CopyEveryFilesFromDirectoryToDestinationDir(List<string> filesNeededToCopy, string sourceDir, string destDir, CancellationToken cancellationToken_in)
        {
            double currentPercentage = 0.0;
            double oneFilePercentageValue = filesNeededToCopy.Count / 100;
            foreach (string fileNeedToCopy in filesNeededToCopy)
            {
                Trace.TraceInformation("File is started to copy: {0}", fileNeedToCopy);
                using (FileStream srcStream = File.Open(Path.Combine(sourceDir, fileNeedToCopy), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (FileStream destStream = File.Create(Path.Combine(destDir, fileNeedToCopy), 8096, FileOptions.Asynchronous))
                    {
                        await srcStream.CopyToAsync(destStream);
                        Form1.Instance.UpdateStatus(string.Format("Copying file: {0}", fileNeedToCopy));
                        currentPercentage += oneFilePercentageValue;
                        Form1.Instance.UpdateImportantStatus(string.Format("Current percentage: {0}%", currentPercentage));
                    }
                }
            }
            await Task.Delay(300000, cancellationToken_in);
        }

        //public static async Task CopyEveryFilesFromDirectoryToDestinationDir(string fileNeededToCopy, string sourceDir, string destDir, CancellationToken cancellationToken_in)
        //{
        //    double currentPercentage = 0.0;
        //    double oneFilePercentageValue = new DirectoryInfo(sourceDir).GetFiles().Length / 100;

        //    using (FileStream srcStream = File.Open(Path.Combine(sourceDir.Substring(sourceDir.LastIndexOf("\\") + 1), fileNeededToCopy), FileMode.Open))
        //    {
        //        using (FileStream destStream = File.Create(destDir + fileNeededToCopy.Substring(fileNeededToCopy.LastIndexOf('\\'))))
        //        {
        //            await srcStream.CopyToAsync(destStream);
        //            Form1.Instance.UpdateStatus(string.Format("Copying file: {0}", fileNeededToCopy));
        //            currentPercentage += oneFilePercentageValue;
        //            Form1.Instance.UpdateImportantStatus(string.Format("Current percentage: {0}%", currentPercentage));
        //        }
        //    }
        //}

        //public async Task CopyEveryFilesFromDirectoryToDestinationDir(string fileNeededToCopy, string sourceDir, string destDir)
        //{
        //    double currentPercentage = 0.0;
        //    double oneFilePercentageValue = new FileInfo(fileNeededToCopy).Length / 100;
        //    using (FileStream srcStream = File.Open(Path.Combine(sourceDir.Substring(sourceDir.LastIndexOf("\\") + 1), fileNeededToCopy), FileMode.Open))
        //    {
        //        using (FileStream destStream = File.Create(destDir + fileNeededToCopy.Substring(fileNeededToCopy.LastIndexOf('\\'))))
        //        {
        //            await srcStream.CopyToAsync(destStream);
        //            Form1.Instance.UpdateStatus(string.Format("Copying file: {0}", fileNeededToCopy));
        //            currentPercentage += oneFilePercentageValue;
        //            Form1.Instance.UpdateImportantStatus(string.Format("Current percentage: {0}%", currentPercentage));
        //        }
        //    }
        //}

        //private static void ExpandDataGridWithRows(DataGridView dgv, FileInfo package)
        //{
        //    DataGridViewCheckBoxCell packageInstall = new DataGridViewCheckBoxCell();
        //    dgv.Rows.Add();
        //    dgv.Rows.Add(new object[] { package.Name, "version", packageInstall });
        //    dgv.Refresh();
        //    dgv.Update();
        //}

        public delegate Action StartProgressDelegate();

        public Action StartProgress()
        {
            if (pbForm != null)
            {
                if (pbForm.InvokeRequired)
                {
                    pbForm.Invoke(new StartProgressDelegate(StartProgress));
                }
                else
                    startProgresses = true;

                return new Action(pbForm.StartPB);
            }
            else
            {
                pbForm = new ProgressForm();
                StartProgress();
            }
            return new Action(pbForm.StartPB);
        }
    }
}