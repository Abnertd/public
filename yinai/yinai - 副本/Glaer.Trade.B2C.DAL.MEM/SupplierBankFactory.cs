using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierBankFactory
    {
        public static ISupplierBank CreateSupplierBank()
        {
            string path = ConfigurationManager.AppSettings["DALSupplierBank"];
            string classname = path + ".SupplierBank";
            return (ISupplierBank)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
