using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopGradeFactory
    {
        public static ISupplierShopGrade CreateSupplierShopGrade()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShopGrade";
            return (ISupplierShopGrade)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
