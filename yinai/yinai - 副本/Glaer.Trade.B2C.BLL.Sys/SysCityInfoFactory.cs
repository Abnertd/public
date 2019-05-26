using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysCityFactory
    {
        public static ISysCity CreateSysCity()
        {
            string path = ConfigurationManager.AppSettings["BLLSysCity"];
            string classname = path + ".SysCity";
            return (ISysCity)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
