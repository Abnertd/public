using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersInvoiceFactory
    {
        public static IOrdersInvoice CreateOrdersInvoice()
        {
            string path = ConfigurationManager.AppSettings["BLLOrdersInvoice"];
            string classname = path + ".OrdersInvoice";
            return (IOrdersInvoice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
