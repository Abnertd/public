using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierCloseShopApplyFactory
    {
        public static ISupplierCloseShopApply CreateSupplierCloseShopApply()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierCloseShopApply";
            return (ISupplierCloseShopApply)Assembly.Load(path).CreateInstance(classname);
        }

    }


}
