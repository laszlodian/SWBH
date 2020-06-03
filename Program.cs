using System;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            TraceHelper.SetupListener();
            //   Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            InitializeControllerClassesInstance();

            Application.Run(new Form1());
        }

        public static Form1 Form1;

        private static void InitializeControllerClassesInstance()
        {
            SwVersion swVersion = new SwVersion();
            ArtifactHandler artifactHandler = new ArtifactHandler();
            CommandControler commandControler = new CommandControler();
            SQLManager sQLManager = new SQLManager();
            CheckStatesHandler checkStatesHandler = new CheckStatesHandler();
            Form1 = new Form1();
            ThreadManager threadManager = new ThreadManager();
        }
    }
}