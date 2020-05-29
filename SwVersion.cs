using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SWB_OptionPackageInstaller
{
   public class SwVersion //: TestToolsBase.Configuration.SwVersionBase
    {
        #region Properties
        private Version actualSWVersion = new Version("4.1.0.0");

        public Version ActualSWVersion
        {
            get { return actualSWVersion; }
            set { actualSWVersion = value; }
        }


        #endregion
        public static SwVersion Instance=null;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SwVersion()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                throw new Exception("This class is singleton DO NOT CREATE more!!");

          actualSWVersion= Assembly.GetExecutingAssembly().GetName().Version;

        }



    }
}
