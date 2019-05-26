using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductNotifyFactory
    {
        public static IProductNotify CreateProductNotify()
        {
            string path = ConfigurationManager.AppSettings["BLLProductNotify"];
            string classname = path + ".ProductNotify";
            return (IProductNotify)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
