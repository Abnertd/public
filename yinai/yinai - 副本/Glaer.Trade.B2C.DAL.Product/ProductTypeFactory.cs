using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductTypeFactory
    {
        public static IProductType CreateProductType()
        {
            string path = ConfigurationManager.AppSettings["DALProductType"];
            string classname = path + ".ProductType";
            return (IProductType)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
