using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersBackApplyProductFactory
    {
        public static IOrdersBackApplyProduct CreateOrdersBackApplyProduct()
        {
            string path = ConfigurationManager.AppSettings["BLLOrdersBackApply"];
            string classname = path + ".OrdersBackApplyProduct";
            return (IOrdersBackApplyProduct)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
