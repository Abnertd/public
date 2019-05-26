using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class AddrFactory
    {
        public static IAddr CreateAddr()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".Addr";
            return (IAddr)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
