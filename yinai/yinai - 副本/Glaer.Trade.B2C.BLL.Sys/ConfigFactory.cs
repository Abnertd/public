using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class ConfigFactory
    {
        public static IConfig CreateConfig()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".Config";
            return (IConfig)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class SysInterfaceLogFactory
    {
        public static ISysInterfaceLog CreateSysInterfaceLog()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".SysInterfaceLog";
            return (ISysInterfaceLog)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
