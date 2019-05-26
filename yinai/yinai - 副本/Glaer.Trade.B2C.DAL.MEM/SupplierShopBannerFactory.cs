using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopBannerFactory
    {
        public static ISupplierShopBanner CreateSupplierShopBanner()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopBanner";
            return (ISupplierShopBanner)Assembly.Load(path).CreateInstance(classname);
        }

    }


}
