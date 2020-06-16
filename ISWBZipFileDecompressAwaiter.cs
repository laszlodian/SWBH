using System;
using System.Threading.Tasks;

namespace SWB_OptionPackageInstaller
{
    public interface ISWBZipFileDecompressAwaiter
    {
        bool IsCompleted { get; set; }

        Task<object> GetResultAsync();
        void OnCompleted(Action continuation);
        Task unzip();
    }
}