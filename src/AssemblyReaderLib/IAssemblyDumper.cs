using System.Collections.Generic;

namespace AssemblyReaderLib
{
    public interface IAssemblyDumper
    {
        void DumpAssemblyDetails(IDictionary<string, string> details, string filename);
    }
}
