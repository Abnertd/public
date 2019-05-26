using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierAddressFactory
    {
        public static ISupplierAddress CreateSupplierAddress()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierAddress";
            return (ISupplierAddress)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
