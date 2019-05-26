using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierTagFactory
    {
        public static ISupplierTag CreateSupplierTag()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierTag";
            return (ISupplierTag)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
