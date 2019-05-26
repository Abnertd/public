using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductFactory
    {
        public static IProduct CreateProduct() {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".Product";
            return (IProduct)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class ProductWholeSalePriceFactory
    {
        public static IProductWholeSalePrice CreateProductWholeSalePrice()
        {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".ProductWholeSalePrice";
            return (IProductWholeSalePrice)Assembly.Load(path).CreateInstance(classname);
        }
    }

}

