using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierAddressFactory
    {
        public static ISupplierAddress CreateSupplierAddress()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierAddress";
            return (ISupplierAddress)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
