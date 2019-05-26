using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierPurchaseFactory
    {
        public static ISupplierPurchase CreateSupplierPurchase()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPurchase";
            return (ISupplierPurchase)Assembly.Load(path).CreateInstance(classname);
        }

    }
    public class SupplierPurchaseDetailFactory
    {
        public static ISupplierPurchaseDetail CreateSupplierPurchaseDetail()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPurchaseDetail";
            return (ISupplierPurchaseDetail)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierPurchaseCategoryFactory
    {
        public static ISupplierPurchaseCategory CreateSupplierPurchaseCategory()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierPurchaseCategory";
            return (ISupplierPurchaseCategory)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
