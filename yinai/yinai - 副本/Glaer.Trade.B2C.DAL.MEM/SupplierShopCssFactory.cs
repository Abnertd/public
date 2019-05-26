using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopCssFactory
    {
        public static ISupplierShopCss CreateSupplierShopCss()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopCss";
            return (ISupplierShopCss)Assembly.Load(path).CreateInstance(classname);
        }

    }



}
