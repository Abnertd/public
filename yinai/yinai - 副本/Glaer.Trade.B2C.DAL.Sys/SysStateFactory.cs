using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysStateFactory
    {
        public static ISysState CreateSysState()
        {
            string path = ConfigurationManager.AppSettings["DALSysState"];
            string classname = path + ".SysState";
            return (ISysState)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
