using System.Linq;

namespace AssemblyReader.NetCore20
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyReader = new AssemblyReaderLib.AssemblyReader();
            var filename = args.ElementAtOrDefault(0);
            var assembly = assemblyReader.LoadAssembly(filename);
            var attributes = assemblyReader.GetAssemblyDetails(assembly);

            var dumper = new AssemblyReaderLib.AssemblyDumper();
            dumper.DumpAssemblyDetails(attributes, filename);
        }
    }
}
