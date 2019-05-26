using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierContractTemplateFactory
    {
        public static ISupplierContractTemplate CreateSupplierContractTemplate()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierContractTemplate";
            return (ISupplierContractTemplate)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

