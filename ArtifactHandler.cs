using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class ArtifactHandler
    {
        public static ArtifactHandler Instance = null;
        private StringCollection _artifacts;
        public List<string> packages = new List<string>();
        public List<string> artifactList = new List<string>();
        private List<string> packagesNames = new List<string>();
        private string[] artifactsArray = new string[13];

        public List<string> PackagesNames
        {
            get { return packagesNames; }
            set { packagesNames = value; }
        }

        public ArtifactHandler()
        {
            Instance = this;
            _artifacts = Properties.Settings.Default.ArtifactsNeededToCopy;
            Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(artifactsArray, 0);
            artifactList.AddRange(artifactsArray);
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