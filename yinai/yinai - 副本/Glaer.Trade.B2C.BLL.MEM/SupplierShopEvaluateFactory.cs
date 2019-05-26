using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopEvaluateFactory
    {
        public static ISupplierShopEvaluate CreateSupplierShopEvaluate()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShopEvaluate";
            return (ISupplierShopEvaluate)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
