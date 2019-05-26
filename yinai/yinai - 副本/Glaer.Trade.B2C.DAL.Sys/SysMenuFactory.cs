using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysMenuFactory
    {
        public static ISysMenu CreateSysMenu()
        {
            string path = ConfigurationManager.AppSettings["DALSysMenu"];
            string classname = path + ".SysMenu";
            return (ISysMenu)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
