using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierSubAccountLogFactory
    {
        public static ISupplierSubAccountLog CreateSupplierSubAccountLog()
        {
            string path = ConfigurationManager.AppSettings["DALSupplierSubAccountLog"];
            string classname = path + ".SupplierSubAccountLog";
            return (ISupplierSubAccountLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
