using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Reflection;
using System.ComponentModel;
using static System.ComponentModel.TypeConverter;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class AyncExample
    {
        private TextBox resultsTextBox = new TextBox();

        public AyncExample()
        {
        }

        private async void startButton_Click()
        {
            resultsTextBox.Clear();

            // Two-step async call.
            Task sumTask = SumFilesSizesAsync(new DirectoryInfo("c:"));
            await sumTask;

            // One-step async call.
            //await SumPageSizesAsync();
            resultsTextBox.Text += "\r\nControl returned to startButton_Click.\r\n";
        }

        private Task SumFilesSizesAsync(object p)
        {
            throw new NotImplementedException();
        }

        private List<FileInfo> GetFileCollection(DirectoryInfo directory)
        {
            List<FileInfo> fileList = new List<FileInfo>();

            foreach (FileInfo item in directory.GetFiles("*.zip"))
            {
                fileList.Add(item);
            }
            return fileList;
        }

        private void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format
            // is designed to be used with a monospaced font, such as
            // Lucida Console or Global Monospace.
            var bytes = content.Length;
            // Strip off the "https://".
            var displayURL = url.Replace("https://", "");
            resultsTextBox.Text += $"\n{displayURL,-58} {bytes,8}";
        }
    }
}