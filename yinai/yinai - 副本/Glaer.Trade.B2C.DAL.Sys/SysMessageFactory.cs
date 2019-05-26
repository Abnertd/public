using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysMessageFactory
    {
        public static ISysMessage CreateSysMessage()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".SysMessage";
            return (ISysMessage)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
