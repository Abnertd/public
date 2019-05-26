using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysCityFactory
    {
        public static ISysCity CreateSysCity()
        {
            string path = ConfigurationManager.AppSettings["DALSysCity"];
            string classname = path + ".SysCity";
            return (ISysCity)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
