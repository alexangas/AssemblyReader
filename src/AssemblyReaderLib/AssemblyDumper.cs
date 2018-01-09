using System;
using System.Collections.Generic;

namespace AssemblyReaderLib
{
    public class AssemblyDumper : IAssemblyDumper
    {
        public void DumpAssemblyDetails(IDictionary<string, string> details, string filename)
        {
            Console.WriteLine($"From assembly: {filename}");
            foreach (var attribute in details)
            {
                Console.WriteLine($"{attribute.Key} = '{attribute.Value}'");
            }
        }
    }
}