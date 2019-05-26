using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopCssFactory
    {
        public static ISupplierShopCss CreateSupplierShopCss()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShopCss";
            return (ISupplierShopCss)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
