using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersFactory
    {
        public static IOrders CreateOrders()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"].ToString();
            string classname = path + ".Orders";
            return (IOrders)Assembly.Load(path).CreateInstance(classname);
        }

        public static IOrdersContract CreateOrdersContract()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"];
            string classname = path + ".OrdersContract";
            return (IOrdersContract)Assembly.Load(path).CreateInstance(classname);
        }
        public static IOrdersLoanApply CreateOrdersLoanApply()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"];
            string classname = path + ".OrdersLoanApply";
            return (IOrdersLoanApply)Assembly.Load(path).CreateInstance(classname);
        }
    }
}

