using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierCreditLimitLogFactory
    {
        public static ISupplierCreditLimitLog CreateSupplierCreditLimitLog()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierCreditLimitLog";
            return (ISupplierCreditLimitLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
