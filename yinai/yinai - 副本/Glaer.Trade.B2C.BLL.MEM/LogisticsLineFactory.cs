using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class LogisticsLineFactory
    {
        public static ILogisticsLine CreateLogisticsLine()
        {
            string path = ConfigurationManager.AppSettings["BLLLogisticsLine"];
            string classname = path + ".LogisticsLine";
            return (ILogisticsLine)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
