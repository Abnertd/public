using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierCategoryFactory
    {
        public static ISupplierCategory CreateSupplierCategory()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierCategory";
            return (ISupplierCategory)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
