using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierMessageFactory
    {
        public static ISupplierMessage CreateSupplierMessage()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplierMessage"];
            string classname = path + ".SupplierMessage";
            return (ISupplierMessage)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
