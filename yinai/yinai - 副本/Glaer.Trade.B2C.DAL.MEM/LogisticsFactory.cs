using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class LogisticsFactory
    {
        public static ILogistics CreateLogistics()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".Logistics";
            return (ILogistics)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
