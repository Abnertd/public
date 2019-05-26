using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierPriceReportFactory
    {
        public static ISupplierPriceReport CreateSupplierPriceReport()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPriceReport";
            return (ISupplierPriceReport)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierPriceReportDetailFactory
    {
        public static ISupplierPriceReportDetail CreateSupplierPriceReportDetail()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPriceReportDetail";
            return (ISupplierPriceReportDetail)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
