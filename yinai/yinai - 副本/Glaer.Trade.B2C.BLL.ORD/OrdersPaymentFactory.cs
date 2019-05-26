using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersPaymentFactory
    {
        public static IOrdersPayment CreateOrdersPayment()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"].ToString();
            string classname = path + ".OrdersPayment";
            return (IOrdersPayment)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
