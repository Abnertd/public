using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductNotifyFactory
    {
        public static IProductNotify CreateProductNotify()
        {
            string path = ConfigurationManager.AppSettings["DALProductNotify"];
            string classname = path + ".ProductNotify";
            return (IProductNotify)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
