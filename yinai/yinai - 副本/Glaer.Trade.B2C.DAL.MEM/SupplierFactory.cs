using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierFactory
    {
        public static ISupplier CreateSupplier()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".Supplier";
            return (ISupplier)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierCommissionCategoryFactory
    {
        public static ISupplierCommissionCategory CreateSupplierCommissionCategory()
        {
            string path = ConfigurationManager.AppSettings["DALSupplierCommissionCategory"];
            string classname = path + ".SupplierCommissionCategory";
            return (ISupplierCommissionCategory)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierMerchantsFactory
    {
        public static ISupplierMerchants CreateSupplierMerchants()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierMerchants";
            return (ISupplierMerchants)Assembly.Load(path).CreateInstance(classname);
        }
    }


    public class SupplierMerchantsMessageFactory
    {
        public static ISupplierMerchantsMessage CreateSupplierMerchantsMessage()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierMerchantsMessage";
            return (ISupplierMerchantsMessage)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SupplierMarginFactory
    {
        public static ISupplierMargin CreateSupplierMargin()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierMargin";
            return (ISupplierMargin)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
