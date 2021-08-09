using RulesDb;
using System;
using System.IO;

namespace RulesEngine
{
    public class WorkFlow
    {
        private ScriptInterface scripts;

        public WorkFlow() 
        {
            scripts = new ScriptInterface();
        }

        #region scripts
        public int GetLogic(string scriptPath)
        {
            return scripts.GetScripts(scriptPath);
        }

        public int Process(Item item)
        {
            return 1;
        }


        #endregion scripts
    }
}
