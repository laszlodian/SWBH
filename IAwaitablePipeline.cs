using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWB_OptionPackageInstaller
{
    public interface IAwaitablePipeline<TOutput>
    {
        Task<TOutput> Execute(object input);
    }
}