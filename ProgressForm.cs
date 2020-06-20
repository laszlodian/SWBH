using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public partial class ProgressForm : Form
    {
        public BackgroundWorker bgProgressSWB = new BackgroundWorker();
        public BackgroundWorker bgProgressArtifacts = new BackgroundWorker();
        public BackgroundWorker bgProgressProducts = new BackgroundWorker();

        public static ProgressForm Instance = null;

        public ProgressForm()
        {
            Instance = this;
            this.Visible = false;
            while (!ArtifactHandler.Instance.startProgresses)
            {
                Thread.Sleep(1000);
            }
            this.Visible = true;
            InitializeComponent();
            InitBackgroundWorker();
            PerformLayout();
            BringToFront();
            Refresh();
            TopMost = true;
        }

        public void InitBackgroundWorker()
        {
            InitProgressBar();

            InitAllBackGroundWorker();
        }

        private void InitAllBackGroundWorker()
        {
            bgProgressArtifacts.DoWork += BgProgressArtifacts_DoWork;
            bgProgressArtifacts.ProgressChanged += BgProgressArtifacts_ProgressChanged;
            bgProgressArtifacts.RunWorkerCompleted += BgProgressArtifacts_RunWorkerCompleted;
            bgProgressArtifacts.WorkerReportsProgress = true;

            bgProgressProducts.DoWork += BgProgressProducts_DoWork;
            bgProgressProducts.ProgressChanged += BgProgressProducts_ProgressChanged;
            bgProgressProducts.RunWorkerCompleted += BgProgressProducts_RunWorkerCompleted;
            bgProgressProducts.WorkerReportsProgress = true;

            bgProgressSWB.DoWork += BgProgressSWB_DoWork;
            bgProgressSWB.ProgressChanged += BgProgressSWB_ProgressChanged;
            bgProgressSWB.RunWorkerCompleted += BgProgressSWB_RunWorkerCompleted;
            bgProgressSWB.WorkerReportsProgress = true;

            bgProgressArtifacts.RunWorkerAsync();
            bgProgressProducts.RunWorkerAsync();
            bgProgressSWB.RunWorkerAsync();
        }

        private void BgProgressSWB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BgProgressSWB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value += e.ProgressPercentage;
        }

        private void BgProgressSWB_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            while (counter < 20)
            {
                Thread.Sleep(200);
                bgProgressSWB.ReportProgress(100 / 20);
                counter++;
            }
        }

        private void BgProgressProducts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BgProgressProducts_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbProducts.Value += e.ProgressPercentage;
        }

        private void BgProgressProducts_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            while (counter < 4)
            {
                Thread.Sleep(2000);
                bgProgressSWB.ReportProgress(100 / 4);
                counter++;
            }
        }

        private void BgProgressArtifacts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BgProgressArtifacts_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int counter = 0;

            while (counter < 13)
            {
                Thread.Sleep(400);
                bgProgressSWB.ReportProgress(100 / 13);
                counter++;
            }
        }

        private void BgProgressArtifacts_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void InitProgressBar()
        {
            pbProducts.Value = 1;
            pbProducts.Maximum = 100;
            pbProducts.Step = 1;

            pbArtifatcts.Value = 1;
            pbArtifatcts.Maximum = 100;
            pbArtifatcts.Step = 1;

            progressBar2.Maximum = 100;
            progressBar2.Step = 1;
            progressBar2.Value = 1;
        }

        private delegate void ChangeInfoTextDelegate(ProgressBar progressBar);

        public void ChangeInfoText(ProgressBar progressBar)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new ChangeInfoTextDelegate(ChangeInfoText), progressBar);
            }
            else
                progressBar.Text = string.Format("{0}%", progressBar.Value);
        }

        public async void Result()
        {
        }

        public async void Start(IAsyncResult ar)
        {
        }

        internal void StartPB()
        {
            this.Visible = true;
            this.BringToFront();
            this.ShowDialog();
        }
    }
}