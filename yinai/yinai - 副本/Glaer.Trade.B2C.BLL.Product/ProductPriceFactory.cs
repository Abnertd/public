using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductPriceFactory
    {
        public static IProductPrice CreateProductPrice()
        {
            string path = ConfigurationManager.AppSettings["BLLProductPrice"];
            string classname = path + ".ProductPrice";
            return (IProductPrice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
