using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock
{
    public static class GameEnv
    {
        public static Assembly CurrentAssembly => Assembly.GetExecutingAssembly();

        public static Stream? GetResourceAsStream(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());

            string newPath = path.Replace(@"/", ".");

            if (newPath.StartsWith("."))
                newPath = newPath.Substring(1);

            newPath = resourceNames.FirstOrDefault(r => r.Contains(newPath));

            if (newPath == null)
                throw new FileNotFoundException("Resource not found");

            return assembly.GetManifestResourceStream(newPath);
        }
    }
}
