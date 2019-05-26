using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{ 
    public class OrdersAccompanyingFactory
    {
        public static IOrdersAccompanying CreateOrdersAccompanying()
        {
            string path = ConfigurationManager.AppSettings["DALORD"];
            string classname = path + ".OrdersAccompanying";
            return (IOrdersAccompanying)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class OrdersAccompanyingGoodsFactory
    {
        public static IOrdersAccompanyingGoods CreateOrdersAccompanyingGoods()
        {
            string path = ConfigurationManager.AppSettings["DALORD"];
            string classname = path + ".OrdersAccompanyingGoods";
            return (IOrdersAccompanyingGoods)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
