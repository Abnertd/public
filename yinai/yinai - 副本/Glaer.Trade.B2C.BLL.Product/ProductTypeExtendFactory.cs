using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductTypeExtendFactory
    {
        public static IProductTypeExtend CreateProductTypeExtend()
        {
            string path = ConfigurationManager.AppSettings["BLLProductTypeExtend"];
            string classname = path + ".ProductTypeExtend";
            return (IProductTypeExtend)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
