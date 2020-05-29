using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class CheckStatesHandler
    {
        public static CheckStatesHandler Instance;

        public CheckStatesHandler()
        {
            Instance = this;
        }
  
        public bool CheckBothTextBoxIsFilled(TextBox pathOfPackages, TextBox pathOfSWB)
        {
            if (pathOfPackages.Text == string.Empty || pathOfSWB.Text == string.Empty)
            {
                Trace.TraceWarning("One of the textboxes has not filled! Unfilled TextBox: {0}", pathOfPackages.Text == string.Empty ? pathOfPackages : pathOfSWB);
                MessageBox.Show(string.Format("One of the textboxes has not filled! Unfilled TextBox: {0}", pathOfPackages.Text == string.Empty ? pathOfPackages : pathOfSWB));

                return false;
            }
            else
                return true;
        }
    
    }
}
