using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersDeliveryFactory
    {
        public static IOrdersDelivery CreateOrdersDelivery()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"].ToString();
            string classname = path + ".OrdersDelivery";
            return (IOrdersDelivery)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
