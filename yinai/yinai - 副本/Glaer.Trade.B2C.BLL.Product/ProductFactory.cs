using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductFactory
    {
        public static IProduct CreateProduct() {
            string path = ConfigurationManager.AppSettings["BLLCategory"];
            string classname = path + ".Product";
            return (IProduct)Assembly.Load(path).CreateInstance(classname);
        }
    }

    public class ProductWholeSalePriceFactory
    {
        public static IProductWholeSalePrice CreateProductWholeSalePrice()
        {
            string path = ConfigurationManager.AppSettings["BLLCategory"];
            string classname = path + ".ProductWholeSalePrice";
            return (IProductWholeSalePrice)Assembly.Load(path).CreateInstance(classname);
        }
    }
}

