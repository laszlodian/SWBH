using System;
using System.Collections.Generic;

namespace SWB_OptionPackageInstaller
{
    public class ConsoleHelper
    {
        public List<EConsoleModeArguments> Arguments = new List<EConsoleModeArguments>();
        public static ConsoleHelper Instance;

        public ConsoleHelper()
        {
            Instance = this;

            int i = 1;
            Console.WriteLine(string.Format("Choose from the following options, and press the run-option number:"));
            foreach (string item in Enum.GetNames(typeof(EConsoleModeArguments)))
            {
                Console.WriteLine(string.Format("{1}. {0} mode:", item, i));
                i++;
            }
            Console.ReadLine();
        }

        public void StartCollectionAndInstallationInConsoleMode()
        {
            string command = Arguments.ItemsToString(";"); //common help flag for console apps
            System.Diagnostics.Process pRun;
            pRun = new System.Diagnostics.Process();
            pRun.EnableRaisingEvents = true;

            pRun.StartInfo.FileName = "QuickInstallConsoleMode.exe";
            pRun.StartInfo.Arguments = command;
            pRun.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            pRun.Start();
            pRun.WaitForExit();
        }

        public void ConoleModeFinishedWork(object sender, EventArgs e)
        {
            //Do Something Here
        }
    }
}