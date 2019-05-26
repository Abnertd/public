using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysCountyFactory
    {
        public static ISysCounty CreateSysCounty()
        {
            string path = ConfigurationManager.AppSettings["BLLSysCounty"];
            string classname = path + ".SysCounty";
            return (ISysCounty)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
