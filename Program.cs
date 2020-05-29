using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeControllerClassesInstance();

            TraceHelper.SetupListener();
            Trace.TraceInformation("Initializing Form...");
        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void InitializeControllerClassesInstance()
        {
            SwVersion swVersion = new SwVersion();           
            IOHandler iOHandler = new IOHandler();
            ArtifactHandler artifactHandler = new ArtifactHandler();
            CommandControler commandControler = new CommandControler();
            SQLManager sQLManager = new SQLManager();
            CheckStatesHandler checkStatesHandler = new CheckStatesHandler();
        }
    }
}
