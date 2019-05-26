using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopFactory
    {
        public static ISupplierShop CreateSupplierShop()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShop";
            return (ISupplierShop)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierShopDomainFactory
    {
        public static ISupplierShopDomain CreateSupplierShopDomain()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShopDomain";
            return (ISupplierShopDomain)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
