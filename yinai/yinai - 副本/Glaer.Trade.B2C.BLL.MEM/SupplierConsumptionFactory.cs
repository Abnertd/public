using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierConsumptionFactory
    {
        public static ISupplierConsumption CreateSupplierConsumption()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierConsumption";
            return (ISupplierConsumption)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
