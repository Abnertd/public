using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductPriceFactory
    {
        public static IProductPrice CreateProductPrice()
        {
            string path = ConfigurationManager.AppSettings["DALProductPrice"];
            string classname = path + ".ProductPrice";
            return (IProductPrice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
