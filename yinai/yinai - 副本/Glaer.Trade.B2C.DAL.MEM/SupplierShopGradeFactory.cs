using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopGradeFactory
    {
        public static ISupplierShopGrade CreateSupplierShopGrade()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopGrade";
            return (ISupplierShopGrade)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
