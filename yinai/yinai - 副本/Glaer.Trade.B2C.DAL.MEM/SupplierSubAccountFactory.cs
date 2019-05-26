using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierSubAccountFactory
    {
        public static ISupplierSubAccount CreateSupplierSubAccount()
        {
            string path = ConfigurationManager.AppSettings["DALSupplierSubAccount"];
            string classname = path + ".SupplierSubAccount";
            return (ISupplierSubAccount)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
