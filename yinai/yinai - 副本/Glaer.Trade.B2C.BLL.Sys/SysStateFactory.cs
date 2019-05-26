using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysStateFactory
    {
        public static ISysState CreateSysState()
        {
            string path = ConfigurationManager.AppSettings["BLLSysState"];
            string classname = path + ".SysState";
            return (ISysState)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
