using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersLogFactory
    {
        public static IOrdersLog CreateOrdersLog() {
            string path = ConfigurationManager.AppSettings["DALORD"].ToString();
            string classname = path + ".OrdersLog";
            return (IOrdersLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

