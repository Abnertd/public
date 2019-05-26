using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersInvoiceFactory
    {
        public static IOrdersInvoice CreateOrdersInvoice()
        {
            string path = ConfigurationManager.AppSettings["DALOrdersInvoice"];
            string classname = path + ".OrdersInvoice";
            return (IOrdersInvoice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
