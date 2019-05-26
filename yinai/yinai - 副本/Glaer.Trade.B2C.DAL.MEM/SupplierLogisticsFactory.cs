using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierLogisticsFactory
    {
        public static ISupplierLogistics CreateSupplierLogistics()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".SupplierLogistics";
            return (ISupplierLogistics)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class LogisticsTenderFactory
    {
        public static ILogisticsTender CreateLogisticsTender()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".LogisticsTender";
            return (ILogisticsTender)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
