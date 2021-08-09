using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    class ScriptInterface
    {
        private const string SCRIPTEXTENSION = "*.lua";
        private const int BADFOLDER = -1;

        internal int GetScripts(string scriptPath)
        {
            int found = 0;

            if (!Directory.Exists(scriptPath))
            {
                Console.WriteLine("Invalid path:" + scriptPath);        // would use a logging api in reality
                return BADFOLDER;
            }

            string[] luaFiles = Directory.GetFiles(scriptPath, SCRIPTEXTENSION);
            foreach (string fileName in luaFiles)
                found++;

            return found;
        }
    }
}
