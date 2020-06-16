using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SWB_OptionPackageInstaller
{
    public class AwaitableCopyOptionPackagesFromRemoteDropDownFolder
    {
        public AwaitableCopyOptionPackagesFromRemoteDropDownFolder()
        {
            new CopyOPPackages().GetResult();
        }

        public async Task DoAction()
        {
            //string[] strArray = new string[4];
            //Properties.Settings.Default.ProductsNeededToCopy.CopyTo(strArray, 0);
            //for (int i = 0; i < strArray.Length; i++)
            //{
            //    () => Action(strArray.AsParallel().ForAll<string>(ArtifactHandler.Instance.CopyEveryFilesFromDirectoryToDestinationDir(strArray[i], ArtifactHandler.Instance.LastBuildPath.FullName, ArtifactHandler.Instance.LocalPath.FullName)));
            //}
            //string[] strArray2 = new string[20];
            //Properties.Settings.Default.ArtifactsNeededToCopy.CopyTo(strArray2, 0);
            //for (int i = 0; i < strArray2.Length; i++)
            //{
            //    if (strArray2[i] == String.Empty || strArray2[i] == null)
            //    {
            //        return;
            //    }

            //  ()=> Action(strArray2.AsParallel().ForAll<string>(ArtifactHandler.Instance.CopyEveryFilesFromDirectoryToDestinationDir(strArray2[i], ArtifactHandler.Instance.LastBuildPath.FullName, ArtifactHandler.Instance.LocalPath.FullName)));
            //}
        }
    }

    public class CopyOPPackages : INotifyCompletion
    {
        public CopyOPPackages()
        {
        }

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }
}