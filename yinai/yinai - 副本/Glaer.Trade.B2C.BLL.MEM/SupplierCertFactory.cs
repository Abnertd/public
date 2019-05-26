using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierCertFactory
    {
        public static ISupplierCert CreateSupplierCert()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierCert";
            return (ISupplierCert)Assembly.Load(path).CreateInstance(classname);
        }

        public static ISupplierCertType CreateSupplierCertType()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierCertType";
            return (ISupplierCertType)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
