using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductHistoryPriceFactory
    {
        public static IProductHistoryPrice CreateProductHistoryPrice()
        {
            string path = ConfigurationManager.AppSettings["BLLProductHistoryPrice"];
            string classname = path + ".ProductHistoryPrice";
            return (IProductHistoryPrice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
