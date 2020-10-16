using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;

namespace AssemblyReaderLib
{
    public class AssemblyReader : IAssemblyReader
    {
        private static readonly IDictionary<Type, Func<Attribute, string>> Attributes =
            new Dictionary<Type, Func<Attribute, string>>
            {
                {typeof(AssemblyInformationalVersionAttribute), t => ((AssemblyInformationalVersionAttribute) t).InformationalVersion},
                {typeof(AssemblyFileVersionAttribute), t => ((AssemblyFileVersionAttribute) t).Version},
                {typeof(AssemblyVersionAttribute), t => ((AssemblyVersionAttribute) t).Version},
                {typeof(AssemblyCompanyAttribute), t => ((AssemblyCompanyAttribute) t).Company},
                {typeof(AssemblyConfigurationAttribute), t => ((AssemblyConfigurationAttribute) t).Configuration},
                {typeof(AssemblyCopyrightAttribute), t => ((AssemblyCopyrightAttribute) t).Copyright},
                {typeof(AssemblyDescriptionAttribute), t => ((AssemblyDescriptionAttribute) t).Description},
                {typeof(AssemblyTitleAttribute), t => ((AssemblyTitleAttribute) t).Title},
                {typeof(NeutralResourcesLanguageAttribute), t => ((NeutralResourcesLanguageAttribute) t).CultureName},
                {typeof(TargetFrameworkAttribute), t => ((TargetFrameworkAttribute) t).FrameworkDisplayName }
            };

        public Assembly LoadAssembly(string filename)
        {
            if (String.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException(nameof(filename));

            var assembly = Assembly.LoadFile(filename);
            return assembly;
        }

        public IDictionary<string, string> GetAssemblyDetails(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var details = new SortedDictionary<string, string>(StringComparer.InvariantCulture);

            var retrievedAttributes = assembly.GetCustomAttributes();
            foreach (var retrieved in retrievedAttributes)
            {
                var attrFunc = Attributes.Where(x => x.Key == retrieved.GetType()).Select(x => x.Value).FirstOrDefault();
                if (attrFunc == null)
                    continue;

                var attrName = retrieved.ToString();
                var attrResult = attrFunc(retrieved);
                details.Add(attrName, attrResult);
            }

            details.Add(nameof(Assembly.FullName), assembly.FullName);

            return details;
        }
    }
}
