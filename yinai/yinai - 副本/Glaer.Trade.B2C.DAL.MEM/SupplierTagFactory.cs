using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierTagFactory
    {
        public static ISupplierTag CreateSupplierTag()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierTag";
            return (ISupplierTag)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
