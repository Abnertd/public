using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersGoodsTmpFactory
    {
        public static IOrdersGoodsTmp CreateOrdersGoodsTmp()
        {
            string path = ConfigurationManager.AppSettings["DALOrdersGoodsTmp"];
            string classname = path + ".OrdersGoodsTmp";
            return (IOrdersGoodsTmp)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
