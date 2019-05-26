using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductTagFactory
    {
        public static IProductTag CreateProductTag() {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".ProductTag";
            return (IProductTag)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

