using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierCreditLimitLogFactory
    {
        public static ISupplierCreditLimitLog CreateSupplierCreditLimitLog()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierCreditLimitLog";
            return (ISupplierCreditLimitLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
