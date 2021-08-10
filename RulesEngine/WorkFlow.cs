using RulesDb;
using System;
using System.IO;

namespace RulesEngine
{
    public class WorkFlow
    {
        private ScriptInterface rules;

        public WorkFlow() 
        {
            rules = new ScriptInterface();
        }

        #region scripts
        public int GetLogic(string scriptPath)
        {
            return rules.GetScripts(scriptPath);
        }

        public string Process(Order order)
        {
            return rules.ExecuteMain(order);
        }


        #endregion scripts
    }
}
