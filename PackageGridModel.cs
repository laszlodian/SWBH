using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
   public class PackageGridModel
    {

        #region Properties
        private bool _installCell;

        public bool InstallCell
        {
            get { return _installCell; }
            set { _installCell = value; }
        }

        private  string _optionPackageName = string.Empty;
        public  string OptionPackageName
        {
            get
            {
                return _optionPackageName;
            }
            set
            {
                _optionPackageName = value;
            }
        }

        private  string _optionPackageVersion = string.Empty;
        public  string Version
        {
            get
            {
                return _optionPackageVersion;
            }
            set
            {
                _optionPackageVersion = value;
            }
        } 
        #endregion



        public PackageGridModel(string optionPackageName,string optionPackageVersion,bool installCell)
        {
            OptionPackageName = optionPackageName;
            Version = optionPackageVersion;
            InstallCell = installCell;
        }
    }
}
