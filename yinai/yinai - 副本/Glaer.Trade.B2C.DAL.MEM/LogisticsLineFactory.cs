using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class LogisticsLineFactory
    {
        public static ILogisticsLine CreateLogisticsLine()
        {
            string path = ConfigurationManager.AppSettings["DALLogisticsLine"];
            string classname = path + ".LogisticsLine";
            return (ILogisticsLine)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
