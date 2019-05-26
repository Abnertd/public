using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysMessageFactory
    { 
        public static ISysMessage CreateSysMessage()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".SysMessage";
            return (ISysMessage)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
