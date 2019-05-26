using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierInvoiceFactory
    {
        public static ISupplierInvoice CreateSupplierInvoice()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierInvoice";
            return (ISupplierInvoice)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
