using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopPagesFactory
    {
        public static ISupplierShopPages CreateSupplierShopPages()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopPages";
            return (ISupplierShopPages)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
