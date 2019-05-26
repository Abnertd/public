using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierOnlineFactory
    {
        public static ISupplierOnline CreateSupplierOnline()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierOnline";
            return (ISupplierOnline)Assembly.Load(path).CreateInstance(classname);
        }

    }



}
