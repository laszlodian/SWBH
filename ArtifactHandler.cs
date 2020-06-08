using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        #endregion Variables

        #region Properties

        private DirectoryInfo destinationDir;

        public DirectoryInfo DestinationDir
        {
            get { return destinationDir; }
            set { destinationDir = value; }
        }

        public List<string> PackagesNames
        {
            get { return packagesNames; }
            set { packagesNames = value; }
        }

        private DirectoryInfo logDir;

        private DirectoryInfo lastBuildDir;

        private string artifactToCopy;

        public string ArtifactToCopy
        {
            get { return artifactToCopy; }
            set { artifactToCopy = value; }
        }

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
        }

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

        private string lastBuildDirName;

        public string LastBuildDirName
        {
            get { return lastBuildDirName; }
            set { lastBuildDirName = value; }
        }

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
            if (Form1.Instance.IsLocalPathConfigured())
            {
                destinationDir = new DirectoryInfo(Form1.Instance.LocalPath);
            }
            else
                destinationDir = new DirectoryInfo(string.Format(@"c:\_SWB\{0}_{1}_{2}\", DateTime.Today.Date.Year, DateTime.Today.Date.Month, DateTime.Today.Date.Day));

            logDir = Directory.CreateDirectory(Path.Combine(destinationDir.FullName, "logs"));
            swbDir = Directory.CreateDirectory(Path.Combine(destinationDir.FullName, "SWB"));

            if (logDir.Exists && swbDir.Exists)
            {
                Trace.TraceInformation("Directories succcesfully created");
            }
        }

        private void ReadOutLastBuildNumber(string lastBuildPathFile)
        {
            using (FileStream fileStream = new FileStream(Path.GetFullPath(lastBuildPathFile.Replace("\\", "/")), FileMode.Open, FileAccess.Read))
            {
                string line = string.Empty;

                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    line = streamReader.ReadLine();
                    streamReader.Close();
                }
                fileStream.Close();
                lastBuildDirName = line;
            }

            if (string.IsNullOrEmpty(LastBuildDirName))
            {
                //MessageBox.Show("Last Build Number Not Found!", "Couldn't read out last build number", MessageBoxIcon.Error);
                MessageBox.Show("Application is now exit...", "Exit application - No Build number found");

                Application.Exit();
            }

            Form1.Instance.UpdateTextBox(string.Format("Last build number: {0}", LastBuildDirName));
            Form1.Instance.UpdateStatus(string.Format("Last build number: {0}", LastBuildDirName));

            //      PreparingToCopyOPsFromRemotePath(lastBuildPathFile, LastBuildDirName);
        }

        public void PrepareThenCopyResources()
        {
            CreateSWBTestDirectoryHierarchy();

            ReadOutLastBuildNumber(Path.Combine(RemoteDropDownRootPath.Trim(), Properties.Settings.Default.LastBuildNumberTextFile));

            PreparingToCopyOPsFromRemotePath(RemoteDropDownRootPath, RemoteDropDownRootPath.Substring(RemoteDropDownRootPath.LastIndexOf("\\") + 1));

            return;
        }

        private void PreparingToCopyOPsFromRemotePath(string lastBuildPathFile, string lastBuildDirectoryName)
        {
            //     UpdateStatus("Application starts to copy the nececcary option packages, please be patient..", "lbInfoText");
            //     ShowImportantMessageDialog("Application starts to copy the nececcary option packages, please be patient..");

            lastBuildPath = GetLastBuildDirectory(lastBuildDirectoryName, lastBuildPathFile);

            ReadOutSWBPathFromJSON(lastBuildPath);

            CopyOptionPackagesFromRemoteDropDownFolder(LastBuildPath.FullName.Substring(LastBuildPath.FullName.LastIndexOf("\\") + 1), LastBuildPath.FullName);
        }

        private DirectoryInfo GetLastBuildDirectory(string buildNumber, string lastBuildPathDir)
        {
            foreach (string actDirectory in Directory.GetDirectories(lastBuildPathDir.Substring(0, lastBuildPathDir.LastIndexOf("\\"))))
            {
                if (actDirectory.Contains(buildNumber))
                {
                    lastBuildDir = new DirectoryInfo(actDirectory);
                    break;
                }
            }

            if (lastBuildDir == null)
            {
                MessageBox.Show("Last Build Directory not found!");
                Application.Exit();
            }

            return lastBuildDir;
        }

        private void CopyProcessOfArtifactsAndProducts(DirectoryInfo localPath, DirectoryInfo lastBuildDirectory)
        {
            string[] artifactsArray = new string[13]; ;
            Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(artifactsArray, 0);
            artifactList.AddRange(artifactsArray);

            Thread cpyArtifacts = new Thread(new ThreadStart(() =>
            {
                foreach (string item in Directory.GetFiles(String.Format("{0}{1}{2}", lastBuildDirectory.FullName, Path.DirectorySeparatorChar, "Artifacts"), "*.zip"))
                {
                    Form1.Instance.UpdateStatus(string.Format("Processing artifact: {0}", item));
                    if (Properties.Settings.Default.ArtifactsNeededToCopy.Contains(Path.GetFileName(item)))
                    {
                        Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));

                        //Task.Run(() =>
                        //{
                        //    artifactList.AsParallel().ForAll(file =>
                        //    {
                        //        string newFile = Path.Combine(destinationDir.FullName, file);
                        //        System.IO.File.Copy(file, newFile);
                        //        collectedOPs.Add(Path.GetFileName(file));
                        //    });
                        //});

                        CopyArtifactsFromRemoteFolder(item);
                    }
                }
            }));
            ThreadManager.Instance.StartAndWaitOneThread(cpyArtifacts);

            string[] productsArray = new string[4];
            Properties.Settings.Default.ProductsNeededToCopy.CopyTo(productsArray, 0);
            ProductsList.AddRange(productsArray);

            Thread cpyProducts = new Thread(new ThreadStart(() =>
            {
                foreach (string item in Directory.GetFiles(Path.GetFullPath(Path.Combine(lastBuildDirectory.FullName, "Product")), "*.zip"))
                {
                    Form1.Instance.UpdateStatus(string.Format("Processing product: {0}", item));
                    if (Properties.Settings.Default.ProductsNeededToCopy.Contains(Path.GetFileName(item)))
                    {
                        Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));
                        //Task.Run(() =>
                        //{
                        //    productsList.AsParallel().ForAll(file =>
                        //    {
                        //        string newFile = Path.Combine(destinationDir.FullName, file);
                        //        System.IO.File.Copy(file, newFile);
                        //        collectedOPs.Add(Path.GetFileName(file));
                        //    });
                        //});

                        CopyProductsFromRemoteFolder(item);
                        // File.Copy(item, String.Format("{0}{1}{2}", Path.GetDirectoryName(destinationDir.FullName), Path.DirectorySeparatorChar, Path.GetFileName(item)), true);//));
                        // collectedOPs.Add(Path.GetFileName(item));
                        //UpdateStatus("Copy Product option packages has finished.", "tbInfo");
                    }
                }
            }));
            ThreadManager.Instance.StartAndWaitOneThread(cpyProducts);

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

        private void CopyProductsFromRemoteFolder(string actProductToCopy)
        {
            actProduct = Path.GetFileName(actProductToCopy);

            CustomFileCopier customFileCopier = new CustomFileCopier(actProductToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actProductToCopy))));
            customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged3;

            Thread customCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
            // customCopyThread.Start();
            ThreadManager.Instance.StartAndWaitOneThread(customCopyThread);

            //UpdateStatus(string.Format("\r\nCurrently copying: {0}", actProductToCopy.Substring(actProductToCopy.LastIndexOf("\\") + 1)), "tbInfo");
        }

        private void CustomFileCopier_OnProgressChanged3(double Persentage, ref bool Cancel)
        {
            //UpdateStatus(string.Format("Copying {0} file...", actProduct), "lbInfo");
            //UpdateStatus(string.Format("Copying progress: {0:0}%", Persentage), "tbInfo");
        }

        private void CopyArtifactsFromRemoteFolder(string actArtifactToCopy)
        {
            ArtifactToCopy = Path.GetFileName(actArtifactToCopy);
            CustomFileCopier customFileCopier = new CustomFileCopier(actArtifactToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actArtifactToCopy))));
            customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged2;

            Thread customArtifactsCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
            //  customArtifactsCopyThread.Start();

            ThreadManager.Instance.StartAndWaitOneThread(customArtifactsCopyThread);

            //UpdateStatus(string.Format("\r\nCurrently copying: {0}", actArtifactToCopy.Substring(actArtifactToCopy.LastIndexOf("\\") + 1)), "lbInfoText");
        }

        private void CustomFileCopier_OnProgressChanged2(double Persentage, ref bool Cancel)
        {
            //UpdateStatus(string.Format("Copying file: {0}", ArtifactToCopy), "lbInfo");
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
                ///    UpdateStatus("Preparing to copy the SWB zip file....", "lbInfoText");
                CustomFileCopier customFileCopier = new CustomFileCopier(SwbBuildPath.FullName, Path.GetFullPath(Path.Combine(destinationDir.FullName, Properties.Settings.Default.SWBZipFileName)));
                customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged;

                Thread customArtifactsCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));

                ThreadManager.Instance.StartAndWaitOneThread(customArtifactsCopyThread);
            }
        }

        private void CustomFileCopier_OnProgressChanged(double Persentage, ref bool Cancel)
        {
            //UpdateStatus("Copy SWB zip file to local path", "lbInfo");
            //UpdateStatus(String.Format("{1}Copying SWB zip file: {0:0}%", Persentage, Environment.NewLine), "tbInfo");
        }

        private void CopyOptionPackagesFromRemoteDropDownFolder(string buildNumber, string lastBuildPath)
        {
            DirectoryInfo lastBuildDirectory = null;

            //lastBuildDirectory = CommandControler.Instance.LookUpForLastBuildDirectory(buildNumber, lastBuildPath);

            Thread copyThread = new Thread(new ThreadStart(() => CopyProcessOfArtifactsAndProducts(DestinationDir, LastBuildPath)));
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
                                sr.Close();
                                break;
                            }
                            readedLine = sr.ReadLine();
                        }
                    }
                }
            }
        }

        public void PrepareAndFinalizeRemoteDropDownCopyingOptionPackages()
        {
            PrepareThenCopyResources();
            Form1.Instance.UpdateStatus("Nececcary option packages has been copied from the latest NAV master build");

            //   FillDatagridView();
            //ConfigureCollectedOPsDatagrid(collectedOPs);

            // ThreadManager.Instance.WaitAllThreadToFinishWork();

            //     CommandControler.Instance.CheckIfInstallationNeeded();
        }

        public void CopyOptionPackagesFromRemote()
        {
            Task.Run(() =>
            {
                artifactList.AsParallel().ForAll(file =>
                  {
                      string newFile = Path.Combine("C:/", file);
                      System.IO.File.Copy(file, newFile);
                      // Do more
                  });
            });
        }

        private async Task<Stream> GetStreamAsync()
        {
            try
            {
                return new FileStream("sample.mp3", FileMode.Open, FileAccess.Write);
            }
            catch (IOException)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                return await GetStreamAsync();
            }
        }

        public void DoTasksParallel(DirectoryInfo directoryFrom, DirectoryInfo directoryTo)
        {
            var stuff = directoryFrom.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            //        Parallel.ForEach(stuff, p => { CopyEveryFilesFromDirectoryToDestinationDir(directoryFrom.FullName, directoryTo.FullName),new Action<FileInfo>(GetFileInfo)});

            //or this
            //var q = stuff.AsParallel().Where(x => p(x)).Orderby(x => k(x)).Select(x => f(x));
            //foreach (var e in q) a(e);
        }

        private void GetFileInfo(FileInfo obj)
        {
            throw new NotImplementedException();
        }

        private async void CopyEveryFilesFromDirectoryToDestinationDir(string sourceDir, string destDir)
        {
            foreach (string filename in Directory.EnumerateFiles(sourceDir))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(destDir + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        await SourceStream.CopyToAsync(DestinationStream);
                    }
                }
            }
        }

        public void CopyPackagesFromRemoteParallel(DirectoryInfo source, DirectoryInfo destination)
        {
            List<string> fileList = new List<string>();
            //HashSet<string> filesToBeCopied = new HashSet<string>(fileList.AsEnumerable<string>().AsParallel().ForAll(d => DoTasksParallel(d, destination)), IEqualityComparer.Default

            // you'll probably have to play with MaxDegreeOfParallellism so as to avoid swamping the i/o system
            //    ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 4 };

            //Parallel.ForEach(filesToBeCopied.SelectMany(fn => source.EnumerateFiles(fn)), options, fi =>
            // {
            //     string destinationPath = Path.Combine(destination.FullName, Path.ChangeExtension(fi.Name, ".jpg"));
            //     fi.CopyTo(destinationPath, false);
            // });
        }

        private static void ExpandDataGridWithRows(DataGridView dgv, FileInfo package)
        {
            DataGridViewCheckBoxCell packageInstall = new DataGridViewCheckBoxCell();
            dgv.Rows.Add();
            dgv.Rows.Add(new object[] { package.Name, "version", packageInstall });
            dgv.Refresh();
            dgv.Update();
        }
    }
}