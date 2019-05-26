using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersBackApplyFactory
    {
        public static IOrdersBackApply CreateOrdersBackApply()
        {
            string path = ConfigurationManager.AppSettings["BLLOrdersBackApply"];
            string classname = path + ".OrdersBackApply";
            return (IOrdersBackApply)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
