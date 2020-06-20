using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public partial class ProgressForm : Form, INotifyPropertyChanged
    {
        public BackgroundWorker bgProgressSWB = new BackgroundWorker();
        public BackgroundWorker bgProgressArtifacts = new BackgroundWorker();
        public BackgroundWorker bgProgressProducts = new BackgroundWorker();

        public static ProgressForm Instance = null;
        private double productsPercent;
        public double ProductPercent { get { return productsPercent; } set { productsPercent = value; SetProgressValue(pbProducts, productsPercent); OnPropertyChanged(); } }
        private double artifactsPercent;
        public double ArtifactsPercent { get { return artifactsPercent; } set { artifactsPercent = value; SetProgressValue(pbArtifatcts, artifactsPercent); OnPropertyChanged(); } }
        private double sWBPercent;

        public event PropertyChangedEventHandler PropertyChanged;

        public double SWBPercent { get { return sWBPercent; } set { sWBPercent = value; SetProgressValue(progressBar2, sWBPercent); OnPropertyChanged(); } }

        public delegate void SetProgressValueDelegate(ProgressBar progressBar, double percent_in);

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SetProgressValue(ProgressBar progressBar, double percent_in)
        {
            if (percent_in >= 100 || percent_in <= 0)
            {
                return;
            }
            if (progressBar.Name.Contains("progress"))
            {
                if (progressBar2.InvokeRequired)
                {
                    progressBar2.Invoke(new SetProgressValueDelegate(SetProgressValue), progressBar, percent_in);
                }
                else
                    progressBar2.Value = Convert.ToInt32(Math.Round(percent_in));
            }
            else if (progressBar.Name.Contains("pbArtifatcts"))
            {
                if (pbArtifatcts.InvokeRequired)
                {
                    pbArtifatcts.Invoke(new SetProgressValueDelegate(SetProgressValue), progressBar, percent_in);
                }
                else
                    pbArtifatcts.Value = Convert.ToInt32(Math.Round(percent_in));
            }
            else if (progressBar.Name.Contains("pbProducts"))
            {
                if (pbProducts.InvokeRequired)
                {
                    pbProducts.Invoke(new SetProgressValueDelegate(SetProgressValue), progressBar, percent_in);
                }
                else
                    pbProducts.Value = Convert.ToInt32(Math.Round(percent_in));
            }
        }

        public ProgressForm()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            this.Visible = false;
            while (!ArtifactHandler.Instance.startProgresses)
            {
                ArtifactHandler.Instance.StartProgress();
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

            //bgProgressArtifacts.RunWorkerAsync();
            //bgProgressProducts.RunWorkerAsync();
            //bgProgressSWB.RunWorkerAsync();
        }

        private void BgProgressSWB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("BgProgressSWB_RunWorkerCompleted");
        }

        private void BgProgressSWB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GuardProgressbarOverflow();

            progressBar2.Value += e.ProgressPercentage;
        }

        private void BgProgressSWB_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            while (counter < 125)
            {
                Thread.Sleep(2800);
                bgProgressSWB.ReportProgress(100 / 125);
                counter++;
            }
        }

        private void BgProgressProducts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("BgProgressProducts_RunWorkerCompleted");
        }

        private void BgProgressProducts_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GuardProgressbarOverflow();

            pbProducts.Value += e.ProgressPercentage;
        }

        private void BgProgressProducts_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            while (counter < 67)
            {
                Thread.Sleep(2200);
                bgProgressSWB.ReportProgress(100 / 67);
                counter++;
            }
        }

        private void BgProgressArtifacts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("BgProgressArtifacts_RunWorkerCompleted");
        }

        private void BgProgressArtifacts_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GuardProgressbarOverflow();
            pbArtifatcts.Value += e.ProgressPercentage;
        }

        private void GuardProgressbarOverflow()
        {
            if (pbArtifatcts.Value >= 90)
            {
                pbArtifatcts.Value = 33;
            }
            else if (pbProducts.Value >= 90)
            {
                pbProducts.Value = 33;
            }
            else if (progressBar2.Value >= 90)
            {
                progressBar2.Value = 33;
            }
        }

        private void BgProgressArtifacts_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            while (counter < 117)
            {
                Thread.Sleep(2600);
                bgProgressSWB.ReportProgress(100 / 117);
                counter++;
            }
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

        private void btHide_Click(object sender, EventArgs e)
        {
            HideThis();
        }

        private delegate void HideThisDelegate();

        public void HideThis()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HideThisDelegate(HideThis));
            }
            else
            {
                this.TopMost = false;
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }
}