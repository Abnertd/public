using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierPayBackApplyFactory
    {
        public static ISupplierPayBackApply CreateSupplierPayBackApply()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPayBackApply";
            return (ISupplierPayBackApply)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
