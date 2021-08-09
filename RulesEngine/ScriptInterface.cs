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
        Dictionary<string, dynamic> scripts;

        private const string SCRIPTEXTENSION = "*.lua";
        private const int ERROR = -1;

        public ScriptInterface()
        {
            scripts = new Dictionary<string, dynamic>();
        }

        internal int GetScripts(string scriptPath)
        {
            int found = 0;

            if (!Directory.Exists(scriptPath))
            {
                Console.WriteLine("Invalid path:" + scriptPath);        // would use a logging api in reality
                return ERROR;
            }

            string[] luaFiles = Directory.GetFiles(scriptPath, SCRIPTEXTENSION);
            foreach (string fileName in luaFiles)
            {
                string luaScript = string.Empty;
                try
                {
                    luaScript = File.ReadAllText(fileName);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid file:" + scriptPath);        // would use a logging api in reality
                    return ERROR;
                }

                try
                {
                    scripts[fileName] = new DynamicLua.DynamicLua();
                    scripts[fileName](luaScript);

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Lua:" + scriptPath);        // would use a logging api in reality
                    return ERROR;
                }
                found++;
            }

            return found;
        }
    }
}
