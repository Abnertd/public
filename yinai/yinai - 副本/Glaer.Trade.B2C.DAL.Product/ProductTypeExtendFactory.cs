using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductTypeExtendFactory
    {
        public static IProductTypeExtend CreateProductTypeExtend()
        {
            string path = ConfigurationManager.AppSettings["DALProductTypeExtend"];
            string classname = path + ".ProductTypeExtend";
            return (IProductTypeExtend)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
