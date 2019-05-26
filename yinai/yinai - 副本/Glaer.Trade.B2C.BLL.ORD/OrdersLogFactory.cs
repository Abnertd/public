using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersLogFactory
    {
        public static IOrdersLog CreateOrdersLog()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"].ToString();
            string classname = path + ".OrdersLog";
            return (IOrdersLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
