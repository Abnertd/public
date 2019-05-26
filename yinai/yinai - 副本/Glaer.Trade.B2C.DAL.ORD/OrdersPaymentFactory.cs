using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersPaymentFactory
    {
        public static IOrdersPayment CreateOrdersPayment()
        {
            string path = ConfigurationManager.AppSettings["DALORD"].ToString();
            string classname = path + ".OrdersPayment";
            return (IOrdersPayment)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
