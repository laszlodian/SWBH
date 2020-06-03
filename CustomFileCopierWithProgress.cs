using System.IO;

namespace SWB_OptionPackageInstaller
{
    public class CustomFileCopier
    {
        public delegate void ProgressChangeDelegate(double Persentage, ref bool Cancel);

        public delegate void Completedelegate();

        public CustomFileCopier(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;

            OnProgressChanged += delegate { };
            OnComplete += delegate { };
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

                using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);

                        cancelFlag = false;
                        OnProgressChanged(persentage, ref cancelFlag);

                        if (cancelFlag == true)
                        {
                            // Delete dest file here
                            break;
                        }
                    }
                }
            }

            OnComplete();
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