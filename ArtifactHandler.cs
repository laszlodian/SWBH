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

        //private int opCountInFolder;

        //public int OPCountInFolder { get { return opCountInFolder; } set { opCountInFolder = value; } }

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

            Form1.Instance.UpdateTextBox(string.Format("Last build number: {0}", LastBuildNumber));
            Form1.Instance.UpdateStatus(string.Format("Last build number: {0}", LastBuildNumber));

            return LastBuildNumber;
            //      PreparingToCopyOPsFromRemotePath(lastBuildPathFile, LastBuildDirName);
        }

        public void PrepareThenCopyResources()
        {
            //Create the folder architecture on local computer
            Task createTestLibraryHierarchy = new Task(CreateSWBTestDirectoryHierarchy);
            createTestLibraryHierarchy.ConfigureAwait(false);
            createTestLibraryHierarchy.RunSynchronously();

            //Read out the build number from the txt file on the server
            lastBuildFilePath = new DirectoryInfo(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));
            lastBuildNumber = ReadOutLastBuildNumber(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));

            ReadOutSWBPathFromJSON(LastBuildPath);
            //Copy the artifacts and products from the newest build
            CancellationToken cancellationToken = new CancellationToken(true);
            //   CopyEveryFilesFromDirectoryToDestinationDir(productsList, Path.Combine(LastBuildPath.FullName, "Product"), LocalPath.FullName, cancellationToken).Wait();
            //    CopyEveryFilesFromDirectoryToDestinationDir(artifactList, Path.Combine(LastBuildPath.FullName, "Artifacts"), LocalPath.FullName, cancellationToken).Wait();
            foreach (string item in artifactList)
            {
                CustomFileCopier artifactsCopier = new CustomFileCopier(Path.Combine(Path.Combine(LastBuildPath.FullName, "Artifacts"), item), Path.Combine(LocalPath.FullName, item));
                artifactsCopier.OnProgressChanged += ArtifactsCopier_OnProgressChanged;
                artifactsCopier.OnComplete += ArtifactsCopier_OnComplete;
                Thread artifactCopyThread = new Thread(new ThreadStart(() => artifactsCopier.Copy()));
                artifactCopyThread.Start();
                //artifactCopyThread.Join();
            }
            foreach (string item in productsList)
            {
                CustomFileCopier productCopier = new CustomFileCopier(Path.Combine(Path.Combine(LastBuildPath.FullName, "Product"), item), Path.Combine(LocalPath.FullName, item));
                productCopier.OnProgressChanged += ProductCopier_OnProgressChanged;
                productCopier.OnComplete += ProductCopier_OnComplete;
                Thread productCopyThread = new Thread(new ThreadStart(() => productCopier.Copy()));
                productCopyThread.Start();
                //      productCopyThread.Join();
            }
            //  PreparingToCopyOPsFromRemotePath(RemoteDropDownRootPath);
        }

        public int prodPercentageOverall = 0;

        private void ProductCopier_OnProgressChanged(double Persentage, ref bool Cancel)
        {
            Form1.Instance.UpdateStatus(string.Format("Current file copy process percentage: {0}%", Persentage));
            Form1.Instance.UpdateTextBox(string.Format("Overall Percent of artifacts copying process: {0}%", prodPercentageOverall + 3));
        }

        private void ProductCopier_OnComplete()
        {
            Form1.Instance.UpdateStatus("All required option package has been copied to local folder from the Product folder.");
            Form1.Instance.UpdateTextBox("Products successfully copied.");
        }

        private void ArtifactsCopier_OnComplete()
        {
            Form1.Instance.UpdateStatus("All required option package has been copied to local folder from the Artifacts");
            Form1.Instance.UpdateTextBox("Artifacts successfully copied");
        }

        public int artifactPercentageOverall = 0;

        private void ArtifactsCopier_OnProgressChanged(double Persentage, ref bool Cancel)
        {
            Form1.Instance.UpdateStatus(string.Format("Current file copy process percentage: {0}%", Persentage));
            Form1.Instance.UpdateTextBox(string.Format("Overall Percent of artifacts copying process: {0}%", artifactPercentageOverall + 3));
        }

        public static void FileCopy(string source, string destination)
        {
            int array_length = (int)Math.Pow(2, 19);
            byte[] dataArray = new byte[array_length];
            using (FileStream fsread = new FileStream
            (source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, array_length, true))
            {
                using (BinaryReader bwread = new BinaryReader(fsread))
                {
                    using (FileStream fswrite = new FileStream
                    (destination, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, array_length, FileOptions.Asynchronous))
                    {
                        using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                        {
                            for (; ; )
                            {
                                int read = bwread.Read(dataArray, 0, array_length);
                                if (0 == read)
                                    break;
                                bwwrite.Write(dataArray, 0, read);
                            }
                        }
                    }
                }
            }
        }

        //public static async Task AwaitSWBZipFileCopy()
        //{
        //    AwaitableSWBZipFileCopier awaitableSWBZipFileCopier = new AwaitableSWBZipFileCopier();
        //    await awaitableSWBZipFileCopier.GetAwaiter();
        //}

        private void PreparingToCopyOPsFromRemotePath(string navServerPath)
        {
            Form1.Instance.UpdateStatus("Get the path of the lastbuild directory");
            //Get the path of the lastbuild directory
            Task getLastBuildPathTask = new Task<DirectoryInfo>(() => ReadOutLastBuildPath(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile)));
            getLastBuildPathTask.RunSynchronously(TaskScheduler.FromCurrentSynchronizationContext());

            //lastBuildPath =getLastBuildPathTask.// ReadOutLastBuildPath(Path.Combine(RemoteDropDownRootPath, Properties.Settings.Default.LastBuildNumberTextFile));

            Form1.Instance.UpdateStatus("Get the compatible sunriseworkbench path and decompress it to the local folder");
            //Get the compatible sunriseworkbench path and decompress it to the local folder
            Task readOut = new Task(() => ReadOutSWBPathFromJSON(LastBuildPath));
            readOut.ConfigureAwait(true);
            readOut.ContinueWith(x =>
            {
                Task copyProductTask = new Task(() => CopySWBFromRemoteLocation(Path.GetFullPath(Path.Combine(swbPath, "Product"))));
                copyProductTask.ConfigureAwait(true);
                copyProductTask.RunSynchronously();
                x.RunSynchronously();
            }).ContinueWith(y =>
            {
                Task decompressTask = new Task(() => CommandControler.Instance.UnzipSunriseWorkbench(new FileInfo(SWBZipFilePath), new DirectoryInfo(Form1.Instance.PathOfSWB)));
                decompressTask.ConfigureAwait(true);
                decompressTask.RunSynchronously();
                y.RunSynchronously();
            }).ContinueWith(c => { CopyOptionPackagesFromRemoteDropDownFolder(LastBuildPath.FullName); c.RunSynchronously(); });
            //CopySWBZipFileToLocal();
            //DecompressSWBZipFile();

            //Copying option packages
        }

        private async void CopySWBZipFileToLocal()
        {
            Task copyProductTask = new Task(() => CopySWBFromRemoteLocation(Path.GetFullPath(Path.Combine(swbPath, "Product"))));
            await copyProductTask.ConfigureAwait(true);
            copyProductTask.RunSynchronously();
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

        //private void CopyProductsFromLatestBuild(DirectoryInfo lastBuildDirectory)
        //{
        //    string[] productsArray = new string[4];
        //    Properties.Settings.Default.ProductsNeededToCopy.CopyTo(productsArray, 0);
        //    ProductsList.AddRange(productsArray);

        //    Thread cpyProducts = new Thread(new ThreadStart(() =>
        //    {
        //        foreach (string item in Directory.GetFiles(Path.GetFullPath(Path.Combine(lastBuildDirectory.FullName, "Product")), "*.zip"))
        //        {
        //            Form1.Instance.UpdateStatus(string.Format("Processing product: {0}", item));
        //            if (Properties.Settings.Default.ProductsNeededToCopy.Contains(Path.GetFileName(item)))
        //            {
        //                Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));
        //                //Task.Run(() =>
        //                //{
        //                //    productsList.AsParallel().ForAll(file =>
        //                //    {
        //                //        string newFile = Path.Combine(destinationDir.FullName, file);
        //                //        System.IO.File.Copy(file, newFile);
        //                //        collectedOPs.Add(Path.GetFileName(file));
        //                //    });
        //                //});

        //                CopyProductsFromRemoteFolder(item);
        //                // File.Copy(item, String.Format("{0}{1}{2}", Path.GetDirectoryName(destinationDir.FullName), Path.DirectorySeparatorChar, Path.GetFileName(item)), true);//));
        //                // collectedOPs.Add(Path.GetFileName(item));
        //                //UpdateStatus("Copy Product option packages has finished.", "tbInfo");
        //            }
        //        }
        //    }));
        //    ThreadManager.Instance.StartAndWaitOneThread(cpyProducts);
        //}

        /// <summary>
        /// TODO: Try out the other solution:
        /// </summary>
        /// <param name="lastBuildDirectory"></param>
        //private void CopyArtifactsFromLatestBuild(DirectoryInfo lastBuildDirectory)
        //{
        //    string[] artifactsArray = new string[13]; ;
        //    Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(artifactsArray, 0);
        //    artifactList.AddRange(artifactsArray);

        //    Thread cpyArtifacts = new Thread(new ThreadStart(() =>
        //    {
        //        foreach (string item in Directory.GetFiles(String.Format("{0}{1}{2}", lastBuildDirectory.FullName, Path.DirectorySeparatorChar, "Artifacts"), "*.zip"))
        //        {
        //            Form1.Instance.UpdateStatus(string.Format("Processing artifact: {0}", item));
        //            if (Properties.Settings.Default.ArtifactsNeededToCopy.Contains(Path.GetFileName(item)))
        //            {
        //                Form1.Instance.UpdateTextBox(string.Format("Copying file: {0}", Path.GetFileName(item)));

        //                //Task.Run(() =>
        //                //{
        //                //    artifactList.AsParallel().ForAll(file =>
        //                //    {
        //                //        string newFile = Path.Combine(destinationDir.FullName, file);
        //                //        System.IO.File.Copy(file, newFile);
        //                //        collectedOPs.Add(Path.GetFileName(file));
        //                //    });
        //                //});

        //                CopyArtifactsFromRemoteFolder(item);
        //            }
        //        }
        //    }));
        //    ThreadManager.Instance.StartAndWaitOneThread(cpyArtifacts);
        ////}

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

        //private void CopyProductsFromRemoteFolder(string actProductToCopy)
        //{
        //    actProduct = Path.GetFileName(actProductToCopy);

        //    CustomFileCopier customFileCopier = new CustomFileCopier(actProductToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actProductToCopy))));
        //    customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged3;

        //    Thread customCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
        //    // customCopyThread.Start();
        //    ThreadManager.Instance.StartAndWaitOneThread(customCopyThread);

        //    //UpdateStatus(string.Format("\r\nCurrently copying: {0}", actProductToCopy.Substring(actProductToCopy.LastIndexOf("\\") + 1)), "tbInfo");
        //}

        //private void CustomFileCopier_OnProgressChanged3(double Persentage, ref bool Cancel)
        //{
        //    //UpdateStatus(string.Format("Copying {0} file...", actProduct), "lbInfo");
        //    //UpdateStatus(string.Format("Copying progress: {0:0}%", Persentage), "tbInfo");
        //}

        //private void CopyArtifactsFromRemoteFolder(string actArtifactToCopy)
        //{
        //    ArtifactToCopy = Path.GetFileName(actArtifactToCopy);
        //    CustomFileCopier customFileCopier = new CustomFileCopier(actArtifactToCopy, Path.GetFullPath(Path.Combine(destinationDir.FullName, Path.GetFileName(actArtifactToCopy))));
        //    customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged2;

        //    Thread customArtifactsCopyThread = new Thread(new ThreadStart(() => customFileCopier.Copy()));
        //    //  customArtifactsCopyThread.Start();

        //    ThreadManager.Instance.StartAndWaitOneThread(customArtifactsCopyThread);

        //    //UpdateStatus(string.Format("\r\nCurrently copying: {0}", actArtifactToCopy.Substring(actArtifactToCopy.LastIndexOf("\\") + 1)), "lbInfoText");
        //}

        //private void CustomFileCopier_OnProgressChanged2(double Persentage, ref bool Cancel)
        //{
        //    //UpdateStatus(string.Format("Copying file: {0}", ArtifactToCopy), "lbInfo");
        //    Form1.Instance.UpdateTextBox(string.Format("Copying Progress: {0:0}%", Persentage));
        //}

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
                customFileCopier.Copy();
                //  ThreadManager.Instance.StartAndWaitOneThread(new Thread(new ThreadStart(() => customFileCopier.Copy())));

                CommandControler.Instance.UnzipSunriseWorkbench(new FileInfo(Path.Combine(destinationDir.FullName, Properties.Settings.Default.SWBZipFileName)), new DirectoryInfo(Form1.Instance.tbPathOfLocalFolder.Text));
            }
        }

        private void CustomFileCopier_OnProgressChanged(double Persentage, ref bool Cancel)
        {
            Form1.Instance.UpdateStatus(string.Format("Copy {0} zip file to local path", Properties.Settings.Default.SWBZipFileName));
            Form1.Instance.UpdateTextBox(String.Format("Copying SWB zip file: {0:0}%", Persentage));
        }

        public void DecompressZipFile(FileInfo zipFile, DirectoryInfo destDir)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipFile.FullName))
            {
                Thread unzipThread = new Thread(new ThreadStart(() =>
                  {
                      foreach (ZipArchiveEntry entry in archive.Entries)
                      {
                          // Gets the full path to ensure that relative segments are removed.
                          string destinationPath = Path.GetFullPath(Path.Combine(destDir.FullName, entry.FullName));
                          entry.ExtractToFile(destinationPath);
                      }
                  }));
                ThreadManager.Instance.StartAndWaitOneThread(unzipThread);
            }
        }

        private void CopyOptionPackagesFromRemoteDropDownFolder(string buildNumber)
        {
            //  DirectoryInfo lastBuildDirectory = null;

            //Is this needed?
            //lastBuildPath = CommandControler.Instance.LookUpForLastBuildDirectory(buildNumber, RemoteDropDownRootPath);

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
                            Task copyProductTask = new Task(() => CopySWBFromRemoteLocation(Path.GetFullPath(Path.Combine(swbPath, "Product"))));
                            await copyProductTask.ConfigureAwait(true);
                            copyProductTask.RunSynchronously();
                            copyProductTask.Wait();
                            //    ThreadManager.Instance.StartAndWaitOneThread(copyProductThread);
                            sr.Close();
                            //  return swbPath;
                        }
                        readedLine = sr.ReadLine();
                    }
                }
            }
        }

        public void PrepareAndFinalizeRemoteDropDownCopyingOptionPackages()
        {
            PrepareThenCopyResources();
            Form1.Instance.UpdateStatus("Nececcary option packages has been copied from the latest NAV master build");
            Form1.Instance.UpdateTextBox("Process Finished!");

            //TODO: Uncomment and debug these
            //   FillDatagridView();
            //ConfigureCollectedOPsDatagrid(collectedOPs);

            // ThreadManager.Instance.WaitAllThreadToFinishWork();

            //     CommandControler.Instance.CheckIfInstallationNeeded();
        }

        #region Deprecated-Obsolated!!

        /*
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
        }*/

        ///This looks FINE!
        //public void CopyFileByteByByte(string srcFile, string dstFile, int offset, int length, byte[] buffer)
        //{
        //    using (Stream inStream = File.OpenRead(srcFile))
        //    using (Stream outStream = File.OpenWrite(dstFile))
        //    {
        //        inStream.Seek(offset, SeekOrigin.Begin);
        //        int bufferLength = buffer.Length, bytesRead;
        //        while ((length > bufferLength) && (bytesRead = inStream.Read(buffer, 0, bufferLength)) > 0)
        //        {
        //            outStream.Write(buffer, 0, bytesRead);
        //            length -= bytesRead;
        //        }
        //        while (length > 0 && (bytesRead = inStream.Read(buffer, 0, length)) > 0)
        //        {
        //            outStream.Write(buffer, 0, bytesRead);
        //            length -= bytesRead;
        //        }
        //    }
        //}

        /*

        public void DoTasksParallel(DirectoryInfo directoryFrom, DirectoryInfo directoryTo)
        {
            var stuff = directoryFrom.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                    Parallel.ForEach(stuff, p => { CopyEveryFilesFromDirectoryToDestinationDir(directoryFrom.FullName, directoryTo.FullName),new Action<FileInfo>(GetFileInfo)});

            //or this
            //var q = stuff.AsParallel().Where(x => p(x)).Orderby(x => k(x)).Select(x => f(x));
            //foreach (var e in q) a(e);
        }

        private void GetFileInfo(FileInfo obj)
        {
            Dispatcher dp = new Dispatcher(obj);
            var dq = new Microsoft.DispatcherQueue("DQ", dp);

            Port<long> offsetPort = new Port<long>();

            Arbiter.Activate(dq, Arbiter.Receive<long>(true, offsetPort,
                new Handler<long>(Split)));

            FileStream fs = File.Open(file_path, FileMode.Open);
            long size = fs.Length;
            fs.Dispose();

            for (long i = 0; i < size; i += split_size)
            {
                offsetPort.Post(i);
            }
        }

        private static void Split(long offset, FileInfo file_path, int split_size)
        {
            FileStream reader = new FileStream(file_path.FullName, FileMode.Open,
                FileAccess.Read);
            reader.Seek(offset, SeekOrigin.Begin);
            long toRead = 0;
            if (offset + split_size <= reader.Length)
                toRead = split_size;
            else
                toRead = reader.Length - offset;

            byte[] buff = new byte[toRead];
            reader.Read(buff, 0, (int)toRead);
            reader.Dispose();
            File.WriteAllBytes("c:\\out" + offset + ".txt", buff);
        }*/
        /// <summary>
        ///// This needs to investigate
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="destination"></param>
        //public void CopyPackagesFromRemoteParallel(DirectoryInfo source, DirectoryInfo destination)
        //{
        //    var ui = TaskScheduler.FromCurrentSynchronizationContext();
        //    var bb = new BroadcastBlock<FileInfo>(i => i);

        //    var copyToDisk = new ActionBlock<FileInfo>(item =>
        //        item.//Image.Save(item.Path));

        //    bb.LinkTo(copyToDisk);
        //}

        #endregion Deprecated-Obsolated!!

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
                        Form1.Instance.UpdateTextBox(string.Format("Current percentage: {0}%", currentPercentage));
                    }
                }
            }
            await Task.Delay(300000, cancellationToken_in);
        }

        public static async Task CopyEveryFilesFromDirectoryToDestinationDir(string fileNeededToCopy, string sourceDir, string destDir, CancellationToken cancellationToken_in)
        {
            double currentPercentage = 0.0;
            double oneFilePercentageValue = new DirectoryInfo(sourceDir).GetFiles().Length / 100;

            using (FileStream srcStream = File.Open(Path.Combine(sourceDir.Substring(sourceDir.LastIndexOf("\\") + 1), fileNeededToCopy), FileMode.Open))
            {
                using (FileStream destStream = File.Create(destDir + fileNeededToCopy.Substring(fileNeededToCopy.LastIndexOf('\\'))))
                {
                    await srcStream.CopyToAsync(destStream);
                    Form1.Instance.UpdateStatus(string.Format("Copying file: {0}", fileNeededToCopy));
                    currentPercentage += oneFilePercentageValue;
                    Form1.Instance.UpdateTextBox(string.Format("Current percentage: {0}%", currentPercentage));
                }
            }
        }

        public async Task CopyEveryFilesFromDirectoryToDestinationDir(string fileNeededToCopy, string sourceDir, string destDir)
        {
            double currentPercentage = 0.0;
            double oneFilePercentageValue = new FileInfo(fileNeededToCopy).Length / 100;
            using (FileStream srcStream = File.Open(Path.Combine(sourceDir.Substring(sourceDir.LastIndexOf("\\") + 1), fileNeededToCopy), FileMode.Open))
            {
                using (FileStream destStream = File.Create(destDir + fileNeededToCopy.Substring(fileNeededToCopy.LastIndexOf('\\'))))
                {
                    await srcStream.CopyToAsync(destStream);
                    Form1.Instance.UpdateStatus(string.Format("Copying file: {0}", fileNeededToCopy));
                    currentPercentage += oneFilePercentageValue;
                    Form1.Instance.UpdateTextBox(string.Format("Current percentage: {0}%", currentPercentage));
                }
            }
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