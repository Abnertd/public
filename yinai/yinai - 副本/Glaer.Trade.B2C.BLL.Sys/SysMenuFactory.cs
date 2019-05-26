using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysMenuFactory
    {
        public static ISysMenu CreateSysMenu()
        {
            string path = ConfigurationManager.AppSettings["BLLSysMenu"];
            string classname = path + ".SysMenu";
            return (ISysMenu)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
