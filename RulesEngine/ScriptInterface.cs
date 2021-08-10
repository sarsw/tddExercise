using RulesDb;
using System;
using System.Collections.Generic;
using System.IO;

namespace RulesEngine
{
    class ScriptInterface
    {
        public Dictionary<string, dynamic> scripts;

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
                    string f = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    f = f.Replace(".lua", "");
                    scripts[f] = new DynamicLua.DynamicLua();
                    scripts[f].import("System");
                    scripts[f]("function PackingSlip(where) return where end");
                    scripts[f]("function ActivateMembership(email) return email end");
                    scripts[f]("function UpgradeMembership(email) return email end");
                    scripts[f]("function AddFreeItem(what) return what end");
                    scripts[f]("function AddCommission(what) return what end");
                    scripts[f](luaScript);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Lua:" + scriptPath+", error:"+e.Message);        // would use a logging api in reality
                    return ERROR;
                }
                found++;
            }

            return found;
        }
        
        internal string ExecuteMain(Order order)
        {
            string rv = string.Empty;
            if (order.Basket.ContainsKey(1))        // enhancement prospect here, for now assume only one item in the basket
            {
                string key = order.Basket[1].SubType;
                if (scripts.ContainsKey(key))
                {
                    try
                    {
                        scripts[key].email = order.CustomerEmail;
                        scripts[key].desc = order.Basket[1].ItemDescription;
                        scripts[key].itemtype = order.Basket[1].ItemType;
                        rv = scripts[key].run();
                    }
                    catch (Exception e)
                    {
                        Console.Write("Runtime error in " + key + ".lua, error:" + e.Message);
                    }
                }

                key = order.Basket[1].ItemType;
                if (scripts.ContainsKey(key))
                {
                    scripts[key].email = order.CustomerEmail;
                    scripts[key].desc = order.Basket[1].ItemDescription;
                    scripts[key].itemtype = order.Basket[1].ItemType;
                    rv += " "+scripts[key].run();
                }
            }
            rv = rv.Trim();
            return rv;
        }
    }
}
