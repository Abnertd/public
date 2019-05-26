using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductHistoryPriceFactory
    {
        public static IProductHistoryPrice CreateProductHistoryPrice()
        {
            string path = ConfigurationManager.AppSettings["DALProductHistoryPrice"];
            string classname = path + ".ProductHistoryPrice";
            return (IProductHistoryPrice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
