using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysCountyFactory
    {
        public static ISysCounty CreateSysCounty()
        {
            string path = ConfigurationManager.AppSettings["DALSysCounty"];
            string classname = path + ".SysCounty";
            return (ISysCounty)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
