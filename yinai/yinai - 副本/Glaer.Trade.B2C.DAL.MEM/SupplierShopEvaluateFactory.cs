using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopEvaluateFactory
    {
        public static ISupplierShopEvaluate CreateSupplierShopEvaluate()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopEvaluate";
            return (ISupplierShopEvaluate)Assembly.Load(path).CreateInstance(classname);
        }

    }


}
