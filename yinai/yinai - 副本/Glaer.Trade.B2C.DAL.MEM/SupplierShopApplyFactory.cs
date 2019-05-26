using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopApplyFactory
    {
        public static ISupplierShopApply CreateSupplierShopApply()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopApply";
            return (ISupplierShopApply)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
