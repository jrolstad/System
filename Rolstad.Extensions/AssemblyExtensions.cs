using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rolstad.Extensions
{
    /// <summary>
    /// Extension methods for assemblies
    /// </summary>
    [Obsolete("Use Directus.Extensions instead")]
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets a list of dependent assemblies - and includes the original assembly
        /// </summary>
        public static IEnumerable<AssemblyName> GetDependentAssemblies(this Assembly assembly)
        {
            var list = new List<AssemblyName>();

            if (assembly != null)
            {
                var codebase = new Uri(assembly.CodeBase);
                var binPath = codebase.LocalPath;
                var binFolder = Path.GetDirectoryName(binPath);
                if (binFolder != null)
                {
                    var files = Directory.GetFiles(binFolder, "*.dll");

                    list.AddRange(files.Select(Assembly.LoadFrom).Select(a => a.GetName()));
                }
            }

            return list;
        }
    }
}