using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWB_OptionPackageInstaller
{
    public class AwaitableSWBZipFileCopier
    {
        public SWBZipFileDecompressAwaiter GetAwaiter()
        {
            new AwaitableCustomFileCopier(ArtifactHandler.Instance.SWBZipFilePath, ArtifactHandler.Instance.LocalPath.FullName).Copy();
            return new SWBZipFileDecompressAwaiter();
        }
    }

    public class SWBZipFileDecompressAwaiter : INotifyCompletion
    {
        public async Task GetResultAsync()
        {
            using (ZipArchive archive = ZipFile.OpenRead(ArtifactHandler.Instance.SWBZipFilePath))
            {
                Task task = await CreateUnzipTask(archive);
                task.Start();
                task.Wait();
                isCompleted = true;
            }
        }

        public async Task<Task> CreateUnzipTask(ZipArchive archive)
        {
            Task unzipThread = new Task(() =>
             {
                 foreach (ZipArchiveEntry entry in new ZipArchive(new FileStream(ArtifactHandler.Instance.SWBZipFilePath, FileMode.Open, FileAccess.Read), ZipArchiveMode.Read, true).Entries)
                 {
                     Form1.Instance.UpdateStatus(string.Format("Extracting file: {0}", entry.Name));
                     // Gets the full path to ensure that relative segments are removed.
                     string destinationPath = Path.GetFullPath(Path.Combine(ArtifactHandler.Instance.DestinationDir.FullName, entry.Name));
                     entry.ExtractToFile(destinationPath);
                 }
             });
            await unzipThread.ConfigureAwait(true);
            await Task.Delay(10000);
            unzipThread.Wait();
            return unzipThread;
        }

        private bool isCompleted = false;

        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }
}