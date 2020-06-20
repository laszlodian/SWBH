using Microsoft.TeamFoundation.Common.Internal;
using System;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    internal static class Program
    {
        private static string option;
        private static string optionMode;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)

        {
            ProcessArguments(args);

            TraceHelper.SetupListener();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            InitializeControllerClassesInstance();

            if (Properties.Settings.Default.IsAppInQuickMode)
            {
                ConsoleHelper consoleHelper = new ConsoleHelper();
                consoleHelper.StartCollectionAndInstallationInConsoleMode();
            }
            else
            {
                Application.Run(new Form1());
            }
        }

        private static void ProcessArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains(":"))
                {
                    //Not used yet - TODO!
                    //options type argument
                    option = args[i].Split(new char[] { ':' })[0];
                    optionMode = args[i].Split(new char[] { ':' })[1];
                }
                else
                {
                    //Normal run mode parameter argument

                    switch (args[i].Trim())
                    {
                        case "/Console":
                            ConsoleMode = true;
                            break;

                        default:
                            ConsoleMode = false;
                            break;
                    }
                }
            }
        }

        public static Form1 Form1;
        public static bool ConsoleMode = false;

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