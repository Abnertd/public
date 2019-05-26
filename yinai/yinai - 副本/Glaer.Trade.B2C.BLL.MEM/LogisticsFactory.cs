using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class LogisticsFactory
    {
        public static ILogistics CreateLogistics()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"];
            string classname = path + ".Logistics";
            return (ILogistics)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
