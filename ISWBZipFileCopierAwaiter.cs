using System;

namespace SWB_OptionPackageInstaller
{
    public interface ISWBZipFileCopierAwaiter
    {
        bool IsCompleted { get; set; }

        void GetResult();
        void OnCompleted(Action continuation);
    }
}