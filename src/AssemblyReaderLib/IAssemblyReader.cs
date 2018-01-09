using System.Collections.Generic;
using System.Reflection;

namespace AssemblyReaderLib
{
    public interface IAssemblyReader
    {
        Assembly LoadAssembly(string filename);

        IDictionary<string, string> GetAssemblyDetails(Assembly assembly);
    }
}