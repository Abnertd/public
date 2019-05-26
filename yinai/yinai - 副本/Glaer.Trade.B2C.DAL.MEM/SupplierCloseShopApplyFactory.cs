using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierCloseShopApplyFactory
    {
        public static ISupplierCloseShopApply CreateSupplierCloseShopApply()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierCloseShopApply";
            return (ISupplierCloseShopApply)Assembly.Load(path).CreateInstance(classname);
        }

    }


}
