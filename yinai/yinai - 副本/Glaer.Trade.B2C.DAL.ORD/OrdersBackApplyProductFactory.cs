using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class OrdersBackApplyProductFactory
    {
        public static IOrdersBackApplyProduct CreateOrdersBackApplyProduct()
        {
            string path = ConfigurationManager.AppSettings["DALOrdersBackApply"];
            string classname = path + ".OrdersBackApplyProduct";
            return (IOrdersBackApplyProduct)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
