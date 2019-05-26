using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductTypeFactory
    {
        public static IProductType CreateProductType()
        {
            string path = ConfigurationManager.AppSettings["BLLProductType"];
            string classname = path + ".ProductType";
            return (IProductType)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
