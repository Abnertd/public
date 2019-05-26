using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierInvoiceFactory
    {
        public static ISupplierInvoice CreateSupplierInvoice()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierInvoice";
            return (ISupplierInvoice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
