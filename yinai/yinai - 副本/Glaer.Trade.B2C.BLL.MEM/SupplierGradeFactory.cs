using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierGradeFactory
    {
        public static ISupplierGrade CreateSupplierGrade()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierGrade";
            return (ISupplierGrade)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
