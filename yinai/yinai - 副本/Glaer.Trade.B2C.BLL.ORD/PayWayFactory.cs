using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PayWayFactory
    {
        public static IPayWay CreatePayWay()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"].ToString();
            string classname = path + ".PayWay";
            return (IPayWay)Assembly.Load(path).CreateInstance(classname);
        }

    }
}