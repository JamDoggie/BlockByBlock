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
            return CurrentAssembly.GetManifestResourceStream(path);
        }
    }
}
