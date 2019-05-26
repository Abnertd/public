using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierAccountLogFactory
    {
        public static ISupplierAccountLog CreateSupplierAccountLog()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierAccountLog";
            return (ISupplierAccountLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
