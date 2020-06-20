using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace SWB_OptionPackageInstaller
{
    public class CustomFileCopier
    {
        public CancellationTokenSource cancellationToken = new CancellationTokenSource();

        public delegate void ProgressChangeDelegate(string fileName, double Persentage);

        public delegate void Completedelegate(ref bool Cancel);

        public CustomFileCopier(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;

            cancellationToken = new CancellationTokenSource();

            OnProgressChanged += new ProgressChangeDelegate(OnProgressChangedHandler);
            OnComplete += new Completedelegate(OnCompleteHandler);
        }

        private void OnProgressChangedHandler(string fileName, double Persentage)
        {
            Form1.Instance.UpdateStatus(string.Format("Copying {1} file process: {0:00}%", Persentage, fileName));
            Form1.Instance.UpdateTextBox(string.Format("Copying overall process: {0:00}%", Persentage));
        }

        private void OnCompleteHandler(ref bool Cancel)
        {
            Cancel = true;
            Form1.Instance.UpdateStatus("Files has been copied succesfull.");
            Form1.Instance.UpdateTextBox("Copying overall process: 100%");
            cancellationToken.Cancel();
        }

        public void Copy()
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;

            if (Directory.Exists(DestFilePath))
            {
                DestFilePath = string.Format("{0}_{1}", DestFilePath, System.DateTime.Now.Millisecond);// Contains
                Form1.Instance.UpdateStatus("Destination directory is already exist so create a new folder to install swb...");
            }
            using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                if (!File.Exists(DestFilePath))
                {
                    Directory.CreateDirectory(DestFilePath.Substring(DestFilePath.LastIndexOf("\\") + 1));
                }
                using (FileStream dest = new FileStream(DestFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);

                        cancelFlag = false;
                        OnProgressChanged(dest.Name, persentage);

                        if (cancelFlag == true)
                        {
                            // Delete dest file here
                            break;
                        }
                    }
                }
            }

            OnComplete(ref cancelFlag);
        }

        public CancellationTokenSource CopyWithReturningCancellationTokenSource()
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;

            if (Directory.Exists(DestFilePath))
            {
                DestFilePath = string.Format("{0}_{1}", DestFilePath, System.DateTime.Now.Millisecond);// Contains
                Form1.Instance.UpdateStatus("Destination directory is already exist so create a new folder to install swb...");
            }
            using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;

                using (FileStream dest = new FileStream(DestFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);
                        //test:
                        //  dest.WriteAsync(buffer, 0, currentBlockSize);
                        cancelFlag = false;
                        OnProgressChanged(dest.Name, persentage);

                        if (cancelFlag == true)
                        {
                            // Delete dest file here
                            break;
                        }
                    }
                }
            }

            OnComplete(ref cancelFlag);

            return cancellationToken;
        }

        #region Properties

        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }
        public object DataTime { get; private set; }

        #endregion Properties

        public event ProgressChangeDelegate OnProgressChanged;

        public event Completedelegate OnComplete;
    }
}