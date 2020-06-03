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
        public BackgroundWorker bg = new BackgroundWorker();
        private string newInfo;

        public string NewInfo
        {
            get { return newInfo; }
            set { newInfo = value; }
        }

        public static ProgressForm Instance = null;
        public Thread progressThread;
        private bool v;

        public ProgressForm()
        {

            Instance = this;

            InitializeComponent();

            
            PerformLayout();
            BringToFront();
            Refresh();
            //    InitProgressBar();
            //    progressThread = new Thread(new ThreadStart(IncreaseProgressBar));
            //    progressThread.Start(); ;

            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.ProgressChanged += Bg_ProgressChanged;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;

            InitBackgroundWorker();
            bg.RunWorkerAsync();

        }

        public ProgressForm(bool v)
        {
            this.v = v;
        }

        public void InitBackgroundWorker()
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
          

        }
        public void InitProgressBar()
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 1;


        }

        public void IncreaseProgressBar()
        {

            
            for (int j = 0; j < 100000; j++)
            {
                //if (progressBar1.Value >= 99)
                //{
                //    progressBar1.Value = 0;
                //}

                double pow = Math.Pow(j, j); //Calculation
                progressBar1.Value++;
                progressBar1.PerformStep();
            }

        }



        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("BackGroundWorker completed: progress form");
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            for (int j = 0; j < 100000; j++)
            {
                double pow = Math.Pow(j,j);
                backgroundWorker.ReportProgress((j * 100) / 100000);
            }
        }
        delegate void ChangeInfoTextDelegate();
        public void ChangeInfoText()
        {
            if (lbInfo.InvokeRequired)
            {
                lbInfo.Invoke(new ChangeInfoTextDelegate(ChangeInfoText));
            }
            else

                lbInfo.Text = string.Format("{0}", NewInfo);


        }


    }
}
