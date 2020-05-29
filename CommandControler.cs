using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class CommandControler
    {
        public Dictionary<string, string> PackagesInfo = new Dictionary<string, string>();
        public static CommandControler Instance = null;
        public string sunriseWorkbenchPath = @"C:\_SWB\SWB";
        /// <summary>
        /// The first argument must look like this: jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/"
        /// The second argument is the name of the feature and it looks like this: com.kuka.kmpOmniMove.swb.feature.feature.group -This could be listed out with 'featuresListCommand'
        /// </summary>
        public string installCommandFormat = @"{1}\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} {2}";
        /// <summary>
        /// First argument should look like this: "file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Sunrise/SunriseWorkbench-1.16.2.16-win32-x86.zip!/"
        /// </summary>
        public string featuresListCommandFormat = @"{1}\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} -list";
        private List<string> packages = new List<string>();
        public string realWorkingCommand = "C:/_SWB/SWB/eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash -repository jar:\"file:///C:/_SWB/optionpackages/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345991;01;KUKA Sunrise.Mobility 1.11 KMP 1500.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345992;01;KUKA Sunrise.Mobility 1.11 KMP 200.zip!/\" -installIUs com.kuka.kmpOmniMove.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove1500.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove200.swb.feature.feature.group";

        #region Properties

        private string sWBZipFilePath;

        public string SWBZipFilePath
        {
            get { return sWBZipFilePath; }
            set { sWBZipFilePath = value; }
        }


        private string optionPackageList;
        private List<string> listOfFeatures = new List<string>();
        private List<string> packagesNames = new List<string>();

        public string OptionPackageList
        {
            get { return optionPackageList; }
            set
            {
                optionPackageList = value;

            }
        }

        #endregion


        public CommandControler()
        {
            Instance = this;
        }
        public string CollectOptionPackages(string pathIn)
        {
            if (!CheckPath(pathIn))
                return string.Empty;

            DirectoryInfo optionPackagesPath = new DirectoryInfo(pathIn);
            DataGridView dgv = MainForm.Instance.DataGridViewOfArtifacts;
            foreach (FileInfo package in optionPackagesPath.GetFiles("*.zip"))
            {
                if (package.Name.StartsWith("SunriseWorkbench"))
                {
                    SWBZipFilePath = package.FullName;
                    UnzipSunriseWorkbench(package.FullName, sunriseWorkbenchPath);
                    continue;
                }
                PackagesCount++;
                packages.Add(package.FullName);
                packagesNames.Add(package.Name);
                
            }

            string optionPackageList = FormatPackageNames(packages);

            return optionPackageList;
        }
        public void UnzipSunriseWorkbench(string swbZipfile,string swbFolderPath)
        {         
            ZipFile.ExtractToDirectory(swbZipfile, swbFolderPath);

        }
        public void UnzipSunriseWorkbench()
        {
            ZipFile.ExtractToDirectory(SWBZipFilePath, sunriseWorkbenchPath);
            MainForm.Instance.unzipSWBThread.Abort();
        }
        public bool CheckPath(string pathIn)
        {
            if (!Directory.Exists(pathIn))
            {
                MessageBox.Show("The given path of the SWB does not exists!");
                Trace.TraceWarning("The given path of the SWB does not exists! Given ath: {0}", pathIn);
                return false;
            }
            else
                return true;
        }

        public string FormatPackageNames(List<string> packages)
        {
            string stringToInsertCommand = string.Empty;

            foreach (string item in packages)
            {
                stringToInsertCommand += string.Format(" -repository jar:\"file:///{0}!/\"", item);
            }
            Trace.TraceInformation("Complete command: {0}", stringToInsertCommand);
            return stringToInsertCommand;
        }
        public void InstallOptionPackages()
        {
            /*List the features and store them for the install command
                * Get the names of the option packages and store them for the install command with list features command
                * Build the install command
                * Run the install command 
                */
            string commandToRun = string.Empty;
            optionPackageList = CollectOptionPackages(MainForm.Instance.PathOfOptionPackages);
            //MainForm.Instance.ConfigureDataGridView(OptionPackageList);
            if (OptionPackageList == string.Empty || OptionPackageList == null)
            {
                return;
            }
            else
            {
                OptionPackageList = OptionPackageList.Replace('\\', '/');
                Trace.TraceInformation("Option packages: {0}", OptionPackageList);
            }
            commandToRun = string.Format(featuresListCommandFormat, OptionPackageList, sunriseWorkbenchPath).Replace('\\', '/');
            Trace.TraceInformation("Command to run for list features:{1} {0}", commandToRun, Environment.NewLine);
            string featuresListResult = RunCommand(commandToRun);

            List<string> featuresList=GetFeatures(featuresListResult);
           string featuresCommandPart= BuildFeaturesStringForCommand(featuresList);
            Trace.TraceInformation("Features part of command: {0}",featuresCommandPart);
        
            commandToRun = string.Format(installCommandFormat, OptionPackageList, sunriseWorkbenchPath,featuresCommandPart);
            Trace.TraceInformation("Command to run for install packages: {0}", commandToRun);
            RunCommand(commandToRun);
            //TODO: FIX storing to database
           // SQLManager.Instance.StoreInstall();

        }
        public List<string> features = new List<string>();
        public List<string> versions = new List<string>();
        public int PackagesCount=0;

        public void GetFeaturesVersions(string optionPackageList)
        {
            string versionCommandResult = RunCommand(string.Format(featuresListCommandFormat,optionPackageList,sunriseWorkbenchPath));
           
            foreach (string version in versionCommandResult.Split('\n'))
            {
                if (version.Contains("feature.feature.group"))
                {
                    if (!features.Contains(version.Split('=')[0].Trim()))
                    {
                        PackagesInfo.Add(version.Split('=')[0].Trim(), version.Split('=')[1].Trim());
                        features.Add(version.Split('=')[0].Trim());
                        versions.Add(version.Split('=')[1].Trim());
                    }
                    
                }
            }
        }

        private string BuildFeaturesStringForCommand(List<string> featuresList)
        {
            string featuresForCommand = string.Empty;
            foreach (string feature in featuresList)
            {
                featuresForCommand += string.Format(" -installIUs {0}", feature);
            }

            return featuresForCommand;
        }

        private List<string> GetFeatures(string featuresListResult)
        {
            List<string> featuresNeededForCommand = new List<string>();

            foreach (string row in featuresListResult.Split('\n'))
            {
                if (row.Contains("feature.feature.group"))
                {
                    string feature=row.Split('=')[0];
                    if (!featuresNeededForCommand.Contains(feature.Trim()))
                    {
                        Trace.TraceInformation("Feature found: {0}",feature.Trim());
                        featuresNeededForCommand.Add(feature.Trim());
                    }
                    
                }
            }
            return featuresNeededForCommand;
        }

        private string FormatFeaturesList(string featuresList)
        {
            Trace.TraceInformation("Features List:{1}{0}{1}", featuresList, Environment.NewLine);
            featuresList = featuresList.Split('\n')[1];
            foreach (string feature in featuresList.Split(' '))
            {
                Trace.TraceInformation("Formated console: Feature: {0}", feature);

                listOfFeatures.Add(feature);

            }
            return "";
        }
        public void StartCmd()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

        }
        public string RunCommand(string commandToRun)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(commandToRun);
            cmd.StandardInput.Flush();

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string output = cmd.StandardOutput.ReadToEnd();
            Trace.TraceInformation("Result for real cmd: {0}", output);
                       
            return output;

        }

        private bool CheckOutput(string output)
        {//TODO: add features names to check
            if (!output.Contains("Installing") || !output.Contains("Operation completed"))
            {
                return false;
            }
            else
                return true;
        }

        public void Trying(string cmd)
        {
            string install = "C:/_SWB/SWB/eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash -repository jar:\"file:///C:/_SWB/optionpackages/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345991;01;KUKA Sunrise.Mobility 1.11 KMP 1500.zip!/\" -repository jar:\"file:///C:/_SWB/optionpackages/0000345992;01;KUKA Sunrise.Mobility 1.11 KMP 200.zip!/\" -installIUs com.kuka.kmpOmniMove.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove1500.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove200.swb.feature.feature.group";

            Process p = System.Diagnostics.Process.Start("CMD.exe", " /K " + cmd);


            // File.WriteAllText(@"C:\_SWB\command.txt", p.StandardOutput.ReadToEnd());

        }


    }
}
