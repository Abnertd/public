using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class ConfigFactory
    {
        public static IConfig CreateConfig() 
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".Config";
            return (IConfig)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class SysInterfaceLogFactory
    {
        public static ISysInterfaceLog CreateSysInterfaceLog()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".SysInterfaceLog";
            return (ISysInterfaceLog)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
