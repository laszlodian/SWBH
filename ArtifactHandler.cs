using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
   public class ArtifactHandler
    {
        public static ArtifactHandler Instance=null;
        public List<string> packages = new List<string>();

        private List<string> packagesNames=new List<string>();

        public List<string> PackagesNames
        {
            get { return packagesNames; }
            set { packagesNames = value; }
        }


        public ArtifactHandler()
        {
            Instance = this;
        }

     

        private static void ExpandDataGridWithRows(DataGridView dgv, FileInfo package)
        {
            DataGridViewCheckBoxCell packageInstall = new DataGridViewCheckBoxCell();
            dgv.Rows.Add();
            dgv.Rows.Add(new object[] { package.Name, "version", packageInstall });
            dgv.Refresh();
            dgv.Update();
        }

       
    }
}
